using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dollar.One { 
    /// <summary>
    /// Adapted from $1.js
    /// Modified by Ivan
    /// </summary>
    public class OneDollar : MonoBehaviour {

        public Model model = new Model();
        public StrokeMeta currentMeta;

        public Result Recognize(Point[] points, Model.StrokeTypeTest test)
        {
            Result result;

            int t0 = System.DateTime.Now.Millisecond;

            Unistroke input = new Unistroke(points, EvaluationType.ProportionateProtractor);

            float b = float.PositiveInfinity;
            int u = -1;

            List<Unistroke> unistrokes = model.GetDataSet(test);

            for(int i = 0; i < unistrokes.Count; i++)
            {
                Unistroke evaluatedModel = new Unistroke(unistrokes[i]._unistroke, EvaluationType.ProportionateProtractor);
                float d;
                d = Util.OptimalCosineDistance(evaluatedModel.Vector, input.Vector);

                if (d < b)
                {
                    b = d;
                    u = i;
                }
            }

            int elapsedTime = System.DateTime.Now.Millisecond - t0;
            Debug.Log("recognizing");
            if (u == -1)
            {
                result = new Result("no match", "-", "-", 0f, elapsedTime, new Unistroke("no match", "", "", new Point[] { }, EvaluationType.None));
                Debug.Log("result: " + result);
            }
            else
            {
                float score = 1f / b;
                result = new Result(unistrokes[u].Name, unistrokes[u].Variant, unistrokes[u].Test, score, elapsedTime, unistrokes[u]);
            }
            return result;
        }

        public void AddGesture(Point[] points, string testCat, EvaluationType evaluationType)
        {
            string msg = "point list to add: ";
            for(int i = 0; i < points.Length; i++)
            {
                msg += "[" + points[i].X + "," + points[i].Y + "] ";
            }
            Debug.Log(msg);

            Unistroke gesture = new Unistroke(model.strokeClassification.name, model.strokeClassification.variant, testCat, points, evaluationType);
            if (testCat == StrokeType.test_positive)
            {
                model.positiveList.Add(gesture);
                model.SaveDataSet(StrokeType.test_positive);
            }
            else if (testCat == StrokeType.test_negative)
            {
                model.negativeList.Add(gesture);
                model.SaveDataSet(StrokeType.test_negative);
            }
        }

        public void DeleteAllGestures(string testCat)
        {
            if (testCat == StrokeType.test_positive)
            {
                model.positiveList.Clear();
                model.SaveDataSet(StrokeType.test_positive);
            }
            else if (testCat == StrokeType.test_negative)
            {
                model.negativeList.Clear();
                model.SaveDataSet(StrokeType.test_negative);
            }
        }

       

    }




}
