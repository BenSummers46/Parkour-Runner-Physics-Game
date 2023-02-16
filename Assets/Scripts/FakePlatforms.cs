using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePlatforms : MonoBehaviour
{ 
    //Provides a random chance for the collider of the fake platforms to spawn for a more random gameplay aspect.
    
    // Start is called before the first frame update
    void Start()
    {
       

        Collider collider = GetComponent<BoxCollider>();

        if(RandomNumber() == 1)
        {
            collider.enabled = true;
        }
        else
        {
            collider.enabled = false;
        }
        
    }

    int RandomNumber()
    {
        return Random.Range(0, 2);
    }
}
