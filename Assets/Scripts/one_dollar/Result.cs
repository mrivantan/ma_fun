namespace Dollar.One
{
    public class Result
    {
        string _name;
        float _score;
        float _elapsedTime;

        public Result(string name, float score, float time)
        {
            _name = name;
            _score = score;
            _elapsedTime = time;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
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
    }
}