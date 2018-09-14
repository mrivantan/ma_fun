using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIModel : MonoBehaviour {

    public Dollar.One.StrokeMeta currentMeta;

    public string strokeName;
    public string strokeLabel;
    public string variant;
    public int variantIndex;
    public string description;
    public int descriptionIndex;
    public string test;
    public float bestPositiveMatchScore;
    public float bestNegativeMatchScore;
    public int positiveListCount;
    public int negativeIntCount;
    public Dollar.One.Point[] strokeInputPoints;
    public Dollar.One.Point[] bestPositiveMatchPoints;
    public Dollar.One.Point[] bestNegativeMatchPoints;

    public float BestOverallMatchScore
    {
        get
        {
            return (bestPositiveMatchScore >= bestNegativeMatchScore) ? bestPositiveMatchScore : bestNegativeMatchScore;
        }
    }

    public Dollar.One.Point[] BestOverallMatchPoints
    {
        get
        {
            return (bestPositiveMatchScore >= bestNegativeMatchScore) ? bestPositiveMatchPoints : bestNegativeMatchPoints;
        }
    }
}
