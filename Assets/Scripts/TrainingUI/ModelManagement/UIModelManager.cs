using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Dollar.One;
[RequireComponent(typeof(UIModel))]
public class UIModelManager : MonoBehaviour {

    public OneDollar oneDollar;
    public UIModel uiModel;

    string lastLoadedStroke, lastLoadedVariant;

    #region Stroke Headers Management
    public void InitSourceModel()
    {
        // load preferences of last used stroke for user convenience
        lastLoadedStroke = PlayerPrefs.GetString("StrokeName", StrokeType.name_H);
        lastLoadedVariant = PlayerPrefs.GetString("StrokeVariant", StrokeType.variant_straight);

        // get dictionary
        uiModel.dictionary = oneDollar.model.strokeDictionary;
        // get the datum of the last used stroke
        oneDollar.currentMeta = uiModel.dictionary[lastLoadedStroke];
        // load the new model
        oneDollar.model.Init();

    }

    public void ConfigHeaders()
    {
        uiModel.currentMeta = oneDollar.currentMeta;
        uiModel.strokeName = uiModel.currentMeta.strokeName;
        uiModel.strokeLabel = uiModel.currentMeta.strokeLabel;
        uiModel.descriptionIndex = uiModel.variantIndex = StrokeType.VariantNameToIndex(lastLoadedVariant);
        uiModel.variant = uiModel.currentMeta.strokeVariants[uiModel.variantIndex];
        uiModel.description = uiModel.currentMeta.description[uiModel.descriptionIndex];
    }
    public void ConfigHeaders<T>(T any)
    {
        ConfigHeaders();
    }

    public void UpdateStrokeName(int strokeIndex)
    {
        // get new datum
        oneDollar.currentMeta = uiModel.dictionary.GetMetaByIndex(strokeIndex);
        // set the new stroke name
        PlayerPrefs.SetString("StrokeName", oneDollar.currentMeta.strokeName);
        PlayerPrefs.SetString("StrokeVariant", oneDollar.currentMeta.strokeVariants[0]);
        //load the new model
        oneDollar.model.Init();
    }
    public void UpdateStrokeVariant(int variantIndex)
    {
        // save preference
        PlayerPrefs.SetString("StrokeVariant", oneDollar.currentMeta.strokeVariants[variantIndex]);
        //load the new model
        oneDollar.model.Init();
    }
    #endregion

    #region Points Management
    public void ClearModelBestNegativeMatchPoints()
    {
        uiModel.bestNegativeMatchPoints = new Point[] { };
        uiModel.negativeListCount = 0;
        uiModel.bestNegativeMatchScore = 0;
    }
    public void ClearModelBestPositiveMatchPoints()
    {
        uiModel.bestPositiveMatchPoints = new Point[] { };
        uiModel.positiveListCount = 0;
        uiModel.bestPositiveMatchScore = 0;
    }
    public void ClearModelInputPoints()
    {
        uiModel.strokeInputPoints = new Point[] { };
    }
    public void ClearModelInputWorldPoints()
    {
        uiModel.currentInputWorldPoints = new Vector3[] { };
    }

    public void SetModelBestNegativeMatchPoints()
    {
        Point[] points = VecToPoint(uiModel.pointList);
        Result result = oneDollar.Recognize(points, Model.StrokeTypeTest.Negative);
        if (result.Points != null)
        {
            uiModel.bestNegativeMatchPoints = result.Points;
            uiModel.negativeListCount = result.Points.Length;
            uiModel.bestNegativeMatchScore = result.Score;
        }
    }
    public void SetModelBestPositiveMatchPoints()
    {
        Point[] points = VecToPoint(uiModel.pointList);
        Result result = oneDollar.Recognize(points, Model.StrokeTypeTest.Positive);
        if (result.Points != null)
        {
            uiModel.bestPositiveMatchPoints = result.Points;
            uiModel.positiveListCount = result.Points.Length;
            uiModel.bestPositiveMatchScore = result.Score;
        }
    }
    public void SetModelInputPoints()
    {
        uiModel.strokeInputPoints = VecToPoint(uiModel.pointList);
    }
    public void SetModelInputWorldPoints()
    {
        uiModel.currentInputWorldPoints = uiModel.worldPointList.ToArray();
    }

    public void AddPointToList(Vector2 sPos, Vector3 wPos)
    {
        uiModel.pointList.Add(sPos);
        uiModel.worldPointList.Add(wPos);
    }

    public void ClearPointList()
    {
        uiModel.pointList.Clear();
        uiModel.worldPointList.Clear();
    }
    #endregion


    #region guiAPIs
    public void AddGestureToPositiveList()
    {
        Point[] points = uiModel.strokeInputPoints;
        oneDollar.AddGesture(points, StrokeType.test_positive, EvaluationType.None);
    }

    public void AddGestureToNegativeList()
    {
        Point[] points = uiModel.strokeInputPoints;
        oneDollar.AddGesture(points, StrokeType.test_negative, EvaluationType.None);
    }

    public void ResetPositiveSet()
    {
        oneDollar.DeleteAllGestures(StrokeType.test_positive);
    }
    public void ResetNegativeSet()
    {
        oneDollar.DeleteAllGestures(StrokeType.test_negative);
    }
    #endregion

    #region Helper functions
    private Point[] VecToPoint(List<Vector2> vectors)
    {
        Point[] result = new Point[vectors.Count];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = new Point(vectors[i].x, vectors[i].y);
        }
        return result;
    }
    #endregion
}
