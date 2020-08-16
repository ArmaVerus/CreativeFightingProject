using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushboxScript : MonoBehaviour
{
    GameObject playerDetail;
    // Start is called before the first frame update
    void Start()
    {
        playerDetail = gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision) //When it comes in contact with a collider
    {
        bool floored = playerDetail.GetComponent<PlayerController>().isOnFloor;
        if ((collision.collider.tag == "Walls" || collision.collider.tag == "TargetHitbox") && !floored) //If that Collider's tagged as being part of the floor
        {
            playerDetail.GetComponent<PlayerController>().isStuck = true; //State that we are now on the floor.
        }
    }

    private void OnCollisionExit2D(Collision2D collision) //When it comes in contact with a collider
    {
        bool floored = playerDetail.GetComponent<PlayerController>().isOnFloor;
        if (collision.collider.tag == "Walls" || collision.collider.tag == "TargetHitbox") //If that Collider's tagged as being part of the floor
        {
            playerDetail.GetComponent<PlayerController>().isStuck = false; //State that we are now on the floor.
        }
    }
}
