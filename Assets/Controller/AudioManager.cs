using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
	// Use this for initialization
	void Start ()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (AppManager.IsUnpaused)
            audioSource.UnPause();
        else
            audioSource.Pause();
	}
}
