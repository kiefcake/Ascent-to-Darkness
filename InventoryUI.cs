using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    private TextMeshProUGUI orbText;
    void Start()
    {

        orbText = GetComponent<TextMeshProUGUI>();
    }

    //update the orb text based on the player's inventory
    public void UpdateOrbText(PlayerInventory playerInventory)
    {
        //sets the text to display the number of orbs in the player's inventory
        orbText.text = playerInventory.NumberOfOrbs.ToString();
    }
}
