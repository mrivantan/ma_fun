using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace App.Validation.Intersection { 
    public class IntersectionValidation : MonoBehaviour {

        public UIModel strokeModel;
        public Model.Word.WordModel wordModel;
        public Animator sceneSM;

        // for concurrent additional actions
        public UnityEvent onTestPass;
        public UnityEvent onTestFail;

        public UnityEvent onEnd;

        public void TestIntersection()
        {
            // TODO: fill in the test

            onEnd.Invoke();
        }

    }
}