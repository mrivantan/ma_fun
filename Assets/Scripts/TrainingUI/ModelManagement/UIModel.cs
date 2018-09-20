using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dollar.One;

public class UIModel : MonoBehaviour {

    [Header("Struct from recognizer model")]
    public StrokeMeta currentMeta;
    [Header("Header values of stroke classification")]
    public string strokeName;
    public string strokeLabel;
    public string variant;
    public int variantIndex;
    public string description;
    public int descriptionIndex;
    public Dictionary<string, StrokeMeta> dictionary = new Dictionary<string, StrokeMeta>();
    [Header("Recognition result from testing input")]
    public float bestPositiveMatchScore;
    public float bestNegativeMatchScore;
    public int positiveListCount;
    public int negativeListCount;
    public Point[] strokeInputPoints;
    public Point[] bestPositiveMatchPoints;
    public Point[] bestNegativeMatchPoints;
    public Vector3[] currentInputWorldPoints;

    [HideInInspector]
    public List<Vector2> pointList;
    [HideInInspector]
    public List<Vector3> worldPointList;

    public float BestOverallMatchScore
    {
        get
        {
            return (bestPositiveMatchScore >= bestNegativeMatchScore) ? bestPositiveMatchScore : bestNegativeMatchScore;
        }
    }
    public string BestOverallMatchText
    {
        get
        {
            string result = "Drawing";
            if (bestPositiveMatchScore > bestNegativeMatchScore) result = "Positive";
            else if (bestPositiveMatchScore < bestNegativeMatchScore) result = "Negative";
            return result;
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
public enum HandwritingDisplayType { InputLive, InputRaw, InputGesture, PositiveRaw, PositiveGesture,  NegativeRaw, NegativeGesture}
