using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed; //the speed in which the character moves.

    public float jumpSpeed; //the speed in which the player jumps.

    private Rigidbody2D rb; //the rigidbody initialization

    private Animator anim; //initializing the animations

    public bool rightJump = false; //Are we jumping right?

    public bool leftJump = false; //Are we jumping left? these are needed for handling the velocity scripts

    public bool isOnFloor = false; //check if the player is on the floor.

    public bool isStuck = false; //a checker if the player is stuck or will be stuck

    public GameObject fireBall; //initializing the fireball projectile, which our player can throw

    float timeStamp = 0.0f; //Collecting a timestamp of the time, this will be useful for logging difference in time as Time.time never resets.

    int coolDown = 0; //a Cooldown timer so that we can't cover the screen with fireballs

    int frameCount = 0; //a Framecounter so that an animation has adequate time to play

    static int[] fireballSequence = new int[4] { 2, 3, 6, 10 }; //Attempting new sequence, trying to match up inputs into Fireball.

    static string fireBallString = "236F";

    string currentString;

    int fireBallIndex = 0;

    float lastInputTime = 0;

    float acceptableTime = 100.0f;

    public float yPos;

    float elapsedTime = 0;

    int currentInput = 0;

    int[] commandArray = new int[3] { 5, 5, 5 };/*initializing an array for command checking, this is similar to a system I used for a previous assignment on a
    controller, I am re-applying it here as it was a very effective way of input checking by asserting whether or not said commands were ran, I believe since
    the array is fixed to a specific set of memory, I won't incur any memory leaks as a result.
    The code has been re-appropriated for this project however, normally it would have been a standalone checker but I had to change quite a few things for it.
    The original project for that is here if you wish to compare: https://learn.gold.ac.uk/mod/assign/view.php?id=730834
    [7][8][9]
    [4][5][6]
    [1][2][3]
    ^ This is the model used for cardinal directions in fighting games and is the model I use to check which directions are stored in the commandArray. 
    It may be more appropriate to also use an int key for certain buttons given the need for ints.


    F Key = 10
     */


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();//Initialize the Physics object.
        anim = GetComponent<Animator>(); //Initialize the animator.
        yPos = rb.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isJumping", !isOnFloor); //Set the animations for these as they need to be checked every frame.
        anim.SetFloat("speed", rb.velocity.x);
        if (isOnFloor == true)
        {
            throwFireball(); //Can only throw fireballs when they're true, Fireballs are not physics based, and inputs should be based on frame.
            //checkFireball();

        }
        handleCooldown(); //this handles the cooldown for the fireball after its been thrown.
        if (frameCount == 0)
        {
            anim.SetBool("isFiring", false); //Because the animation is so quick and we don't want it repeating I had to put a frame limiter on the animation.
        }
        else
        {
            frameCount -= 1;
        }
    }

    void FixedUpdate() //All the command functions will be nested here, FixedUpdate is better for Physics
    {
        if (isOnFloor == true)
        {
            resetJump(); //to offset boolean values for jumping when 
            Jump();
            Move();

        }
        else if (isOnFloor == false)
        {
            if (isStuck)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            else
            {
                maintainJump();
            }
            //this code is designed to handle the jumping physics while in the air.
            //positionCorrect();
        }
    }

    void refreshArray(int c) //refresh the command array when a value is added
    {
        for (int i = 0; i < 2; i++)
        {
            commandArray[i] = commandArray[i + 1];
        }
        commandArray[2] = c;
    }

    void clearArray() //Clear the commandArray so I can't hit a button twice to get the same special command
    {
        for (int i = 0; i < 3; i++)
        {
            commandArray[i] = 5; //set commandArray as if no prior directions pushed
        }
    }

    //void checkFireballString()
    //{

    //}

    void checkFireball()
    {

        if (fireBallIndex == 0 || Time.time - lastInputTime < acceptableTime)
        {

            if (currentInput == fireballSequence[fireBallIndex])
            {

                fireBallIndex++;
                lastInputTime = Time.time;
                if (fireBallIndex == fireballSequence.Length)
                {
                    createFireball();
                    fireBallIndex = 0;

                }
                Debug.Log(fireBallIndex);

            }
            else
            {
                fireBallIndex = 0;
            }
        }
    }

    void createFireball()
    {
        anim.SetBool("isFiring", true);
        GameObject f = Instantiate(fireBall) as GameObject; //create the Fireball
        f.transform.position = new Vector2(rb.position.x + 3.0f, 1.39f); //adjust the Fireball's position
    }

    void throwFireball() //The code for throwing a fireball.
    {
        if (Input.GetKey(KeyCode.F) && coolDown == 0)
        {
            if ((commandArray[0] == 2 && //Checking if all inputs in the array are in the right order.
             commandArray[1] == 3 &&
             commandArray[2] == 6))
            {
                createFireball();
                clearArray(); //Clear out the array so more Fireballs don't spawn for free.
                coolDown = 2; //Set Cooldown Timer
                frameCount = 8; //Set Frame counter
            }
        }

    }

    void handleCooldown() //a function for determining cooldown functionality.
    {
        if (coolDown > 0 && Time.time - timeStamp > 1) /*The timeStamp logs the time, but only changes when Time is more than a second away
                                                         so this function simulates the cooldown in actual sections*/
        {
            coolDown -= 1;
            timeStamp = Time.time;
        }
    }

    bool dupChecker(int x) //this program checks for duplicate commands in the array, if it finds a command already in the array, it will be true.
    {                      //this is useful for when we want to do things like complex motions, because without this, too many of the same input would be read 
        bool isDupe = false; //making it super hard to do a complex input like a quarter circle forward.
        for (int i = 0; i < 3; i++)
        {
            if (commandArray[i] == x)
            {
                isDupe = true;
            }
        }
        return isDupe;
    }

    void Jump() //Simple, this is my code for jumping.
    {
        if (Input.GetKey(KeyCode.UpArrow)) //Jumping is done by pressing the Up arrow
        {
            if (Input.GetKey(KeyCode.RightArrow)) //Of course, in fighting games we can jump left or right so we have parameters for that too.
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

    void Move() //Handling the moving logic for the player
    {
        if (Input.GetKey(KeyCode.DownArrow)) //Is the player pressing down? While this doesn't beget an action its necessary for Fireballs.
        {
            if (Input.GetKey(KeyCode.RightArrow)) //Are we also pressing right?
            {
                if (!dupChecker(3))
                {
                    refreshArray(3); //Enter 3 into the commandArray
                }
                currentInput = 3;
            }
            else
            {
                if (!dupChecker(2))
                {
                    refreshArray(2); //Otherwise enter 2 into the commandArray
                }
                currentInput = 2;
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow)) //Check if we're pressing left.
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y); //of course we need to move backward, so we use -speed here.
        }
        else if (Input.GetKey(KeyCode.RightArrow)) //Check if we're pressing right
        {
            if (!dupChecker(6))
            {
                refreshArray(6); //Log "6" which is the 1-9 model of looking right into the commandArray
            }
            currentInput = 6;
            rb.velocity = new Vector2(speed, rb.velocity.y); //Set the speed for us moving.
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y); //Of course when we're not clicking either direction we should stop moving
            refreshArray(5); //And since our directions will be in neutral, we'll return 5, this needs to be here regardless of the dupChecker
            //Because otherwise the commands will get stuck and we'll never be able to get reliable fireballs after the first.
        }
        
    }

    void resetJump() //This lets me offset the values for jumping as I do not need them while I am on the ground, it's also important for maintaining jump direction.
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
        if (rightJump == true) //So while we're jumping right.
        {
            rb.velocity = new Vector2(speed, rb.velocity.y); //We maintain a constant speed, in fighting games you can't back out of a jumping direction.
            //hence the need for variables, for which jump is being used.
        }
        else if (leftJump == true)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
    }

    void letterKeyOperations()
    {
        if (Input.GetKey(KeyCode.F))
        {
            currentInput = 10;
        }
    }

    //void positionCorrect()
    //{
    //    if (isStuck)
    //    {
    //        rb.transform.position = new Vector2(rb.position.x + 0.01f, rb.position.y);
    //    }
    //}
}
