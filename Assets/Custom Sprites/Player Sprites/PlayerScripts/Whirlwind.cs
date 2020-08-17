using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whirlwind : MonoBehaviour
{
    public GameObject player;
    public float spinSpeed;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>(); //initialize rigidbody
        rb.velocity = new Vector2(player.GetComponent<PlayerController>().xSpeed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerController>().isSpinning == false)
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
