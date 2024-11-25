using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbScript : MonoBehaviour
{
    //AudioClip for the orb collection sound
    public AudioClip orbSound;

    //AudioSource for playing the orb sound
    private AudioSource audioSource;
    private Transform player;

    void Start()
    {
        //AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        //find the player using the "Player" tag
        player = GameObject.FindGameObjectWithTag("Player").transform;

        //set the AudioClip to the AudioSource if both are not null
        if (audioSource != null && orbSound != null)
        {
            audioSource.clip = orbSound;
        }
        else
        {
            //log an error if AudioSource or AudioClip is not set up correctly
            Debug.LogError("AudioSource or AudioClip not set up correctly.");
        }
    }

    void Update()
    {
        //check if the player is not null
        if (player != null)
        {
            //calculate distance between the orb and the player
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            //play the orb sound if the player is within a certain distance and the sound is not already playing
            if (distanceToPlayer <= 5f && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}