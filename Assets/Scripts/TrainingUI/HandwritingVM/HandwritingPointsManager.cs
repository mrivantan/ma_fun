using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Dollar.One;
namespace Strokes.Handwriting
{
    /// <summary>
    /// 
    /// </summary>
    public class HandwritingPointsManager : MonoBehaviour
    {

        public OneDollar oneDollar;
        public UIModel uiModel;
        public RectTransform drawingImage;
        [Tooltip("Distance between each point for drawing")]
        public float intervalDistance;

        [Tooltip("mouse screen position - read only")]
        [SerializeField]
        private Vector2 sPos;
        [Tooltip("mouse world position - read only")]
        [SerializeField]
        private Vector3 wPos;

        private Vector2 prevPointRegistered;
        [HideInInspector]
        public List<Vector2> pointList;
        [HideInInspector]
        public List<Vector3> worldPointList;

        // core actions for handwriting input to work. view and model concerns done in inspector
        public UnityAction initializeDrawing; 
        public UnityAction whileDrawing;
        public UnityAction endDrawing;

        // State for managing input detection
        private bool registering = false;


        private void Awake()
        {
            initializeDrawing += InitializeDrawing;
            whileDrawing += WhileDrawing;
            endDrawing += EndDrawing;

        }

        #region Event APIs

        public void UpdateMousePoint(Vector2 point, Vector3 worldPoint)
        {
            sPos = point;
            wPos = worldPoint;
        }
        #endregion

        

        #region modelAPIs
        public void ClearModelBestNegativeMatchPoints()
        {
            uiModel.bestNegativeMatchPoints = new Point[] { };
            uiModel.negativeListCount = 0;
            uiModel.bestNegativeMatchScore = 0;
        }

        public void SetModelBestNegativeMatchPoints()
        {
            Point[] points = VecToPoint(pointList);
            Result result = oneDollar.Recognize(points, Model.StrokeTypeTest.Negative);
            if(result.Points != null) { 
                uiModel.bestNegativeMatchPoints = result.Points;
                uiModel.negativeListCount = result.Points.Length;
                uiModel.bestNegativeMatchScore = result.Score;
            }
        }

        public void ClearModelBestPositiveMatchPoints()
        {
            uiModel.bestPositiveMatchPoints = new Point[] { };
            uiModel.positiveListCount = 0;
            uiModel.bestPositiveMatchScore = 0;
        }

        public void SetModelBestPositiveMatchPoints()
        {
            Point[] points = VecToPoint(pointList);
            Result result = oneDollar.Recognize(points, Model.StrokeTypeTest.Positive);
            if (result.Points != null)
            {
                uiModel.bestPositiveMatchPoints = result.Points;
                uiModel.positiveListCount = result.Points.Length;
                uiModel.bestPositiveMatchScore = result.Score;
            }
        }

        public void ClearModelInputPoints()
        {
            uiModel.strokeInputPoints = new Point[] { };
        }
        public void SetModelInputPoints()
        {
            uiModel.strokeInputPoints = VecToPoint(pointList);
        }

        public void ClearModelInputWorldPoints()
        {
            uiModel.currentInputWorldPoints = new Vector3[] { };
        }
        public void SetModelInputWorldPoints()
        {
            uiModel.currentInputWorldPoints = worldPointList.ToArray();
        }

        #endregion


        #region guiAPIs
        public void AddGestureToPositiveList()
        {
            Point[] points = VecToPoint(pointList);
            oneDollar.AddGesture(points, StrokeType.test_positive, EvaluationType.None); // add RAW points
                                                                                         //Debug.Log("PositiveList Length: " + oneDollar.model.positiveList.Count);

        }

        public void AddGestureToNegativeList()
        {
            Point[] points = VecToPoint(pointList);
            oneDollar.AddGesture(points, StrokeType.test_negative, EvaluationType.None);
            //Debug.Log("NegativeList Length: " + oneDollar.model.negativeList.Count);
        }

        public void ResetPositiveSet()
        {
            oneDollar.DeleteAllGestures(StrokeType.test_positive);
        }
        public void ResetNegativeSet()
        {
            oneDollar.DeleteAllGestures(StrokeType.test_negative);
        }
        #endregion

        #region Core Event Actions
        private void InitializeDrawing()
        {
            if (registering) return;
            registering = true;
            ClearPointList();
            AddPointToList();
            UpdatePrevPoint();
        }
        private void WhileDrawing()
        {
            if (!registering) return;
            if (Vector2.Distance(sPos, prevPointRegistered) < intervalDistance) return;
            AddPointToList();
            UpdatePrevPoint();
        }

        private void EndDrawing()
        {
            AddPointToList();
            registering = false;

        }
        #endregion

        #region Private functions

        private Point[] VecToPoint(List<Vector2> vectors)
        {
            Point[] result = new Point[vectors.Count];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new Point(vectors[i].x, vectors[i].y);
            }
            return result;
        }

        private void UpdatePrevPoint()
        {
            prevPointRegistered = sPos;
        }

        private void AddPointToList()
        {
            pointList.Add(sPos);
            worldPointList.Add(wPos);
        }

        private void ClearPointList()
        {
            pointList.Clear();
            worldPointList.Clear();
        }


        #endregion
    }
}
