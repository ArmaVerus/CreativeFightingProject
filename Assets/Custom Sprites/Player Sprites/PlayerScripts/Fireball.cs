using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float ballSpeed; //How fast to move the fireball
    private Rigidbody2D rb; //The Fireball's rigidbody
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //initialize rigidbody
        rb.velocity = new Vector2(ballSpeed, 0); //keep the fireball in constant motion.
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 40) //When it goes beyond the range of the camera
        {
            Destroy(this.gameObject); //Destroy the prefab, otherwise it will take up computing space.
        }
    }
}
