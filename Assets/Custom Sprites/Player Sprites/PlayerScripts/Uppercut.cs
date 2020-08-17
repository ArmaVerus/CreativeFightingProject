using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uppercut : MonoBehaviour
{
    public GameObject player;
    public float fistSpeedX;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>(); //initialize rigidbody
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(fistSpeedX, player.GetComponent<PlayerController>().ySpeed);
        if (player.GetComponent<PlayerController>().ySpeed < 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Hurtbox")
        {
            Destroy(this.gameObject);
        }
    }
}
