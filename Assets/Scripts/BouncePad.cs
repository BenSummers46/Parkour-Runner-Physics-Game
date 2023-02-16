using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    Rigidbody rb;

    float multiplier = 12f;

    
    //Handles the collisions and physics calculations for the bounce pads, to make it so that they bounce roughly the
    //same height that they have dropped.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerCapsule"))
        {
            rb = other.gameObject.GetComponent<Rigidbody>();
            float yVelocity = rb.velocity.y;
            rb.AddForce(Vector3.up * -yVelocity * multiplier, ForceMode.Impulse);
        }
    }
}
