using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipCollider : MonoBehaviour
{
    // Controls the tips popup for level 2 so that they are not visible all the time.

    [SerializeField] GameObject quickText;
    [SerializeField] GameObject safeText;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerCapsule"))
        {
            quickText.SetActive(true);
            safeText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        quickText.SetActive(false);
        safeText.SetActive(false);
    }
}
