using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsPlayer : MonoBehaviour
{
    AudioSource src;
    public AudioClip tradeCard;

    AudioSource src2;
    public AudioClip musicFile;

    public void Awake()
    {
        src = GetComponent<AudioSource>();
    }
    public void playCardButtonSoundEffect()
    {
        Debug.Log("Playing card sound effect");
        src.clip = tradeCard;
        src.Play();
    }

    public void playMusic()
    {
        Debug.Log("starting music");            
        if(src2 != null)
        {
            src2 = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        }
        src2.clip = musicFile;
        src2.Play();
    }
}
