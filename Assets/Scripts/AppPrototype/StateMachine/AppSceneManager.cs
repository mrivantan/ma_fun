using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App { 
public class AppSceneManager : MonoBehaviour {
        private Animator anim;
        public UIModelController strokeModelController;
        public Model.Word.WordModel wordModel;
        public Model.Word.WordModelManager wordModelManager;

        private float strokeCompletedGaugeNormal;
	   public void Init()
        {
            anim = GetComponent<Animator>();
            anim.SetFloat("StrokesCompletedGauge", 0f);
            
        }

        public void UpdateStroke()
        {
            int i = anim.GetInteger("StrokeCount");
            Debug.Log(wordModel.currentWord.strokes[i]);
            strokeModelController.OnChangeStroke(wordModel.currentWord.strokes[i], wordModel.currentWord.variants[i]);
            strokeCompletedGaugeNormal = 1f / (float)wordModel.currentWord.strokes.Length;
            Debug.Log("strokeCompletedGaugeNormal: 1 / " + wordModel.currentWord.strokes.Length);
        }

        public void IncreaseStrokeCount()
        {
            int i = anim.GetInteger("StrokeCount");
            i++;
            anim.SetInteger("StrokeCount", i);
        }

        public void IncreaseStrokeGauge()
        {
            float f = anim.GetFloat("StrokesCompletedGauge");
            f += strokeCompletedGaugeNormal;
            anim.SetFloat("StrokesCompletedGauge", f);
        }

        public void ResetCheck_Quality()
        {
            anim.SetBool("PassQuality", false);
        }
        public void ResetCheck_Position()
        {
            anim.SetBool("PassPosition", false);
        }
        public void ResetCheck_Size()
        {
            anim.SetBool("PassSize", false);
        }

        public void AdvanceState()
        {
            anim.SetTrigger("AdvanceState");
        }

    }
}
