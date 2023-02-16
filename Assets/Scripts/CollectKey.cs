using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectKey : MonoBehaviour
{
    //Function that handles the key needed to complete the dynamic AI area on level 4.
    //Plays a sound clip when collected and sets the correct variables.
    [SerializeField] GameObject collected;
    [SerializeField] AudioClip collectedSound;

    DynamicMapPuzzleEnding key;

    private void Awake()
    {
        key = FindObjectOfType<DynamicMapPuzzleEnding>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerCapsule"))
        {
            key.KeyCollected();

            AudioSource.PlayClipAtPoint(collectedSound, transform.position);

            this.gameObject.SetActive(false);
            collected.SetActive(true);
            Invoke("Reset", 5f);
        }
    }

    private void Reset()
    {
        collected.SetActive(false);
    }

}
