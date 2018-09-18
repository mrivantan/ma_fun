using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextColorFilter : MonoBehaviour {

    public Text textToTest;
    public Image image;
    public TextColorCombination[] combinations;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        for (int i = 0; i < combinations.Length; i++)
        {
            if(textToTest.text == combinations[i].text)
            {
                image.color = combinations[i].color;
            }
        }

	}

    
}
[System.Serializable]
public struct TextColorCombination
{
    public Color color;
    public string text;
}