﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dollar.One
{

    public static class Util
    {
        //public static int NumUnistrokes = 16;
        public static int NumPoints = 64;
        public static float SquareSize = 250f;
        public static Point Origin = new Point(0f, 0f);
        public static float Diagonal = Mathf.Sqrt( (SquareSize * SquareSize) * 2 );
        public static float HalfDiagonal = 0.5f * Diagonal;
        public static float AngleRange = Mathf.Deg2Rad * 45f;
        public static float AnglePrecision = Mathf.Deg2Rad * 2f;
        public static float Phi = 0.5f * (-1f + Mathf.Sqrt(5f));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static Point[] Resample(Point[] points, int n)
        {
            float I = PathLength(points) / (n - 1); // interval length
            float D = 0f;
            List<Point> newpoints = new List<Point>(n);
            List<Point> srcPts = new List<Point>(points);
            for (int i = 1; i < points.Length; i++)
            {
                float d = Distance(points[i - 1], points[i]);
                if ((D + d) >= I)
                {
                    float qx = points[i - 1].X + ((I - D) / d) * (points[i].X - points[i - 1].X);
                    float qy = points[i - 1].Y + ((I - D) / d) * (points[i].Y - points[i - 1].Y);
                    Point q = new Point(qx, qy);
                    newpoints.Add(q);
                    srcPts.Insert(i, q); // insert 'q' at position i in points s.t. 'q' will be the next i
                    D = 0f;
                }
                else D += d;
            }
            // somtimes we fall a rounding-error short of adding the last point, so add it if so
            if (newpoints.Count == n - 1)
            {
                newpoints.Add(srcPts[srcPts.Count - 1]);
            }
            return newpoints.ToArray();
        }

        public static float IndicativeAngle(Point[] points)
        {
            Point c = Centroid(points);
            return points.Length > 0 ? Mathf.Atan2(c.Y - points[0].Y, c.X - points[0].X) : 0;
        }

        /// <summary>
        /// rotates points around centroid
        /// </summary>
        /// <param name="points"></param>
        /// <param name="radians"></param>
        /// <returns></returns>
        public static Point[] RotateBy(Point[] points, float radians)
        {
            Point c = Centroid(points);
            float cos = Mathf.Cos(radians);
            float sin = Mathf.Sin(radians);
            List<Point> newpoints = new List<Point>(points.Length);
            for (var i = 0; i < points.Length; i++)
            {
                var qx = (points[i].X - c.X) * cos - (points[i].Y - c.Y) * sin + c.X;
        
                var qy = (points[i].X - c.X) * sin + (points[i].Y - c.Y) * cos + c.Y;
                newpoints.Add(new Point(qx, qy));
            }
            return newpoints.ToArray();
        }

        /// <summary>
        /// scales the points so that they form the size given. does not restore the origin of the box.
        /// non-uniform scale; assumes 2D gestures (i.e., no lines)
        /// </summary>
        /// <param name="points"></param>
        /// <param name="size">SquareSize canvas area</param>
        /// <returns></returns>
        public static Point[] ScaleTo(Point[] points, float size)
        {
            Rectangle B = BoundingBox(points);
            List<Point> newpoints = new List<Point>(points.Length);
            for (var i = 0; i < points.Length; i++)
            {
                var qx = points[i].X * (size / B.Width);
                var qy = points[i].Y * (size / B.Height);
                newpoints.Add(new Point(qx, qy));
            }
            return newpoints.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Point[] ScaleProportionatelyTo(Point[] points, float size)
        {
            Rectangle B = BoundingBox(points);
            float widthMultiplier, heightMultiplier;
            if(B.Width > B.Height)
            {
                // wider
                widthMultiplier = heightMultiplier = size / B.Width;
            }
            else if (B.Width < B.Height)
            {
                // taller
                widthMultiplier = heightMultiplier = size / B.Height;
            }
            else
            {
                // equal
                widthMultiplier = size / B.Width;
                heightMultiplier = size / B.Height;
            }

            List<Point> newpoints = new List<Point>(points.Length);



            for (var i = 0; i < points.Length; i++)
            {
                var qx = points[i].X * widthMultiplier;
                var qy = points[i].Y * heightMultiplier;
                newpoints.Add(new Point(qx, qy));
            }
            return newpoints.ToArray();
        }


        /// <summary>
        /// Translates points' centroid to target
        /// </summary>
        /// <param name="points">path</param>
        /// <param name="pt">target centroid</param>
        /// <returns>translated path</returns>
        public static Point[] TranslateTo(Point[] points, Point pt)
        {
            Point c = Centroid(points);
            List<Point> newpoints = new List<Point>(points.Length);
            for (var i = 0; i < points.Length; i++)
            {
                var qx = points[i].X + pt.X - c.X;
                var qy = points[i].Y + pt.Y - c.Y;
                newpoints.Add( new Point(qx, qy) );
            }
            return newpoints.ToArray();
        }

        /// <summary>
        /// for Protractor
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        /// <seealso cref="http://yangl.org/protractor/"/>
        public static float[] Vectorize(Point[] points) // for Protractor
        {
            float sum = 0f;
            List<float> vector = new List<float>(points.Length * 2);
            for (int i = 0; i < points.Length; i++)
            {
                vector.Add(points[i].X);
                vector.Add(points[i].Y);
                sum += points[i].X * points[i].X + points[i].Y * points[i].Y;
            }
            float magnitude = Mathf.Sqrt(sum);
            for (int i = 0; i < vector.Count; i++)
            {
                vector[i] /= magnitude;
            }
            return vector.ToArray();
        }

        /// <summary>
        /// for Protractor
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        /// <seealso cref="http://yangl.org/protractor/"/>
        public static float OptimalCosineDistance(float[] v1, float[] v2)
        {
            float a = 0f;
            float b = 0f;
            for (int i = 0; i < Mathf.Min(v1.Length, v2.Length); i += 2)
            {
                a += v1[i] * v2[i] + v1[i + 1] * v2[i + 1];
                b += v1[i] * v2[i + 1] - v1[i + 1] * v2[i];
            }
            var angle = Mathf.Atan(b / a);
            return Mathf.Acos(a * Mathf.Cos(angle) + b * Mathf.Sin(angle));
        }

        /// <summary>
        /// Find the deviation distance when rotated to fix to all 8 segments (45 deg * 8 segments = 360deg) gets the smallest deviation distance amongst them
        /// </summary>
        /// <param name="points">Input stroke</param>
        /// <param name="T">Unistroke model</param>
        /// <param name="a">min AngleRange</param>
        /// <param name="b">max AngleRange</param>
        /// <param name="threshold">Angle Precision</param>
        /// <returns>Smallest possible deviation distance</returns>
        public static float DistanceAtBestAngle(Point[] points, Unistroke T, float a, float b, float threshold)
        {
            float x1 = Phi * a + (1f - Phi) * b;
            float f1 = DistanceAtAngle(points, T, x1);
            float x2 = (1f - Phi) * a + Phi * b;
            var f2 = DistanceAtAngle(points, T, x2);
            while (Mathf.Abs(b - a) > threshold)
            {
                if (f1 < f2)
                {
                    b = x2;
                    x2 = x1;
                    f2 = f1;
                    x1 = Phi * a + (1f - Phi) * b;
                    f1 = DistanceAtAngle(points, T, x1);
                }
                else
                {
                    a = x1;
                    x1 = x2;
                    f1 = f2;
                    x2 = (1f - Phi) * a + Phi * b;
                    f2 = DistanceAtAngle(points, T, x2);
                }
            }
            return Mathf.Min(f1, f2);
        }

        /// <summary>
        /// Finds deviation distance after adjusting rotation
        /// </summary>
        /// <param name="points">Input stroke</param>
        /// <param name="T">Unistroke model</param>
        /// <param name="radians">Rotation transform</param>
        /// <returns>Deviation distance</returns>
        public static float DistanceAtAngle(Point[] points, Unistroke T, float radians)
        {
            Point[] newpoints = RotateBy(points, radians);
            return PathDistance(newpoints, T.Points);
        }

        /// <summary>
        /// Find the centroid of a path
        /// </summary>
        /// <param name="points">Path points</param>
        /// <returns>Centroid point</returns>
        public static Point Centroid(Point[] points)
        {
            float x = 0f;
            float y = 0f;
            for (int i = 0; i < points.Length; i++)
            {
                x += points[i].X;
                y += points[i].Y;
            }
            x /= points.Length;
            y /= points.Length;
            return new Point(x, y);
        }

        /// <summary>
        /// Find the bounding box of a path
        /// </summary>
        /// <param name="points">Path points</param>
        /// <returns>Bounding box rectangle</returns>
        public static Rectangle BoundingBox(Point[] points)
        {
            float minX = float.PositiveInfinity;
            float maxX = float.NegativeInfinity;
            float minY = float.PositiveInfinity;
            float maxY = float.NegativeInfinity;
            for (var i = 0; i < points.Length; i++)
            {
                minX = Mathf.Min(minX, points[i].X);
                minY = Mathf.Min(minY, points[i].Y);
                maxX = Mathf.Max(maxX, points[i].X);
                maxY = Mathf.Max(maxY, points[i].Y);
            }
            return new Rectangle(minX, minY, maxX - minX, maxY - minY);
        }

        /// <summary>
        /// Average deviation distance when comparing 2 paths
        /// </summary>
        /// <param name="pts1">First path</param>
        /// <param name="pts2">Second path</param>
        /// <returns>Average deviation distance</returns>
        public static float PathDistance(Point[] pts1, Point[] pts2)
        {
            if(pts1.Length != pts2.Length)
            {
                Debug.LogError("Unequal number of points for paths");
            }

            float d = 0f;
            for (int i = 0; i > pts1.Length; i++)
                d += Distance(pts1[i], pts2[i]);
            return d / pts1.Length;
        }

        /// <summary>
        /// Total length of a path simplified into a poly
        /// </summary>
        /// <param name="points">Points on a path</param>
        /// <returns>Total length of poly path</returns>
        public static float PathLength(Point[] points)
        {
            float d = 0f;
            for (int i = 1; i < points.Length; i++)
                d += Distance(points[i - 1], points[i]);
            return d;
        }

        /// <summary>
        /// Distance of 2 points
        /// </summary>
        /// <param name="p1">Point From</param>
        /// <param name="p2">Point To</param>
        /// <returns>Float distance of 2 points</returns>
        public static float Distance(Point p1, Point p2)
        {
            float dx = p2.X - p1.X;
            float dy = p2.Y - p1.Y;
            return Mathf.Sqrt(dx * dx + dy * dy);
        }


        public static StrokeMeta GetMetaByIndex(this Dictionary<string, StrokeMeta> dictionary, int index)
        {
            StrokeMeta result = dictionary["h"];
            foreach(KeyValuePair<string, StrokeMeta> kvp in dictionary)
            {
                StrokeMeta datum = kvp.Value;
                if(datum.index == index)
                {
                    result = datum;
                    break;
                }
            }

            return result;
        }

    }


    /// <summary>
    /// </summary>
    /// <seealso cref="https://en.wikipedia.org/wiki/Stroke_(CJKV_character)"/>
    /// <seealso cref="https://www.unicode.org/charts/PDF/U31C0.pdf"/>
    public static class StrokeType
    {
        public const string name_H = "h";
        public const string name_HG = "hg";
        public const string name_HP = "hp";
        public const string name_HPWG = "hpwg";
        public const string name_HZ = "hz";
        public const string name_HZT = "hzt";
        public const string name_HZW = "hzw";
        public const string name_HZG = "hzg";
        public const string name_HZWG = "hzwg";
        public const string name_HZZ = "hzz";
        public const string name_HZZP = "hzzp";
        public const string name_HZZZ = "hzzz";
        public const string name_HZZZG = "hzzzg";
        public const string name_HXWG = "hxwg";
        public const string name_T = "t";
        public const string name_TN = "tn";
        public const string name_S = "s";
        public const string name_SG = "sg";
        public const string name_ST = "st";
        public const string name_SP = "sp";
        public const string name_SW = "sw";
        public const string name_SWZ = "swz";
        public const string name_SWG = "swg";
        public const string name_SZ = "SZ";
        public const string name_SZP = "szp";
        public const string name_SZZ = "szz";
        public const string name_SZWG = "szwg";
        public const string name_P = "p";
        public const string name_PZ = "pz";
        public const string name_PD = "pd";
        public const string name_PG = "pg";
        public const string name_D = "d";
        public const string name_N = "n";
        public const string name_WG = "wg";
        public const string name_XG = "xg";


        public const string variant_none = "none";

        public const string variant_straight = "straight";
        public const string variant_slanted = "slanted";

        public const string variant_short = "short";
        public const string variant_long = "long";

        public const string variant_forward = "forward";
        public const string variant_backward = "backward";

        public const string test_positive = "positive";
        public const string test_negative = "negative";

        public static int VariantNameToIndex(string variantName)
        {
            int result = 0;
            if( variantName == variant_slanted ||
                variantName == variant_long ||
                variantName == variant_backward)
            {
                result = 1;
            }
            return result;
        }
    }



    [System.Serializable]
    public struct StrokeClassification
    {
        public string name;
        public string variant;
        public string test;
        public string description;

    }
}
