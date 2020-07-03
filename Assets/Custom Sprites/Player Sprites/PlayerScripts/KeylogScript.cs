using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeylogScript : MonoBehaviour
{
    List<int> log = new List<int>(); //Initializing a list as its dynamic and has an indefinite amount of capacity, may incur memory problems
    enum previousKey { };

    /*
     [7][8][9]
     [4][5][6]
     [1][2][3]

    Once again I'll be using the above graph for determining direction.
    */


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void commandLogger()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                log.Add(5); //Neutral Input
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                log.Add(1); //Down Left
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                log.Add(3); //Down Right
            }
            else
            {
                log.Add(2); //Down
            }
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                log.Add(5); //Neutral Input
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                log.Add(7); //Up left
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                log.Add(9); //Up right
            }
            else
            {
                log.Add(8); //Up
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                log.Add(5); //Neutral
            }
            else
            {
                log.Add(6); //Right
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                log.Add(5); //Neutral
            }
            else
            {
                log.Add(4); //Left
            }
        }


    }
}
