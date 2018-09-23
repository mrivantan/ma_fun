using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillStrokeAnimationManager : MonoBehaviour {

    public Animator sceneSM;
    public float delayStart, delayEnd;

    private List<FillStrokeAnimation> strokeAnimations;
	// Use this for initialization
	void Start () {
        Init();
        ResetAllAnimations();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Init()
    {
        strokeAnimations = new List<FillStrokeAnimation>();

        for (int i = 0; i < transform.childCount; i++)
        {
            Debug.Log("Loading child: " + transform.GetChild(i).name);
            FillStrokeAnimation animation = transform.GetChild(i).GetComponentInChildren<FillStrokeAnimation>();
            strokeAnimations.Add(animation);
        }
        Debug.Log("Stroke animations loaded. Count: " + strokeAnimations.Count);
    }

    public void RunCurrentStroke()
    {
        StartCoroutine(RunCurrentStrokeSequence());
    }

    private IEnumerator RunCurrentStrokeSequence()
    {
        int i = sceneSM.GetInteger("StrokeCount");
        strokeAnimations[i].Trigger();
        yield return new WaitForSeconds(strokeAnimations[i].duration);
        sceneSM.SetTrigger("AdvanceState");
    }

    public void RunWalkthroughAnimation()
    {
        StartCoroutine(WalkthroughAnimationSequence());
    }
    private IEnumerator WalkthroughAnimationSequence()
    {
        yield return StartCoroutine(RunAllAnimations());
        yield return StartCoroutine(ResetAllAnimations_E());
    }

    private IEnumerator RunAllAnimations()
    {
        yield return new WaitForSeconds(delayStart);
        foreach (FillStrokeAnimation anim in strokeAnimations)
        {
            Debug.Log("Start animation for stroke: " + anim.transform.parent.name);
            anim.Trigger();
            yield return new WaitForSeconds(anim.duration);
        }
        yield return new WaitForSeconds(delayEnd);
        sceneSM.SetTrigger("AdvanceState");
    }


    private IEnumerator ResetAllAnimations_E()
    {
        foreach (FillStrokeAnimation anim in strokeAnimations)
            anim.Reset();
        yield return null;
    }
    public void ResetAllAnimations()
    {
        foreach (FillStrokeAnimation anim in strokeAnimations)
            anim.Reset();
    }
}
