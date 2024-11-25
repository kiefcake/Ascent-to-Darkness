using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    //called when another collider enters the trigger zone of the orb
    private void OnTriggerEnter(Collider other)
    {
        //attempt to get the PlayerInventory component from the collider's GameObject
        PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();

        //check if the PlayerInventory component is found
        if (playerInventory != null)
        {
            //notify the PlayerInventory that an orb has been collected
            playerInventory.OrbCollected();

            //deactivate the orb game object
            gameObject.SetActive(false);
        }
    }
}