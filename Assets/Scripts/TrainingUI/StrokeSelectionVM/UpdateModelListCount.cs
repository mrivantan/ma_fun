using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateModelListCount : MonoBehaviour {

    public Dollar.One.OneDollar oneDollar;

    public Text[] positiveListCountLabels, negativeListCountLabels;

    Dollar.One.Model model;

	void Start () {
        model = oneDollar.model;
	}

    private void OnGUI()
    {
        for (int i = 0; i < positiveListCountLabels.Length; i++)
        {
            positiveListCountLabels[i].text = model.positiveList.Count.ToString();
        }
        for (int i = 0; i < negativeListCountLabels.Length; i++)
        {
            negativeListCountLabels[i].text = model.negativeList.Count.ToString();
        }
    }

}
