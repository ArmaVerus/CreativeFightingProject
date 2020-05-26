using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;
    private Rigidbody2D rb;
    private Vector2 velocity;
    public bool isOnFloor = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), 0);
        velocity = moveInput * speed;
        
        
    }

    void FixedUpdate()
    {
        
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        //rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
        //if (counter < 50)
        //{
        //    counter += 1;
        //}
        //else
        //{
        //    Debug.Log("jump");
        //    counter = 0;
        //    rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
        //}
        Jump();
        if (!isOnFloor)
        {
            rb.AddForce(new Vector2(0, -jumpSpeed/5), ForceMode2D.Impulse);
        }
    }

    void Jump()
    {
        if (Input.GetAxis("Vertical") > 0 && isOnFloor == true)
        {
            Debug.Log("jump");
            rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
        }
        
    }
}
