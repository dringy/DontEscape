using UnityEngine;
using System.Collections;

//Detects if the player has stopped for a specified length of time
public class WaitDetector : MonoBehaviour {
    //Timer
    [SerializeField]
    private float timeToWait;

    private float timeWaited = 0.0f;
	
	private void Update () {
        if (
            !SpeechManager.get.InSpeechMode() &&
            Input.GetAxisRaw("Vertical") == 0.0f &&
            Input.GetAxisRaw("Horizontal") == 0.0f &&
            !Input.GetKeyDown(KeyCode.Space)
            )
        {
            //When the timer is reached, disable
            timeWaited += Time.deltaTime;

            if (timeWaited > timeToWait)
            {
                enabled = false;
            }
        }
        //Controls will reset the timer
        else
        {
            timeWaited = 0.0f;
        }
	}

    public void Activate()
    {
        timeWaited = 0.0f;
        enabled = true;
    }

    public void Deactivate()
    {
        timeWaited = 0.0f;
        enabled = false;
    }

    public bool IsTriggered()
    {
        return (timeWaited > timeToWait);
    }
}
