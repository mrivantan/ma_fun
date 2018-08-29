using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dollar.One { 
    /// <summary>
    /// Adapted from $1.js
    /// </summary>
    public class OneDollar : MonoBehaviour {


        public Result Recognize(Point[] points, bool useProtractor)
        {
            Result result;

            float t0 = Time.time;
            Point[] p = Util.Resample(points, Util.NumPoints);
            float rad = Util.IndicativeAngle(p);
            p = Util.RotateBy(p, -rad);
            p = Util.ScaleTo(p, Util.SquareSize);
            p = Util.TranslateTo(p, Util.Origin);
            float[] vector = Util.Vectorize(p);

            float b = float.PositiveInfinity;
            int u = -1;

            List<Unistroke> unistrokes = new List<Unistroke>(); // TODO: insert model here

            for(int i = 0; i < unistrokes.Count; i++)
            {
                float d;
                if (useProtractor)
                    d = Util.OptimalCosineDistance(unistrokes[i].Vector, vector);
                else
                    d = Util.DistanceAtBestAngle(p, unistrokes[i], -Util.AngleRange, Util.AngleRange, Util.AnglePrecision);

                if (d < b)
                {
                    b = d;
                    u = i;
                }
            }

            float elapsedTime = Time.time - t0;

            if (u == -1)
                result = new Result("no match", 0f, elapsedTime);
            else {
                float score = useProtractor ? 1f / b : 1f - b / Util.HalfDiagonal;
                result = new Result(unistrokes[u].Name, score, elapsedTime);
            }
            return result;
        }

        public int AddGesture(string name, Point[] points)
        {
            int count = 0;
            //TODO: 
            

            return count;
        }


    }


}
