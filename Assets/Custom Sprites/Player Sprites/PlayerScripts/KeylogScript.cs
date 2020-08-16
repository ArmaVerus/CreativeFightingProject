#define UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;



public class KeylogScript : MonoBehaviour
{
    List<string> log = new List<string>(); //Initializing a list as its dynamic and has an indefinite amount of capacity, may incur memory problems
    string previousKey;
    string currentKey;
    bool duplicateKey;
    bool saved;
    public GameObject stageTarget; //getting the stage target for this stage.
    public TargetScript st;
    

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
        st = stageTarget.GetComponent<TargetScript>();
        saved = false;
    }

    // Update is called once per frame
    void Update()
    {
        currentKey = currentKeyCheck();
        duplicateKey = string.Equals(previousKey, currentKey);
        if (!duplicateKey && st.currentHealth > 0)
        {
            commandLogger();
        }
        if (st.currentHealth <= 0 && !saved)
        {
            outputData(storagePath(), log);
            saved = true;
        }
    }

    string currentKeyCheck()
    {
        if (Input.GetKey(KeyCode.F))
        {
            return "Punch";
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                return "Neutral";
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                return "Down-Left";
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                return "Down-Right";
            }
            else
            {
                return "Down";
            }
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                return "Up-Left";
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                return "Up-Right";
            }
            else
            {
                return "Up";
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                return "Neutral";
            }
            else
            {
                return "Right";
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            return "Left";
        }
        else
        {
            return "None";
        };


    }

    void commandLogger()
    {
        if (Input.GetKey(KeyCode.F))
        {
            log.Add("Punch"); //Punching is necessary to throw a Fireball
            previousKey = "Punch";
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                log.Add("Neutral"); //Neutral Input
                previousKey = "Neutral";
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                log.Add("Down-Left"); //Down Left
                previousKey = "Down-Left";
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                log.Add("Down-Right"); //Down Right
                previousKey = "Down-Right";
            }
            else
            {
                log.Add("Down"); //Down
                previousKey = "Down";
            }
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                log.Add("Up-Right"); //Up left
                previousKey = "Up-Left";
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                log.Add("Up-Right"); //Up right
                previousKey = "Up-Right";
            }
            else
            {
                log.Add("Up"); //Up
                previousKey = "Up";
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                log.Add("Neutral"); //Neutral
                previousKey = "Neutral";
            }
            else
            {
                log.Add("Right"); //Right
                previousKey = "Right";
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            log.Add("Left"); //Left
            previousKey = "Left";
        }


    }

    private string storagePath()
    {
        DateTime theTime = DateTime.Now;
        string datetime = theTime.ToString("yyyy-MM-dd-HH-mm");
#if UNITY_EDITOR
        return Application.dataPath + "/LogData/"  + "CommandLog_" + datetime + ".csv";
#else
        return Application.dataPath + "/" + "CommandLog_" + datetime + ".csv";
#endif
    }

    void outputData(string filePath, List<string> input)
    {
        StreamWriter output = new StreamWriter(filePath);

        for (int i = 0; i < input.Count; i++)
        {
            if (i == 0)
            {
                output.Write(input[i]);
            }
            else
            {
                output.Write("," + input[i]);
            }
        }

        output.Flush();
        output.Close();
    }


}
