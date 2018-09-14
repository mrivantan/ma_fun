namespace Dollar.One { 
    public class Rectangle
    {
        private float _x, _y, _width, _height;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Rectangle(float x, float y, float width, float height)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
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
        public float Width
        {
            get { return _width; }
            set { _width = value; }
        }
        public float Height
        {
            get { return _height; }
            set { _height = value; }
        }
    }
}
