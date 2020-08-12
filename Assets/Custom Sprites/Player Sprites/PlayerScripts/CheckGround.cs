using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    GameObject playerDetail;
    PlayerController parentPlayer;
    float groundYPos;

    // Start is called before the first frame update
    void Start()
    {
        playerDetail = gameObject.transform.parent.gameObject; //initialize the gameObject
        parentPlayer = playerDetail.GetComponent<PlayerController>();
        groundYPos = parentPlayer.yPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision) //When it comes in contact with a collider
    {
        if (collision.collider.tag == "Floor") //If that Collider's tagged as being part of the floor
        {
            playerDetail.GetComponent<PlayerController>().isOnFloor = true; //State that we are now on the floor.
        }
        //else if (collision.collider.tag == "TargetHitbox")
        //{
        //    float currentYPos = playerDetail.GetComponent<PlayerController>().yPos;
        //    if (currentYPos > groundYPos)
        //    {
        //        playerDetail.GetComponent<PlayerController>().isOnFloor = true;
        //        playerDetail.GetComponent<PlayerController>().leftJump = false;
        //        playerDetail.GetComponent<PlayerController>().rightJump = false;
        //    }

        //}
    }

    private void OnCollisionExit2D(Collision2D collision) //When the collider leaves contact with a collider
    {
        if (collision.collider.tag == "Floor") //If the collider we separated from was the floor
        {
            playerDetail.GetComponent<PlayerController>().isOnFloor = false; //We're no longer on the floor
        }
    }

    //void collisionCorrection()
    //{
    //    float currentYPos = playerDetail.GetComponent<PlayerController>().yPos;

    //}
}
