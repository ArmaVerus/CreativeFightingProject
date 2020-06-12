using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRate : MonoBehaviour
{
    int frameRate = 60; //Set the framerate
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = frameRate; //Set the application's frameRate to my intended framerate
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
