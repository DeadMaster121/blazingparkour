using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySoundButton : MonoBehaviour {
    public Button button; 
    public AudioSource audioSource;
    
    void Start() {
        button.onClick.AddListener(PlaySound);
    }

    void Update() {
    }
    
    // This method will play the audio
    void PlaySound() {
        audioSource.Play();
    }
}
