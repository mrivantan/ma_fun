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

        private AppSceneManager manager;
        private UnityAction m_Init;
        private UnityAction m_LoadTestModel;
        private UnityAction m_AdvanceStateParams;
        private UnityAction m_RollbackStateParams;
        void Start () {
            manager = GetComponent<AppSceneManager>();
            m_Init += manager.wordModelManager.Init;
            m_Init += manager.Init;
            m_Init += manager.ResetCheck_Quality;
            m_Init += manager.ResetCheck_Position;
            m_Init += manager.ResetCheck_Size;
            m_Init += manager.AdvanceState;
            onInit.AddListener(m_Init);
            onInit.Invoke();

            m_LoadTestModel += manager.UpdateStroke;
            m_LoadTestModel += manager.AdvanceState;
            onLoadTestModel.AddListener(m_LoadTestModel);

            m_AdvanceStateParams += manager.IncreaseStrokeCount;
            m_AdvanceStateParams += manager.IncreaseStrokeGauge;
            m_AdvanceStateParams += manager.ResetCheck_Quality;
            m_AdvanceStateParams += manager.ResetCheck_Position;
            m_AdvanceStateParams += manager.ResetCheck_Size;
            m_AdvanceStateParams += manager.AdvanceState;
            onAdvanceStateParams.AddListener(m_AdvanceStateParams);

            m_RollbackStateParams += manager.ResetCheck_Quality;
            m_RollbackStateParams += manager.ResetCheck_Position;
            m_RollbackStateParams += manager.ResetCheck_Size;
            m_RollbackStateParams += manager.AdvanceState;
            onRollbackStateParams.AddListener(m_RollbackStateParams);

        }

        public void Introduction()
        {
            onIntroduction.Invoke();
        }
        public void LoadTestModel()
        {
            onLoadTestModel.Invoke();
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
            onLoadUI_SceneManagement.Invoke();
        }
    }
}
