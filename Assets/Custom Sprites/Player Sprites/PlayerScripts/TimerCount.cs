using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCount : MonoBehaviour
{
    float storedTime; //Stores the current time as a float
    [SerializeField] Text Timer; //This initializes the text field.
    public GameObject stageTarget; //getting the stage target for this stage.
    public TargetScript st;

    void Start()
    {
        st = stageTarget.GetComponent<TargetScript>();
    }

    void Update()
    {
        if (st.currentHealth > 0) //we want to stop the timer when the stage target has 0 health so we know how much time it took to win.
        {
            storedTime = Time.time;
            Timer.text = storedTime.ToString("0");//This command turns the float to a string, 0 means that it will only use the first character in the string
        }

    }
}
