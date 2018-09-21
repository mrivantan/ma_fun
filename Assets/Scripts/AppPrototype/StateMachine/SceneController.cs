using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace App { 
    public class SceneController : MonoBehaviour {

        public UnityEvent onInit;
        public UnityEvent onIntroduction;
        public UnityEvent onStrokeInstruction;
        [Header("Update models")]
        public UnityEvent onLoadTestModel;
        public UnityEvent onWatchingInput;
        [Header("Testing Input validators")]
        public UnityEvent onTestInput_Quality;
        public UnityEvent onTestInput_Position;
        public UnityEvent onTestInput_Size;
        public UnityEvent onTestInput_Intersection;
        [Header("Fail Feedback Animations")]
        public UnityEvent onFailFeedback_Quality;
        public UnityEvent onFailFeedback_Position;
        public UnityEvent onFailFeedback_Size;
        public UnityEvent onFailFeedback_Intersection;
        [Header("Update StateMachine params")]
        public UnityEvent onRollbackStateParams;
        public UnityEvent onAdvanceStateParams;
        [Header("Pass Feedback Animations")]
        public UnityEvent onPassFeedback_Stroke;
        public UnityEvent onPassFeedback_Word;
        [Header("Loading additional UI")]
        public UnityEvent onLoadUI_SceneManagement;

        public UnityAction RunTest;

        void Start () {











            onInit.Invoke();
	    }
	

	    void Update () {
		
	    }



    }
}
