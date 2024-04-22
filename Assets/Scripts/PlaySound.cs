using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour {
    public AudioSource audioSource;
    public AudioClip audioClip;

    void Start() {
        audioSource.clip = audioClip;
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    void Update() {
        
    }
}
