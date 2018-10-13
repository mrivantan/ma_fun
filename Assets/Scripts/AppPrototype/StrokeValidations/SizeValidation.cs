using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace App.Validation.Size { 
    public class SizeValidation : MonoBehaviour {

        public UIModel strokeModel;
        public Model.Word.WordModel wordModel;
        public Animator sceneSM;
        public StrokeSize[] strokes;

        public Vector2 canvasRefRes = new Vector2(768, 1024);

        [SerializeField]
        private Vector2 idealSize;
        [SerializeField]
        private Vector2 inputSize;



        // for concurrent additional actions
        public UnityEvent onTestPass;
        public UnityEvent onTestFail;

        public UnityEvent onEnd;

        public void TestSize()
        {
            int strokeIndex = sceneSM.GetInteger("StrokeCount");

            idealSize = GetWorldSize(strokes[strokeIndex].stroke);
            inputSize = BoundingBoxSize(strokeModel.worldPointList.ToArray());

            Debug.Log("Testing input size: " + inputSize + " against Stroke ideal size: " + idealSize);

            sceneSM.SetBool("PassSize", false);
            Vector2 tol = strokes[strokeIndex].tolerance;
            if (InRange(inputSize, idealSize, tol))
            {
                Debug.Log("Stroke size test Pass");
                sceneSM.SetBool("PassSize", true);
                onTestPass.Invoke();
            }
            else
            {
                Debug.Log("Stroke size test Fail");
                onTestFail.Invoke();
            }
            onEnd.Invoke();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a">input</param>
        /// <param name="b">ideal</param>
        /// <param name="t">tolerance</param>
        /// <returns></returns>
        private bool InRange(Vector2 a, Vector2 b, Vector2 t)
        {
            return a.x <= b.x + t.x && a.x >= b.x - t.x && a.y <= b.y + t.y && a.y >= b.y - t.y;
        }

        private Vector2 GetWorldSize(RectTransform rt)
        {
            Vector2 s = new Vector2(rt.rect.size.x, rt.rect.size.y);
            //Debug.Log("expected width:" + s.x);
            float camS = Camera.main.orthographicSize * 2;
            Vector2 scaledSize = new Vector2(s.x / canvasRefRes.y * camS, s.y / canvasRefRes.y * camS);

            return scaledSize;
        }

        private Vector2 BoundingBoxSize(Vector3[] worldPoints)
        {
            float minX = float.PositiveInfinity;
            float maxX = float.NegativeInfinity;
            float minY = float.PositiveInfinity;
            float maxY = float.NegativeInfinity;
            for (var i = 0; i < worldPoints.Length; i++)
            {
                minX = Mathf.Min(minX, worldPoints[i].x);
                minY = Mathf.Min(minY, worldPoints[i].y);
                maxX = Mathf.Max(maxX, worldPoints[i].x);
                maxY = Mathf.Max(maxY, worldPoints[i].y);
            }
            float width = Mathf.Abs(maxX - minX);
            float height = Mathf.Abs( maxY - minY);
            return new Vector2(width,height);
        }

    }


    [System.Serializable]
    public struct StrokeSize
    {
        public RectTransform stroke;
        public Vector2 tolerance;
    }
}