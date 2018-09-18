using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class BestMatchScore : MonoBehaviour {

    public enum ScoreType { Overall, Positive, Negative, OverallText};

    public UIModel uiModel;

    public ScoreType scoreType;

    private Text text;

	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        switch (scoreType)
        {
            case ScoreType.Overall:
                text.text = uiModel.BestOverallMatchScore.ToString();
                break;
            case ScoreType.OverallText:
                text.text = uiModel.BestOverallMatchText;
                break;
            case ScoreType.Positive:
                text.text = uiModel.bestPositiveMatchScore.ToString();
                break;
            case ScoreType.Negative:
                text.text = uiModel.bestNegativeMatchScore.ToString();
                break;
        }
	}
}
