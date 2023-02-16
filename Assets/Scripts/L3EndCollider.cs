using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L3EndCollider : MonoBehaviour
{
    [SerializeField] PhysicMaterial groundMat;
    [SerializeField] GameObject gun;
    [SerializeField] GameObject wall;

    // Resest the values of the physics materials back to their original values.
    // Returns the users gun and also opens their path forward.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Physics Box"))
        {
            groundMat.dynamicFriction = 1f;
            groundMat.staticFriction = 1f;
            gun.SetActive(true);
            wall.SetActive(false);
        }
    }
}
