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




        public string Name
        {
            get { return unistroke.name; }
            set { unistroke.name = value; }
        }
        public Point[] Points
        {
            get { return unistroke.points; }
            set { unistroke.points = value; }
        }
        public float Radians
        {
            get { return unistroke.radians; }
            set { unistroke.radians = value; }
        }
        public float[] Vector
        {
            get { return unistroke.vector; }
            set { unistroke.vector = value; }
        }



    }

    [Serializable]
    public struct _Unistroke
    {
        public string name;
        public Point[] points;
        public float radians;
        public float[] vector;
    }
}