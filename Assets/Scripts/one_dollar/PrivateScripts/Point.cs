namespace Dollar.One {
    /// <summary>
    /// 
    /// </summary>
    /// 
    [System.Serializable]
    public class Point
    {
        private float _x, _y;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Point(float x, float y)
        {
            _x = x;
            _y = y;
        }
        public float X
        {
            get { return _x; }
            set { _x = value; }
        }
        public float Y
        {
            get { return _y; }
            set { _y = value; }
        }
    }
}
