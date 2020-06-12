using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    GameObject playerDetail;
    // Start is called before the first frame update
    void Start()
    {
        playerDetail = gameObject.transform.parent.gameObject; //initialize the gameObject
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
    }

    private void OnCollisionExit2D(Collision2D collision) //When the collider leaves contact with a collider
    {
        if (collision.collider.tag == "Floor") //If the collider we separated from was the floor
        {
            playerDetail.GetComponent<PlayerController>().isOnFloor = false; //We're no longer on the floor
        }
    }
}
