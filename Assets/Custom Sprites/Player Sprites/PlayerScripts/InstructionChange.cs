using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionChange : MonoBehaviour
{ 
    public GameObject stageController;
    public int currentStage;
    private SpriteRenderer renderer;
    private Sprite instFire, instUpper, instWind;
    // Start is called before the first frame update
    void Start()
    {
        stageController = GameObject.Find("StageController");
        currentStage = stageController.GetComponent<StageControl>().Stage;
        renderer = GetComponent<SpriteRenderer>();
        instFire = Resources.Load<Sprite>("InstructionFireball");
        instUpper = Resources.Load<Sprite>("InstructionUppercut");
        instWind = Resources.Load<Sprite>("InstructionWhirlwind");
        if (currentStage == 1)
        {
            renderer.sprite = instFire;
        }
        if (currentStage == 2)
        {
            renderer.sprite = instUpper;
        }
        if (currentStage == 3)
        {
            renderer.sprite = instWind;
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
