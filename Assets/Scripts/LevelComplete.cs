using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    // Script will activate when the user enters the winning area.
    // Stops the round timer.
    // Brings up the level complete menu.
    // Sets the relevant values for the level which has been completed.
    // Effects and sounds are now played at the end of each level
    // Handles updating the relevant varibales when the user completes a level.

    [SerializeField] Canvas levelComplete;
    [SerializeField] GameObject timer;
    [SerializeField] GameObject endEffect;
    [SerializeField] AudioClip endSound;

    RoundTimer roundTimer;
    PersistentLogic pl;

    private void Start()
    {
        roundTimer = timer.GetComponent<RoundTimer>();
        pl = FindObjectOfType<PersistentLogic>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerCapsule")){
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            roundTimer.StopAllCoroutines();
            levelComplete.gameObject.SetActive(true);
            endEffect.SetActive(true);

            AudioSource.PlayClipAtPoint(endSound, transform.position);

            if (SceneManager.GetActiveScene().name == "Level 1 - Introduction")
            {
                pl.setLevel2Status();
            }
            else if(SceneManager.GetActiveScene().name == "Level 2 - Puzzles")
            {
                pl.setLevel3Status();
            }
            else if(SceneManager.GetActiveScene().name == "Level 3 - Bouncing")
            {
                pl.setLevel4Status();
            }
            else if(SceneManager.GetActiveScene().name == "Level 4 - Horde")
            {
                pl.setLevel4Complete();
            }
        }
    }
}
