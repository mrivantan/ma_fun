using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Dollar.One;

[RequireComponent(typeof(UIModel))]
public class UIModelController : MonoBehaviour {

    public UnityEvent onInit;
    public UnityEvent onChangeClassification;
    public UnityEvent onAddToPositiveSet;
    public UnityEvent onAddToNegativeSet;
    public UnityEvent onRecognize;

    private UIModel uiModel;
    // Use this for initialization
    void Start () {
        uiModel = GetComponent<UIModel>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}




}
