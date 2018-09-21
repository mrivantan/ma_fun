using UnityEngine;
using UnityEngine.Events;

namespace App.Validation.Position { 
    public class PositionValidation : MonoBehaviour {

        public UIModel strokeModel;
        public Model.Word.WordModel wordModel;
        public Animator sceneSM;
        public StrokePosition[] strokes;
        [SerializeField]
        private Vector2 idealPosition;
        [SerializeField]
        private Vector2 inputPosition;

        // for concurrent additional actions
        public UnityEvent onTestPass;
        public UnityEvent onTestFail;

        public UnityEvent onEnd;
        public void TestPosition()
        {
            int strokeIndex = sceneSM.GetInteger("StrokeCount");
            //idealPosition = wordModel.currentWord.centroids[strokeIndex];
            idealPosition = strokes[strokeIndex].stroke.position;
            inputPosition = Centroid(strokeModel.worldPointList.ToArray());
            Debug.Log("Testing against Stroke ideal position: " + strokes[strokeIndex].stroke.position);

            sceneSM.SetBool("PassPosition", false);
            Vector2 tol = strokes[strokeIndex].tolerance;
            if (InRange(inputPosition, idealPosition, tol))
            {
                Debug.Log("Stroke position test Pass");
                sceneSM.SetBool("PassPosition", true);
                onTestPass.Invoke();
            } else
            {
                Debug.Log("Stroke position test Fail");
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
            return a.x <= b.x + t.x && a.x >= b.x - t.x &&  a.y <= b.y + t.y && a.y >= b.y - t.y;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="worldPoints">input</param>
        /// <returns></returns>
        private Vector2 Centroid(Vector3[] worldPoints)
        {
            float x = 0, y = 0;
            int length = worldPoints.Length;
            for (int i = 0; i < length; i++)
            {
                x += worldPoints[i].x;
                y += worldPoints[i].y;
            }
            x /= length;
            y /= length;
            return new Vector2(x, y);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(inputPosition, 0.05f);
        }

    }

    [System.Serializable]
    public struct StrokePosition
    {
        public Transform stroke;
        public Vector2 tolerance;
    }

}