using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCount : MonoBehaviour
{
    public float storedTime; //Stores the current time as a float
    [SerializeField] Text Timer; //This initializes the text field.
    public GameObject stageTarget; //getting the stage target for this stage.
    public TargetScript st;

    void Start()
    {
        st = stageTarget.GetComponent<TargetScript>();
        Timer.text = "0"; //Set the Timer to 0 when the Scene starts
    }

    void Update()
    {
        storedTime += Time.deltaTime; //Increment progressed Time, We use Delta Time and not Time.time as Time.time cannot be reset, DeltaTime is much more useful for reset timers

        if (st.currentHealth > 0) //we want to stop the timer when the stage target has 0 health so we know how much time it took to win.
        {
            Timer.text = storedTime.ToString("0");
        }

    }
}
