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

        public void Introduction()
        {
            onIntroduction.Invoke();
        }
        public void LoadTestModel()
        {

        }

        public void StrokeInstruction()
        {
            onStrokeInstruction.Invoke();
        }

        public void WatchingInput()
        {
            onWatchingInput.Invoke();
        }


        public void RollbackStateParams()
        {
            onRollbackStateParams.Invoke();
        }

        public void TestInput_Quality()
        {
            onTestInput_Quality.Invoke();
        }

        public void TestInput_Position()
        {
            onTestInput_Position.Invoke();
        }

        public void TestInput_Size()
        {
            onTestInput_Size.Invoke();
        }

        public void TestInput_Intersection()
        {
            onTestInput_Intersection.Invoke();
        }

        public void FailFeedback_Quality()
        {
            onFailFeedback_Quality.Invoke();
        }

        public void FailFeedback_Position()
        {
            onFailFeedback_Position.Invoke();
        }

        public void FailFeedback_Size()
        {
            onFailFeedback_Size.Invoke();
        }

        public void FailFeedback_Intersection()
        {
            onFailFeedback_Intersection.Invoke();
        }

        public void PassFeedback_Stroke()
        {
            onPassFeedback_Stroke.Invoke();
        }

        public void AdvanceStateParams()
        {
            onAdvanceStateParams.Invoke();
        }

        public void PassFeedback_Word()
        {
            onPassFeedback_Word.Invoke();
        }

        public void LoadUI_SceneManagement()
        {

        }
    }
}
