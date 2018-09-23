using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dollar.One;
namespace App.Model.Word { 
    [RequireComponent(typeof(WordModel))]
    public class WordModelManager : MonoBehaviour {

        public WordModel model;
        public string wordName;
        public Vector2 sizeTolerance = new Vector2(80f, 80f);
        public Vector2 positionTolerance = new Vector2(100f, 100f);

        public void Init () {
            model = GetComponent<WordModel>();
	    }

        /// <summary>
        /// 
        /// </summary>
        public void LoadWordData()
        {
            model.currentWord = model.wordDictionary[wordName]._wordMeta;
        }


        public void PopulateModel()
        {
            // TODO: Populate dictionary from XML files instead of hard coded
            WordMeta word = new WordMeta();
            word.Word = "大";
            word.Id = 0;
            word.Strokes = new string[] { StrokeType.name_H, StrokeType.name_P, StrokeType.name_N };
            word.Variants = new string[] { StrokeType.variant_straight, StrokeType.variant_none, StrokeType.variant_none };
            word.Intersections = new int[,] {
                { (int)IntersectionType.Self, (int)IntersectionType.MustIntersect, (int)IntersectionType.CanIntersect },
                { (int)IntersectionType.MustIntersect, (int)IntersectionType.Self, (int)IntersectionType.CanIntersect },
                { (int)IntersectionType.CanIntersect, (int)IntersectionType.CanIntersect, (int)IntersectionType.Self }
            };
            // 468 40, 262 540, 246 360
            word.BBSizes = new Vector2[] { new Vector2(468, 40), new Vector2(262, 540), new Vector2(246, 360) };
            // 300 370, 198 300, 411 210
            word.Centroids = new Vector2[] { new Vector2(300, 370), new Vector2(198, 300), new Vector2(411, 210) };
            model.wordDictionary.Add("大", word);
        }
    }


   
}
