using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IntroductionManager : MonoBehaviour {
    public Animator sceneSM;
    public Animator pinyinAnim;
    public AudioSource audioSource;
    public AudioClip audioClip;

    private UnityAction playInstructions;

    private void Start()
    {
        playInstructions += _ShowPinyin;
        playInstructions += _PlayAudio;
    }

    public void PlayInstructions()
    {
        playInstructions();
    }

    public void CompleteSequenceCallback()
    {
        sceneSM.SetTrigger("AdvanceState");
    }

    private void _ShowPinyin()
    {
        pinyinAnim.SetTrigger("Show");
    }

    private void _HidePinyin()
    {
        pinyinAnim.SetTrigger("Hide");
    }

    private void _PlayAudio()
    {
        audioSource.PlayOneShot(audioClip);
    }
}
