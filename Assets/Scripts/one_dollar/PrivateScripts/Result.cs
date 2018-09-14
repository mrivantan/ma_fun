namespace Dollar.One
{
    public class Result
    {
        string _name;
        string _variant;
        string _test;
        float _score;
        float _elapsedTime;
        Unistroke _unistroke;
        public Result(string name, string variant, string test, float score, float time, Unistroke unistroke)
        {
            _name = name;
            _variant = variant;
            _test = test;
            _score = score;
            _elapsedTime = time;
            _unistroke = unistroke;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Variant
        {
            get { return _variant; }
            set { _variant = value; }
        }
        public string Test
        {
            get { return _test; }
            set { _test = value; }
        }
        public float Score
        {
            get { return _score; }
            set { _score = value; }
        }
        public float Time
        {
            get { return _elapsedTime; }
            set { _elapsedTime = value; }
        }

        public Point[] Points
        {
            get { return _unistroke.Points; }
        }
    }
}