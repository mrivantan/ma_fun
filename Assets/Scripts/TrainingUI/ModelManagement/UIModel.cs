using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dollar.One;

public class UIModel : MonoBehaviour {

    public StrokeMeta currentMeta;

    public string strokeName;
    public string strokeLabel;
    public string variant;
    public int variantIndex;
    public string description;
    public int descriptionIndex;
    //public string test;
    public float bestPositiveMatchScore;
    public float bestNegativeMatchScore;
    public int positiveListCount;
    public int negativeListCount;
    public Point[] strokeInputPoints;
    public Point[] bestPositiveMatchPoints;
    public Point[] bestNegativeMatchPoints;

    public Vector3[] currentInputWorldPoints;


    public float BestOverallMatchScore
    {
        get
        {
            return (bestPositiveMatchScore >= bestNegativeMatchScore) ? bestPositiveMatchScore : bestNegativeMatchScore;
        }
    }

    public Point[] BestOverallMatchPoints
    {
        get
        {
            return (bestPositiveMatchScore >= bestNegativeMatchScore) ? bestPositiveMatchPoints : bestNegativeMatchPoints;
        }
    }

    public string TestResult
    {
        get
        {
            string result = "";
            if (bestPositiveMatchScore > bestNegativeMatchScore) result = StrokeType.test_positive;
            else if (bestPositiveMatchScore < bestNegativeMatchScore) result = StrokeType.test_negative;
            return result;
        }
    }

}
[System.Serializable]
public enum HandwritingDisplayType { InputLive, InputResult, MatchPositive, MatchNegative}
