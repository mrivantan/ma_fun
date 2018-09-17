using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Dollar.One;

[RequireComponent(typeof(Dropdown))]
public class StrokeSelectionUI : MonoBehaviour {

    public OneDollar oneDollar;

    [Header("Inputs")]
    public Dropdown dropName, dropVariant;

    [Header("Outputs")]
    public Text[] strokeTitles, strokeDescriptors;


    Dictionary<string, StrokeMeta> dictionary;

    private void Start()
    {
        // load preferences of last used stroke for user convenience
        string lastLoadedStroke = PlayerPrefs.GetString("StrokeName", StrokeType.name_H);
        string lastLoadedVariant = PlayerPrefs.GetString("StrokeVariant", StrokeType.variant_straight);

        // get dictionary
        dictionary = oneDollar.model.strokeDictionary;
        // get the datum of the last used stroke
        oneDollar.currentMeta = dictionary[lastLoadedStroke];

        // set list of names on UI
        SetNameOptions(oneDollar.currentMeta, lastLoadedStroke);
        // set list of variants on UI
        UpdateVariantOptions(oneDollar.currentMeta, lastLoadedVariant);

        // load the new model
        oneDollar.InitModel();
    }

    private void OnGUI()
    {
        for (int i = 0; i < strokeTitles.Length; i++)
        {
            strokeTitles[i].text = oneDollar.currentMeta.strokeLabel;
        }
        for (int i = 0; i < strokeDescriptors.Length; i++)
        {
            strokeDescriptors[i].text = oneDollar.currentMeta.description[dropVariant.value];
        }
    }


    #region UI api
    /// <summary>
    /// triggered by stroke name dropdown
    /// </summary>
    public void UpdateStrokeName()
    {
        // get new datum
        oneDollar.currentMeta = dictionary.GetMetaByIndex(dropName.value);
        // set the new stroke name
        PlayerPrefs.SetString("StrokeName", oneDollar.currentMeta.strokeName);
        // set list of variants on UI
        UpdateVariantOptions(oneDollar.currentMeta);

        //load the new model
        oneDollar.InitModel();

    }
    /// <summary>
    /// triggered by stroke variant dropdown
    /// </summary>
    public void UpdateStrokeVariant()
    {
        // save preference
        PlayerPrefs.SetString("StrokeVariant", oneDollar.currentMeta.strokeVariants[dropVariant.value]);
        //load the new model
        oneDollar.InitModel();
    }

    #endregion

    #region private methods
    /// <summary>
    /// set list of names on UI
    /// </summary>
    /// <param name="m">current meta datum</param>
    private void SetNameOptions(StrokeMeta m, string strokeName)
    {
        // set labels
        dropName.ClearOptions();
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>(dictionary.Count);
        int val = 0;
        foreach (KeyValuePair<string, StrokeMeta> kvp in dictionary) { 
            options.Insert(kvp.Value.index, new Dropdown.OptionData { text = kvp.Value.strokeLabel });
            if (kvp.Value.strokeName == strokeName) val = kvp.Value.index;
        }
        dropName.options = options;
        // change selection on dropdown
        dropName.value = val;
        // set the new stroke name
        PlayerPrefs.SetString("StrokeName", oneDollar.currentMeta.strokeName);
    }
    /// <summary>
    /// set list of variants on UI
    /// </summary>
    /// <param name="m">current meta datum</param>
    private void UpdateVariantOptions(StrokeMeta m)
    {
        // set the labels
        string[] variants = m.strokeVariants;
        dropVariant.ClearOptions();
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>(variants.Length);
        for (int i = 0; i < variants.Length; i++)
            options.Add(new Dropdown.OptionData { text = variants[i] });
        dropVariant.options = options;
        // change selection on dorpdown to first option
        dropVariant.value = 0;
        // save preference
        PlayerPrefs.SetString("StrokeVariant", oneDollar.currentMeta.strokeVariants[0]);
    }
    private void UpdateVariantOptions(StrokeMeta m, string variant)
    {
        // set the labels
        string[] variants = m.strokeVariants;
        dropVariant.ClearOptions();
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>(variants.Length);
        int val = 0;
        for (int i = 0; i < variants.Length; i++)
        {
            options.Add(new Dropdown.OptionData { text = variants[i] });
            if (variants[i] == variant) val = i;
        }
        dropVariant.options = options;
        // change selection on dropdown
        dropVariant.value = val;
        // save preference
        PlayerPrefs.SetString("StrokeVariant", oneDollar.currentMeta.strokeVariants[val]);
    }
    #endregion

}
