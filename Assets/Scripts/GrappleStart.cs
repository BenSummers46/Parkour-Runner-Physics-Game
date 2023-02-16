using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleStart : MonoBehaviour
{
    //Handles the UI elements being shown that alert the user to the fact that they can now use a grappling hook.
    
    [SerializeField] GameObject gun;
    [SerializeField] GameObject UI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerCapsule"))
        {
            gun.GetComponent<GrapplingGun>().enabled = true;
            UI.SetActive(true);
            Invoke("ResetUI", 5f);
        }
    }

    void ResetUI()
    {
        UI.SetActive(false);
    }
}
