using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(UIModelManager))]
public class UIModelController : MonoBehaviour {
    public UnityEvent onPostInit;
    public UnityEvent onAddToPositiveSet;
    public UnityEvent onAddToNegativeSet;
    public UnityEvent onRecognize;

    private UIModelManager manager;

    public UnityAction OnInit;
    public UnityAction<int> OnChangeStrokeName, OnChangeVariant;
    public UnityAction AddInputToPositiveSet, AddInputToNegativeSet;
    public UnityAction ClearViewPoints, SetViewPoints;

    // Use this for initialization
    void Start () {
        manager = GetComponent<UIModelManager>();
        OnInit += manager.oneDollar.InitOneDollar;
        OnInit += manager.InitSourceModel;
        OnInit += manager.ConfigHeaders;

        OnInit();
        onPostInit.Invoke();

        OnChangeStrokeName += manager.UpdateStrokeName;
        OnChangeStrokeName += manager.ConfigHeaders;


        OnChangeVariant += manager.UpdateStrokeVariant;
        OnChangeVariant += manager.ConfigHeaders;

        AddInputToPositiveSet += manager.AddGestureToPositiveList;
        onAddToPositiveSet.AddListener(AddInputToPositiveSet);


        AddInputToNegativeSet += manager.AddGestureToNegativeList;
        onAddToNegativeSet.AddListener(AddInputToNegativeSet);

        ClearViewPoints += manager.ClearModelInputPoints;
        ClearViewPoints += manager.ClearModelInputWorldPoints;
        ClearViewPoints += manager.ClearModelBestNegativeMatchPoints;
        ClearViewPoints += manager.ClearModelBestPositiveMatchPoints;

        SetViewPoints += manager.SetModelInputPoints;
        SetViewPoints += manager.SetModelInputWorldPoints;
        SetViewPoints += manager.SetModelBestNegativeMatchPoints;
        SetViewPoints += manager.SetModelBestPositiveMatchPoints;

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

    public void ClearViewPointsAction()
    {
        ClearViewPoints();
    }

    public void SetViewPointsAction()
    {
        SetViewPoints();
    }
}
[System.Serializable]
public class UpdateModelEvent : UnityEvent<int> { }