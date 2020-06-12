using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    public int maxHealth = 10000; //Target's maximum health
    public int currentHealth; //a Variable to store its current health

    public HealthSlider stageHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        stageHealth.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("Damage");
            LoseHealth(2000);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Hurtbox") //When we collide with something tagged as a "Hurtbox"
        {
            LoseHealth(2000);
        }
    }

    void LoseHealth(int healthLost) //Script to lose HP.
    {
        currentHealth -= healthLost;
        stageHealth.SetHealth(currentHealth); //We need to reduce our current health, and then set the slider value.
    }
}
