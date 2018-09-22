using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillStrokeAnimationManager : MonoBehaviour {

    public float delayStart;

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

    public void RunWalkthroughAnimation()
    {
        StartCoroutine(RunAllAnimations());
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
    }

    public void ResetAllAnimations()
    {
        foreach (FillStrokeAnimation anim in strokeAnimations)
            anim.Reset();
    }
}
