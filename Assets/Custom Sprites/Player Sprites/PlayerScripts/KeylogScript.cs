using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeylogScript : MonoBehaviour
{
    List<string> log = new List<string>(); //Initializing a list as its dynamic and has an indefinite amount of capacity, may incur memory problems
    string previousKey;
    string currentKey;
    bool duplicateKey;

    /*
     [7][8][9]
     [4][5][6]
     [1][2][3]

    Once again I'll be using the above graph for determining direction.
    */


    // Start is called before the first frame update
    void Start()
    {
        previousKey = "No Key Yet";
    }

    // Update is called once per frame
    void Update()
    {
        currentKey = currentKeyCheck();
        duplicateKey = string.Equals(previousKey, currentKey);
        if (!duplicateKey)
        {
            commandLogger();
        }
    }

    string currentKeyCheck()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                return "5";
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                return "1";
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                return "3";
            }
            else
            {
                return "2";
            }
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                return "7";
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                return "9";
            }
            else
            {
                return "8";
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                return "5";
            }
            else
            {
                return "6";
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            return "4";
        }
        else
        {
            return "none";
        };


    }

    void commandLogger()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                log.Add("5"); //Neutral Input
                previousKey = "5";
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                log.Add("1"); //Down Left
                previousKey = "1";
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                log.Add("3"); //Down Right
                previousKey = "3";
            }
            else
            {
                log.Add("2"); //Down
                previousKey = "2";
            }
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                log.Add("7"); //Up left
                previousKey = "7";
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                log.Add("9"); //Up right
                previousKey = "9";
            }
            else
            {
                log.Add("8"); //Up
                previousKey = "8";
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                log.Add("5"); //Neutral
                previousKey = "5";
            }
            else
            {
                log.Add("6"); //Right
                previousKey = "6";
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            log.Add("4"); //Left
            previousKey = "4";
        }


    }
}
