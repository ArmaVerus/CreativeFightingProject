﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadWhirlwind : MonoBehaviour
{
    public StageControl stageController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadScene()
    {
        stageController.GetComponent<StageControl>().Stage = 3;
        SceneManager.LoadScene("FightScene");
    }
}
