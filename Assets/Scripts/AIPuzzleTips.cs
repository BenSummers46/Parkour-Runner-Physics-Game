using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPuzzleTips : MonoBehaviour
{
    //Class that provides the user with a tip for the level 2 puzzle if they get stuck, example of positive feedback
    //and collision response.

    [SerializeField] GameObject tip;

    float timeBeforeTip = 30f;
    float timeBeforeHide = 10f;

    //Detects the users collision into the puzzle area
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerCapsule"))
        {
            Invoke("ShowTip", timeBeforeTip);
        }
    }

    //Invoke used for timing the popups.
    void ShowTip()
    {
        tip.SetActive(true);
        Invoke("HideTip", timeBeforeHide);
    }

    void HideTip()
    {
        tip.SetActive(false);
    }
}
