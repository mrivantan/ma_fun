using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateModelListCount : MonoBehaviour {

    public UIModel uiModel;

    public Text[] positiveListCountLabels, negativeListCountLabels;

    private void OnGUI()
    {
        for (int i = 0; i < positiveListCountLabels.Length; i++)
        {
            positiveListCountLabels[i].text = uiModel.positiveListCount.ToString();
        }
        for (int i = 0; i < negativeListCountLabels.Length; i++)
        {
            negativeListCountLabels[i].text = uiModel.positiveListCount.ToString();
        }
    }

}
