using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMapPuzzleEnding : MonoBehaviour
{
    //Handles whether the user has met the requirements to complete the dynamic map puzzle in level 4
    //The key must be collected from the centre and then the user must get to the right spot on the map to progress.
    [SerializeField] GameObject runWall;
    [SerializeField] GameObject tipUI;
    [SerializeField] GameObject completedUI;

    bool iskeyCollected = false;

    public void KeyCollected()
    {
        iskeyCollected = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerCapsule"))
        {
            if (iskeyCollected)
            {
                runWall.SetActive(true);
                completedUI.SetActive(true);
                Invoke("ResetCompletion", 5f);
            }
            else
            {
                tipUI.SetActive(true);
                Invoke("ResetTip", 10f);
            }
        }
    }

    void ResetTip()
    {
        tipUI.SetActive(false);
    }

    void ResetCompletion()
    {
        completedUI.SetActive(false);
    }
}
