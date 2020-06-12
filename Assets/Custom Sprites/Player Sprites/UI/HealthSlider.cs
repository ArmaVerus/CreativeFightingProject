using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{

    //Code re-appropriated from Brackeys Healthbar tutorial: https://youtu.be/BLfNP4Sc_iA

    public Slider healthbar; //make a Slider for the healthbar

    public void SetMaxHealth(int health)
    {
        healthbar.maxValue = health; //Sets the value for the maximum HP
        healthbar.value = health; //Sets the value for the current HP.
    }

    public void SetHealth(int health)
    {
        healthbar.value = health; //Does the same as the above, but doesn't reset the Max Health.
    }
}
