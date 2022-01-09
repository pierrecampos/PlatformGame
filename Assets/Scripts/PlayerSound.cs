using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour {

    private AudioSource audioSource;

    public AudioClip hitSound;
    public AudioClip jumpSound;
    public AudioClip coinSound;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySfx(AudioClip sfx) {
        audioSource.PlayOneShot(sfx);
    }

}
