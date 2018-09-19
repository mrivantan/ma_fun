using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Dollar.One;
[RequireComponent(typeof(UIModel))]
public class UIModelManager : MonoBehaviour {

    public OneDollar oneDollar;
    private UIModel uiModel;

    string lastLoadedStroke, lastLoadedVariant;

    private void Start()
    {
        uiModel = GetComponent<UIModel>();       
    }

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

}
