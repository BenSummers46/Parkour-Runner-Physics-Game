using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    // Controls the dynamic gravity values that are used within the level 2 gravity section.

    Vector3 newGravity = new Vector3(0f, -3.3f, 0f);
    Vector3 resetGravity = new Vector3(0f, -9.8f, 0f);

    public float powerupTime = 11f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerCapsule"))
        {
            Physics.gravity = newGravity;
        }
    }

    // Resets the value of gravity back to the original value.
    private void OnTriggerExit(Collider other)
    {
        Invoke("GravityReset", powerupTime);
    }

    void GravityReset()
    {
        Physics.gravity = resetGravity;
    }
}
