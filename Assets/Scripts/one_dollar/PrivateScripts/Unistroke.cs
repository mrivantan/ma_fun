using System;
namespace Dollar.One
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    [Serializable]
    public class Unistroke
    {
        private _Unistroke unistroke;

        public Unistroke(_Unistroke u)
        {
            unistroke = u;
        }

        public Unistroke(_Unistroke u, EvaluationType evaluationType)
        {
            unistroke = u;
            unistroke.evaluationType = evaluationType;
            Evaluate();
        }

        public Unistroke(Point[] points, EvaluationType evaluationType)
        {
            unistroke.name = "";
            unistroke.variant = "";
            unistroke.test = "";
            unistroke.points = points;
            unistroke.evaluationType = evaluationType;
            Evaluate();
        }

        public Unistroke(string _name, string variant, string test, Point[] points, EvaluationType evaluationType)
        {
            unistroke.name = _name;
            unistroke.variant = variant;
            unistroke.test = test;
            unistroke.points = points;
            unistroke.evaluationType = evaluationType;
            Evaluate();
            
        }

        private void Evaluate()
        {
            float rad;
            switch (unistroke.evaluationType)
            {
                case EvaluationType.OriginalProtractor:
                    unistroke.points = Util.Resample(unistroke.points, Util.NumPoints);
                    rad = Util.IndicativeAngle(unistroke.points);
                    unistroke.points = Util.RotateBy(unistroke.points, -rad);
                    unistroke.points = Util.ScaleTo(unistroke.points, Util.SquareSize);
                    unistroke.points = Util.TranslateTo(unistroke.points, Util.Origin);
                    unistroke.vector = Util.Vectorize(unistroke.points);
                    break;
                case EvaluationType.ResampledOnly:
                    unistroke.points = Util.Resample(unistroke.points, Util.NumPoints);
                    rad = Util.IndicativeAngle(unistroke.points);
                    unistroke.points = Util.TranslateTo(unistroke.points, Util.Origin);
                    unistroke.vector = Util.Vectorize(unistroke.points);
                    break;
                case EvaluationType.ProportionateProtractor:
                    unistroke.points = Util.Resample(unistroke.points, Util.NumPoints);
                    rad = Util.IndicativeAngle(unistroke.points);
                    unistroke.points = Util.RotateBy(unistroke.points, -rad);
                    unistroke.points = Util.ScaleProportionatelyTo(unistroke.points, Util.SquareSize);
                    unistroke.points = Util.TranslateTo(unistroke.points, Util.Origin);
                    unistroke.vector = Util.Vectorize(unistroke.points);
                    break;
                case EvaluationType.ResampledScaledProportionately:
                    unistroke.points = Util.Resample(unistroke.points, Util.NumPoints);
                    rad = Util.IndicativeAngle(unistroke.points);
                    unistroke.points = Util.ScaleProportionatelyTo(unistroke.points, Util.SquareSize);
                    unistroke.points = Util.TranslateTo(unistroke.points, Util.Origin);
                    unistroke.vector = Util.Vectorize(unistroke.points);
                    break;
                default:
                    break;
            }
        }

        public string Name
        {
            get { return unistroke.name; }
            set { unistroke.name = value; }
        }
        public string Variant
        {
            get { return unistroke.variant; }
            set { unistroke.variant = value; }
        }
        public string Test
        {
            get { return unistroke.test; }
            set { unistroke.test = value; }
        }
        public Point[] Points
        {
            get { return unistroke.points; }
            set { unistroke.points = value; }
        }

        public float[] Vector
        {
            get { return unistroke.vector; }
            set { unistroke.vector = value; }
        }

        public _Unistroke _unistroke
        {
            get { return unistroke; }
            set { unistroke = value; }
        }

        public EvaluationType evaluationType
        {
            get { return unistroke.evaluationType; }
        }
    }

    [Serializable]
    public struct _Unistroke
    {
        public string name;
        public string variant;
        public string test;
        public Point[] points;
        public float[] vector;
        public EvaluationType evaluationType;
    }

    public enum EvaluationType { OriginalProtractor, ResampledOnly, ProportionateProtractor, ResampledScaledProportionately, None }
}