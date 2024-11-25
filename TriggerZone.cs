using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public EnemyAI enemyAI;

    private void OnTriggerEnter(Collider other)
    {
        //check if the player entered the trigger zone
        if (other.CompareTag("Player"))
        {
            //enable the enemy's movement
            if (enemyAI != null)
            {
                enemyAI.StartMoving();
            }
            else
            {
                Debug.LogError("EnemyAI reference is not assigned in TriggerZone.");
            }
        }
    }
}
