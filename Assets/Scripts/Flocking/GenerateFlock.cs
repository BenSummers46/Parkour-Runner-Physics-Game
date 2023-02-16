using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateFlock : MonoBehaviour
{
    //Function generates the zombies at random points onto the given plane.
    
    [SerializeField] Transform floor;
    [SerializeField] GameObject enemy;


    float xMax; 
    float xMin; 

    float zMax; 
    float zMin; 

    float yValue = 337f;

    public int enemiesToSpawn = 50;

    // On level start 50 enemies will be spawned in a uniformly random position.
    void Start()
    {
        xMax = floor.transform.position.x + 50;
        xMin = floor.transform.position.x - 50;

        zMax = floor.transform.position.z + 50;
        zMin = floor.transform.position.z - 50;

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(xMin, xMax), yValue, Random.Range(zMin, zMax));

            Instantiate(enemy, randomPosition, transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0));
        }
    }

    
}
