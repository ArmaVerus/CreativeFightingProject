using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LoadMenu();
    }

    void LoadMenu()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            SceneManager.LoadScene("MenuSelect");
        }
    }
}
