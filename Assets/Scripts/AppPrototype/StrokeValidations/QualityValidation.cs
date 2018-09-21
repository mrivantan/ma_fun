using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace App.Validation.Quality
{ 
    public class QualityValidation : MonoBehaviour {
        public UIModel strokeModel;
        public Model.Word.WordModel wordModel;
        public Animator sceneSM;

        // for concurrent additional actions
        public UnityEvent onTestPass;
        public UnityEvent onTestFail;

        public UnityEvent onEnd;

        public void TestQuality()
        {
            string testResult = strokeModel.BestOverallMatchText;
            sceneSM.SetBool("PassQuality", false);
            if (testResult.ToLower() == Dollar.One.StrokeType.test_positive)
            {
                
                sceneSM.SetBool("PassQuality", true);
                onTestPass.Invoke();
            }
            else
            {
                Debug.Log("Stroke quality test Fail");
                onTestFail.Invoke();
            }
            Debug.Log("Stroke quality test result: " + testResult);
            onEnd.Invoke();

        }
	    

    }
}