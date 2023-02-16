using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmScript : MonoBehaviour
{
    //Script used to play and stop the alarm that can be found in the GOAP section of level 4.
    AudioSource audioSource;
    public bool playerTriggered;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (playerTriggered && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        playerTriggered = true;
    }
}

