using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(UIModelManager))]
public class UIModelController : MonoBehaviour {

    public UnityEvent onAddToPositiveSet;
    public UnityEvent onAddToNegativeSet;
    public UnityEvent onRecognize;

    private UIModelManager manager;

    public UnityAction OnInit;
    public UnityAction<int> OnChangeStrokeName, OnChangeVariant;


    // Use this for initialization
    void Start () {
        manager = GetComponent<UIModelManager>();
        OnInit += manager.InitSourceModel;
        OnInit += manager.ConfigHeaders;
        OnInit();

        OnChangeStrokeName += manager.UpdateStrokeName;
        OnChangeStrokeName += manager.ConfigHeaders;


        OnChangeVariant += manager.UpdateStrokeVariant;
        OnChangeVariant += manager.ConfigHeaders;

	}


    public void OnAddToPositiveSet()
    {
        onAddToPositiveSet.Invoke();
    }

    public void OnAddToNegativeSet()
    {
        onAddToNegativeSet.Invoke();
    }

    public void OnRecognize()
    {
        onRecognize.Invoke();
    }
}
[System.Serializable]
public class UpdateModelEvent : UnityEvent<int> { }