using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L3StartCollider : MonoBehaviour
{
    [SerializeField] GameObject gun;
    [SerializeField] GameObject tipText;
    [SerializeField] GameObject wall;

    // Controls the puzzle by removing the users gun and blocking their path backwards.
    // Provides the user with a tip on how to progress.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerCapsule"))
        {
            gun.SetActive(false);
            tipText.SetActive(true);
            wall.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Invoke("ResetTip", 10f);
    }

    void ResetTip()
    {
        tipText.SetActive(false);
    }
}
