using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour
{
    // Controls the wall-running physics for the player capsule.

    [SerializeField] float downAcceleration = 1f;
    [SerializeField] float wallRunJump = 25f;
    [SerializeField] GameObject windEffect;

    Camera cam;
    AudioSource audioSource;

    Vector3 normal;

    bool attached = false;

    Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        cam = GetComponentInChildren<Camera>();

        audioSource = GetComponent<AudioSource>();
    }

    // Checks if the player is in the "attached" state
    // If so then all the applicable variables are set to match.
    void Update()
    {
        if (attached)
        {
            rb.useGravity = false;

            setCam();
            PlaySound();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(transform.up * wallRunJump, ForceMode.Impulse);
                rb.AddForce(normal * 10, ForceMode.Impulse);
            }

            if (rb.velocity.magnitude < 0.5)
            {
                rb.AddForce(-transform.up * 10 * downAcceleration, ForceMode.Acceleration);
            }

        } else if (!attached && cam.fieldOfView > 80f)
        {
            ResetCam();
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Pause();
            }
        }
    }

    // Checks if the user collides with a runnable wall.
    // Gets the collision normal to set the camera.
    // Wind effect added for when the player is wall running.
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.name == "RunWall")
        {
            attached = true;
            windEffect.SetActive(true);
        }
        normal = collision.contacts[0].normal;
    }

    // Resets the game state after the player exits the wall.
    private void OnCollisionExit(Collision collision)
    {
        attached = false;
        rb.useGravity = true;
        windEffect.SetActive(false);
    }

    // Sets camera position while wall running.
    void setCam()
    {

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 95f, 0.1f);
    }

    void ResetCam()
    {
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 80f, 0.1f);
    }

    void PlaySound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

}
