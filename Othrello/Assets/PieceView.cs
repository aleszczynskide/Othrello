using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceView : MonoBehaviour
{
    AudioSource audioData;
    internal Animator anim;
    public bool WhiteColor;
    void Start()
    {
        audioData = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }
   public void PlaySound()
    {
        audioData.Play();
    }
}
