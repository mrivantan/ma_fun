using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionAnimationManager : MonoBehaviour {

    public Animator sceneSM;

    private List<Animator> strokeAnimators;
    void Start () {
        Init();
	}

    #region APIs for state machine
    public void RunCurrentStrokeInstruction()
    {
        int i = sceneSM.GetInteger("StrokeCount");
        strokeAnimators[i].SetTrigger("ActiveSequence");
    }

    public void RunCurrentStrokeComplete()
    {
        int i = sceneSM.GetInteger("StrokeCount");
        strokeAnimators[i].SetTrigger("CompleteSequence");
    }
    #endregion

    //#region APIs for child animations
    //public void CompleteSequenceCallback()
    //{
    //    sceneSM.SetTrigger("AdvanceState");
    //}
    //#endregion

    #region private methods
    private void Init()
    {
        strokeAnimators = new List<Animator>();
        for (int i = 0; i < transform.childCount; i++)
        {
            Debug.Log("Loading child: " + transform.GetChild(i).name);
            Animator anim = transform.GetChild(i).GetComponent<Animator>();
            strokeAnimators.Add(anim);
        }
        Debug.Log("Stroke animations loaded. Count: " + strokeAnimators.Count);
    }

    #endregion
}
