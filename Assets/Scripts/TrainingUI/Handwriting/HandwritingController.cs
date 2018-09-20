using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Strokes.Handwriting { 
    /// <summary>
    /// 
    /// </summary>
    [RequireComponent(typeof(HandwritingPointsManager))]
    public class HandwritingController : MonoBehaviour {
        [Header("Drawing events triggered by mouse input")]
        public UnityEvent onStartDrawing;
        public UnityEvent onDrawing;
        public UnityEvent onEndDrawing;
        [Header("To replace with touch input in future")]
        public VectorUpdateEvent onUpdate;

        HandwritingPointsManager manager;
        private void Start()
        {
            manager = GetComponent<HandwritingPointsManager>();
            onStartDrawing.AddListener(manager.initializeDrawing);
            onDrawing.AddListener(manager.whileDrawing);
            onEndDrawing.AddListener(manager.endDrawing);
        }


    }

    [System.Serializable]
    public class VectorUpdateEvent : UnityEvent<Vector2, Vector3> { }
}


