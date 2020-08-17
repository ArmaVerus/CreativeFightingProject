using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject stageController;

    public int currentStage;

    public float speed; //the speed in which the character moves.

    public float jumpSpeed; //the speed in which the player jumps.

    private Rigidbody2D rb; //the rigidbody initialization

    private Animator anim; //initializing the animations

    public bool rightJump = false; //Are we jumping right?

    public bool leftJump = false; //Are we jumping left? these are needed for handling the velocity scripts

    public bool isOnFloor = false; //check if the player is on the floor.

    public bool isStuck = false; //a checker if the player is stuck or will be stuck

    public bool isRising = false;

    public bool isJumping = false;

    public bool isSpinning = false;

    float startTime;

    public GameObject fireBall; //initializing the fireball projectile, which our player can throw

    public GameObject firePunch;

    public GameObject whirlwind;

    float timeStamp = 0.0f; //Collecting a timestamp of the time, this will be useful for logging difference in time as Time.time never resets.

    int coolDown = 0; //a Cooldown timer so that we can't cover the screen with fireballs

    int frameCount = 0; //a Framecounter so that an animation has adequate time to play

    string currentString;

    public float yPos;

    public float ySpeed;

    public float xSpeed;

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
        stageController = GameObject.Find("StageController");
        currentStage = stageController.GetComponent<StageControl>().Stage;
        rb = GetComponent<Rigidbody2D>();//Initialize the Physics object.
        anim = GetComponent<Animator>(); //Initialize the animator.
        yPos = rb.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        setAnimations();//Set the animations for these as they need to be checked every frame.
        ySpeed = rb.velocity.y;
        xSpeed = rb.velocity.x; //need to store these variables as the variables themselves are private and I need to access them.
        if (isOnFloor == true && !isSpinning)
        {
            if (currentStage == 1)
            {
                throwFireball(); //Can only throw fireballs when they're true, Fireballs are not physics based, and inputs should be based on frame.
            }
            if (currentStage == 2)
            {
                throwUppercut();
            }
            if (currentStage == 3)
            {
                throwWhirlwind();
            }
        }
        handleCooldown(); //this handles the cooldown for abilities after they've been used
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
        ySpeed = rb.velocity.y;
        if (isOnFloor == true && !isSpinning)
        {
            resetJump(); //to offset boolean values for jumping when 
            Jump();
            Move();

        }
        else if (isSpinning)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            checkAirTime();
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

    void setAnimations()
    {
        anim.SetBool("isSpinning", isSpinning);
        anim.SetBool("isRising", isRising);
        anim.SetBool("isJumping", isJumping);
        anim.SetFloat("speed", rb.velocity.x);
        anim.SetFloat("jumpSpeed", rb.velocity.y);
    }

    void createFireball()
    {
        anim.SetBool("isFiring", true);
        GameObject f = Instantiate(fireBall) as GameObject; //create the Fireball
        f.transform.position = new Vector2(rb.position.x + 3.0f, 1.39f); //adjust the Fireball's position
    }

    void createUppercut()
    {
        isRising = true;
        rb.velocity = Vector2.up * jumpSpeed;
        rightJump = true;
        GameObject u = Instantiate(firePunch) as GameObject;
        u.transform.position = new Vector2(rb.position.x + 2.5f, rb.position.y + 3.0f);
    }

    void checkAirTime()
    {
        if (Time.time - startTime > 1.0f)
        {
            isSpinning = false;
        }
    }

    void createWhirlwind()
    {
        xSpeed = speed;
        startTime = Time.time;
        isSpinning = true;
        GameObject w = Instantiate(whirlwind) as GameObject;
        w.transform.position = new Vector2(rb.position.x, rb.position.y);
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

    void throwUppercut() //The code for throwing a fireball.
    {
        if (Input.GetKey(KeyCode.F) && coolDown == 0)
        {
            if ((commandArray[0] == 6 && //Checking if all inputs in the array are in the right order.
             commandArray[1] == 2 &&
             commandArray[2] == 3))
            {
                createUppercut();
                clearArray(); //Clear out the array so more Fireballs don't spawn for free.
                coolDown = 2; //Set Cooldown Timer
                frameCount = 8; //Set Frame counter
            }
        }

    }

    void throwWhirlwind() //The code for throwing a fireball.
    {
        if (Input.GetKey(KeyCode.G) && coolDown == 0)
        {
            if ((commandArray[0] == 2 && //Checking if all inputs in the array are in the right order.
             commandArray[1] == 1 &&
             commandArray[2] == 4))
            {
                createWhirlwind();
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
            isJumping = true;
            if (Input.GetKey(KeyCode.RightArrow)) //Of course, in fighting games we can jump left or right so we have parameters for that too.
            {
                arrayLog(9);
                rb.velocity = Vector2.up * jumpSpeed;
                rightJump = true;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                arrayLog(7);
                rb.velocity = Vector2.up * jumpSpeed;
                leftJump = true;
            }
            else
            {
                arrayLog(8);
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
                arrayLog(3);
            }
            else if (Input.GetKey(KeyCode.LeftArrow)) //Are we also pressing right?
            {
                arrayLog(1);
            }
            else
            {
                arrayLog(2);
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow)) //Check if we're pressing left.
        {
            arrayLog(4);
            rb.velocity = new Vector2(-speed, rb.velocity.y); //of course we need to move backward, so we use -speed here.
        }
        else if (Input.GetKey(KeyCode.RightArrow)) //Check if we're pressing right
        {
            arrayLog(6);
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
            Debug.Log("I'm here guys");
            rb.velocity = new Vector2(speed, rb.velocity.y); //We maintain a constant speed, in fighting games you can't back out of a jumping direction.
            //hence the need for variables, for which jump is being used.
        }
        else if (leftJump == true)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
    }

    void arrayLog(int x)
    {
        if (!dupChecker(x))
        {
            refreshArray(x); //Log "6" which is the 1-9 model of looking right into the commandArray
        }
        currentInput = x;
    }
}
