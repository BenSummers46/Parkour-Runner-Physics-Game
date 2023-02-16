using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityTextCollider : MonoBehaviour
{
    [SerializeField] GameObject text;

    // Controls when the text for the gravity section appears in the game world.
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerCapsule"))
        {
            text.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        text.SetActive(false);
    }
}
