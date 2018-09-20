using Dollar.One;
using UnityEngine;
namespace App.Model.Word
{
    public class WordMeta
    {
        public _WordMeta _wordMeta;

        /// <summary>
        /// Actual Chinese word
        /// </summary>
        public string Word{
            get{return _wordMeta.word;}
            set{_wordMeta.word = value;}
        }

        /// <summary>
        /// unique id
        /// </summary>
        public int Id{
            get{return _wordMeta.id;}
            set{_wordMeta.id = value;}
        }

        /// <summary>
        /// Uses stroke names from Dollar.One.StrokeType
        /// </summary>
        public string[] Strokes{
            get{return _wordMeta.strokes;}
            set{_wordMeta.strokes = value;}
        }

        /// <summary>
        /// Uses stroke types from Dollar.One.StrokeType
        /// </summary>
        public string[] Variants{
            get{return _wordMeta.variants;}
            set{_wordMeta.variants = value;}
        }

        /// <summary>
        /// Stroke variants in int for indexing
        /// </summary>
        public int[] VariantIndexes{
            get {
                int[] result = new int[_wordMeta.variants.Length];
                for (int i = 0; i < result.Length; i++)
                    result[i] = StrokeType.VariantNameToIndex(_wordMeta.variants[i]);
                return result;
            }
        }

        /// <summary>
        /// Uses IntersectionType enum
        /// </summary>
        public int[,] Intersections{
            get{return _wordMeta.intersections;}
            set{_wordMeta.intersections = value;}
        }

        /// <summary>
        /// Width and height of bounding boxes of each stroke
        /// </summary>
        public Vector2[] BBSizes{
            get{return _wordMeta.bbSizes;}
            set{_wordMeta.bbSizes = value;}
        }

        /// <summary>
        /// Centroid of bounding box of each stroke
        /// </summary>
        public Vector2[] Centroids{
            get{return _wordMeta.centroids;}
            set{_wordMeta.centroids = value;}
        }

    }

    [System.Serializable]
    public struct _WordMeta
    {
        public string word;
        public int id;
        public string[] strokes;
        public string[] variants;
        public int[,] intersections;
        public Vector2[] bbSizes;
        public Vector2[] centroids;
    }

    [System.Serializable]
    public enum IntersectionType { Self, NoIntersect, CanIntersect, MustIntersect }

}