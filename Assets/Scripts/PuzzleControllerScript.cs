using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleControllerScript : MonoBehaviour
{
    // Controls the cube puzzle at the end of level 2.

    [SerializeField] GameObject door;
    [SerializeField] GameObject physicsCube;

    [SerializeField] Transform cube1Spawn1;
    [SerializeField] Transform cube1Spawn2;
    [SerializeField] Transform cube1Spawn3;

    [SerializeField] Transform cube2Spawn1;
    [SerializeField] Transform cube2Spawn2;
    [SerializeField] Transform cube2Spawn3;

    List<Transform> cube1List = new List<Transform>();
    List<Transform> cube2List = new List<Transform>();


    // Variables must be set to true for the puzzle to complete.
    public bool cube1Complete = false;
    public bool cube2Complete = false;
    
    // Adds the physics cubes to the level, there are always 2 that spawn in a random location in that area.
    // This means the level will always be completable.
    void Start()
    {
        AddCube1Points();
        AddCube2Points();
        int cube1Position = Random.Range(0, 3);
        int cube2Position = Random.Range(0, 3);
        Instantiate(physicsCube, cube1List[cube1Position].position, Quaternion.identity);
        Instantiate(physicsCube, cube2List[cube2Position].position, Quaternion.identity);
    }

    // Opens the door if the puzzle has been completed.
    void Update()
    {
        if (PuzzleComplete())
        {
            door.SetActive(false);
        }
    }

    // Checks if the puzzle has been completed.
    bool PuzzleComplete()
    {
        return cube1Complete && cube2Complete;
    }

    // Adds cube 1's random spawn points.
    void AddCube1Points()
    {
        cube1List.Add(cube1Spawn1);
        cube1List.Add(cube1Spawn2);
        cube1List.Add(cube1Spawn3);
    }

    // Adds cube 2's random spawn points.
    void AddCube2Points()
    {
        cube2List.Add(cube2Spawn1);
        cube2List.Add(cube2Spawn2);
        cube2List.Add(cube2Spawn3);
    }
}
