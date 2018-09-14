namespace Dollar.One
{
    [System.Serializable]
    public struct StrokeMeta
    {
        public int index;
        public string strokeName;
        public string strokeLabel;
        public string[] strokeVariants;
        public string[] test;
        public string[] description;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_index">index id</param>
        /// <param name="_strokeName">code name of stroke</param>
        /// <param name="_strokeLabel">display name for menu</param>
        /// <param name="_strokeVariants">variant type</param>
        /// <param name="_description">variant description</param>
        public StrokeMeta(int _index, string _strokeName, string _strokeLabel, string[] _strokeVariants, string[] _description)
        {
            index = _index;
            strokeName = _strokeName;
            strokeLabel = _strokeLabel;
            strokeVariants = _strokeVariants;
            description = _description;
            test = new string[] { "negative", "positive" };
        }

    }
}
