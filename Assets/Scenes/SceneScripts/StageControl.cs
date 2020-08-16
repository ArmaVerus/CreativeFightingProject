using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageControl : MonoBehaviour
{
    public static StageControl stageHandler;

    public int Stage = 0;
    // Start is called before the first frame update

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DestroyWhenDone();
    }

    void DestroyWhenDone()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            if (Stage > 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
