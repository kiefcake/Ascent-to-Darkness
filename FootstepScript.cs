using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepScript : MonoBehaviour
{
    public AudioSource footstepAudio;

    void Start()
    {
        //check if the footstep audio source is not null
        if (footstepAudio != null)
        {
            //make sure the audio is stopped initially
            footstepAudio.Stop();
        }
    }

    void Update()
    {
        //checks if the player is moving
        bool isMoving = Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d");

        //plays footsteps if the player is moving and footsteps are not already playing
        if (isMoving && !footstepAudio.isPlaying)
        {
            PlayFootsteps();
        }
        //stops footsteps if the player is not moving and footsteps are playing
        else if (!isMoving && footstepAudio.isPlaying)
        {
            StopFootsteps();
        }
    }


    void PlayFootsteps()
    {
        //checks if the footstep audio source is not null
        if (footstepAudio != null)
        {
            //plays the footstep sound
            footstepAudio.Play();
        }
    }

    void StopFootsteps()
    {
        //checks if the footstep audio source is not null
        if (footstepAudio != null)
        {
            //stops the footstep sound
            footstepAudio.Stop();
        }
    }
}
