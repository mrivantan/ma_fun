using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FailText : MonoBehaviour {

    public float displayDuraiton;
    public UnityEvent SequenceCallback;
    private Text text;

	void Start () {
        text = GetComponent<Text>();
        ClearMessage();
	}

    public void ClearMessage()
    {
        text.text = "";
    }

    public void FailQuality()
    {
        StartCoroutine(FailQualitySequence());
    }

    private IEnumerator FailQualitySequence()
    {
        text.text = "Fail Quality";
        yield return new WaitForSeconds(displayDuraiton);
        ClearMessage();
        SequenceCallback.Invoke();
    }

    public void FailPosition()
    {
        StartCoroutine(FailPositionSequence());
    }
    private IEnumerator FailPositionSequence()
    {
        text.text = "Fail Position";
        yield return new WaitForSeconds(displayDuraiton);
        ClearMessage();
        SequenceCallback.Invoke();
    }

    public void FailSize()
    {
        StartCoroutine(FailSizeSequence());
    }
    private IEnumerator FailSizeSequence()
    {
        text.text = "Fail Size";
        yield return new WaitForSeconds(displayDuraiton);
        ClearMessage();
        SequenceCallback.Invoke();
    }

}
