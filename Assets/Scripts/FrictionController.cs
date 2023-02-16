using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrictionController : MonoBehaviour
{
    // Controls the variation in physics material properties for the level 3 puzzle.

    [SerializeField] PhysicMaterial ground;
    [SerializeField] GameObject hint;


    //Sets the physics material properties after the collision.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerCapsule"))
        {
            ground.dynamicFriction = 0f;
            ground.staticFriction = 0f;
            hint.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        hint.SetActive(false);
    }
}
