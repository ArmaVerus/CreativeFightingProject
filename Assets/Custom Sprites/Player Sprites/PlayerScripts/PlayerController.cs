using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;
    private Rigidbody2D rb;
    private Vector2 velocity;
    bool rightJump = false;
    bool leftJump = false;
    bool stopChecker = true; //something to check isOnFloor against.
    public bool isOnFloor = false; //check if the player is on the floor.
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //Initialize the Physics object.
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    void FixedUpdate() //All the command functions will be nested here, FixedUpdate is better for Physics
    {
        if (isOnFloor == true)
        {
            resetJump();
            Jump();
            Move();
        }
        else if (isOnFloor == false)
        {
            maintainJump();
        }
    }

    void Jump()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rb.velocity = Vector2.up * jumpSpeed;
                rightJump = true;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                rb.velocity = Vector2.up * jumpSpeed;
                leftJump = true;
            }
            else
            {
                rb.velocity = Vector2.up * jumpSpeed;
            }
            
        }
        
    }

    void Move()
    {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        
    }

    void resetJump()
    {
        if (rightJump == true)
        {
            rightJump = false;
        }
        else if (leftJump == true)
        {
            leftJump = false;
        }
    }

    void maintainJump()
    {
        if (rightJump == true)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else if (leftJump == true)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
    }
}
