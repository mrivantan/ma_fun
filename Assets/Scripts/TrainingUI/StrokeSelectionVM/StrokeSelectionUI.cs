using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Dollar.One;

[RequireComponent(typeof(Dropdown))]
public class StrokeSelectionUI : MonoBehaviour {

    //public OneDollar oneDollar;
    public UIModel uiModel;
    public UIModelController uiModelController;


    [Header("Inputs")]
    public Dropdown dropName, dropVariant;

    [Header("Outputs")]
    public Text[] strokeTitles, strokeDescriptors;

    private void Start()
    {
        //// set list of names on UI
        //LoadNameOptions(uiModel.strokeName);
        //// set list of variants on UI
        //LoadVariantOptions(uiModel.currentMeta, uiModel.variantIndex);
    }

    private void OnGUI()
    {
        for (int i = 0; i < strokeTitles.Length; i++)
        {
            strokeTitles[i].text = uiModel.strokeLabel;
        }
        for (int i = 0; i < strokeDescriptors.Length; i++)
        {
            strokeDescriptors[i].text = uiModel.description;
        }
    }


    #region UI api
    /// <summary>
    /// triggered by stroke name dropdown
    /// </summary>
    public void UpdateStrokeName()
    {

        // update the model
        uiModelController.OnChangeStrokeName(dropName.value);
        // set list of variants on UI
        LoadVariantOptions();

    }
    /// <summary>
    /// triggered by stroke variant dropdown
    /// </summary>
    public void UpdateStrokeVariant()
    {
        uiModelController.OnChangeVariant(dropVariant.value);
    }

    #endregion

    #region private methods
    /// <summary>
    /// set list of names on UI
    /// </summary>
    /// <param name="m">current meta datum</param>
    public void LoadNameOptions()
    {
        // set labels
        dropName.ClearOptions();
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>(uiModel.dictionary.Count);
        int val = 0;
        foreach (KeyValuePair<string, StrokeMeta> kvp in uiModel.dictionary) { 
            options.Insert(kvp.Value.index, new Dropdown.OptionData { text = kvp.Value.strokeLabel });
            if (kvp.Value.strokeName == uiModel.strokeName) val = kvp.Value.index;
        }
        dropName.options = options;
        // change selection on dropdown
        dropName.value = val;

    }

    public void LoadVariantOptions()
    {
        // set the labels
        string[] variants = uiModel.currentMeta.strokeVariants;
        dropVariant.ClearOptions();
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>(variants.Length);
        for (int i = 0; i < variants.Length; i++)
            options.Add(new Dropdown.OptionData { text = variants[i] });
        dropVariant.options = options;
        // change selection on dropdown
        dropVariant.value = 0;
    }
    #endregion

}
