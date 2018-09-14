using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ToggleVisibleUI : MonoBehaviour {
    [Header("More Options UI")]
    public Button toggleButton;
    public GameObject hideablePanel;

    private void Start()
    {
        toggleButton.onClick.AddListener(TogglePanel);
    }


    public void TogglePanel()
    {
        hideablePanel.SetActive(!hideablePanel.activeSelf);
    }

}
