using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2FinishPoint1 : MonoBehaviour
{
    // Script for checking the completion of the level 2 cube puzzle.

    [SerializeField] PuzzleControllerScript puzzleController;
    [SerializeField] Material finishedMat;
    [SerializeField] Material originalMat;

    MeshRenderer myRenderer;

    private void Start()
    {
        myRenderer = GetComponent<MeshRenderer>();
    }

    // Checks if the cube has been pushed into point 1 or point 2 and will update the relevant puzzle variables.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Physics Box"))
        {
            if(gameObject.name == "FinishPoint1")
            {
                puzzleController.cube1Complete = true;

                myRenderer.material = finishedMat;
            }
            else if(gameObject.name == "FinishPoint2")
            {
                puzzleController.cube2Complete = true;

                myRenderer.material = finishedMat;
            }

        }
    }

    // If the cubes leave the area, the puzzle variables will be updated.
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Physics Box"))
        {
            if(gameObject.name == "FinishPoint1")
            {
                puzzleController.cube1Complete = false;

                myRenderer.material = originalMat;
            }else if (gameObject.name == "FinishPoint2")
            {
                puzzleController.cube1Complete = false;

                myRenderer.material = originalMat;
            }
        }
    }
}
