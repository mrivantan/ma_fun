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
        public UIModelManager modelManager;
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
        //[HideInInspector]
        //public List<Vector2> pointList;
        //[HideInInspector]
        //public List<Vector3> worldPointList;

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

        
        #region Core Event Actions
        private void InitializeDrawing()
        {
            if (registering) return;
            registering = true;
            modelManager.ClearPointList();
            modelManager.AddPointToList(sPos, wPos);
            UpdatePrevPoint();
        }
        private void WhileDrawing()
        {
            if (!registering) return;
            if (Vector2.Distance(sPos, prevPointRegistered) < intervalDistance) return;
            modelManager.AddPointToList(sPos, wPos);
            UpdatePrevPoint();
        }

        private void EndDrawing()
        {
            modelManager.AddPointToList(sPos, wPos);
            registering = false;

        }
        #endregion

        #region Private functions
        

        private void UpdatePrevPoint()
        {
            prevPointRegistered = sPos;
        }

        #endregion
    }
}
