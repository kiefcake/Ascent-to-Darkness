using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flashlight : MonoBehaviour
{
    public GameObject flashlight;

    //audio sources for turning on and off the flashlight
    public AudioSource turnOn;
    public AudioSource turnOff;

    //slider to show the remaining flashlight time
    public Slider flashlightSlider;

    private bool on;
    private bool off;

    //manage flashlight time and recharge
    private float flashlightTimeLeft;
    private const float maxFlashlightTime = 10f;
    private bool recharging;

    void Start()
    {
        //initialize the flashlight as off with a full charge
        off = true;
        on = false;
        recharging = false;
        flashlight.SetActive(false);
        flashlightTimeLeft = maxFlashlightTime;

        //set up the UI slider
        flashlightSlider.maxValue = maxFlashlightTime;
        flashlightSlider.value = maxFlashlightTime;
    }


    void Update()
    {
        //if the flashlight is off and the "F" key is pressed, turn it on
        if (off && Input.GetButtonDown("F") && flashlightTimeLeft > 0)
        {
            TurnOnFlashlight();
        }
        //if the flashlight is on and the "F" key is pressed, turn it off
        else if (on && Input.GetButtonDown("F"))
        {
            TurnOffFlashlight();
        }

        //if the flashlight is on, decrease the remaining time
        if (on)
        {
            flashlightTimeLeft -= Time.deltaTime;
            if (flashlightTimeLeft <= 0)
            {
                flashlightTimeLeft = 0;
                TurnOffFlashlight();
            }
        }
        //if the flashlight is off, recharge it
        else
        {
            RechargeFlashlight();
        }


        flashlightSlider.value = flashlightTimeLeft;
    }


    private void TurnOnFlashlight()
    {
        flashlight.SetActive(true);
        turnOn.Play();
        off = false;
        on = true;
        recharging = false;
    }

    //method to turn off the flashlight
    private void TurnOffFlashlight()
    {
        flashlight.SetActive(false);
        turnOff.Play();
        off = true;
        on = false;
        recharging = true;
    }

    //method to recharge the flashlight over time
    private void RechargeFlashlight()
    {
        flashlightTimeLeft += Time.deltaTime;
        if (flashlightTimeLeft >= maxFlashlightTime)
        {
            flashlightTimeLeft = maxFlashlightTime;
            recharging = false;
        }
    }
}
