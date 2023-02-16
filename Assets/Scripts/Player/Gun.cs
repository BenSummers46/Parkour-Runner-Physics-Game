using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    //Script controls all aspects of the gun, including shooting, reloading and fire rate.

    public GameObject bullet;

    public float shootForce;

    public float timeBetweenShooting, reloadTime;

    public int magazineSize, bulletsLeft, bulletsShot;

    bool shooting, readyToShoot, reloading;

    public Camera fpsCam;
    public Transform attackPoint;

    public bool allowInvoke = true;

    GameObject currentBullet;

    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject muzzleFlash;

    //Gunshot audio clip
    [SerializeField] AudioClip GunShot;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Checks for the users input
    void Update()
    {
        MyInput();
    }


    //Provides the different states that the gun can be in from the users input.
    //Plays the audio clip when the user shoots the gun.
    private void MyInput()
    {
        shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if(Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
        {
            GetComponent<Animator>().SetTrigger("Reload");
            Reload();
        }
        
        if(readyToShoot && shooting && !reloading && bulletsLeft <= 0)
        {
            GetComponent<Animator>().SetTrigger("Reload");
            Reload();
        }

        if(readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = 0;

            AudioSource.PlayClipAtPoint(GunShot, transform.position);
            Shoot();

        }
    }

    // Controls the shooting by instantiating the bullet prefab and adding force in the direction of the centre of the screen.
    // Controls the amount of bullets left in the clip
    void Shoot()
    {
        readyToShoot = false;

        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if(Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(250);
        }

        Vector3 direction = targetPoint - attackPoint.position;

        currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);

        currentBullet.transform.forward = direction.normalized;

        currentBullet.GetComponent<Rigidbody>().AddForce(direction.normalized * shootForce, ForceMode.Impulse);

        PlayAnimation();
        muzzleFlash.SetActive(true);
        Invoke("ResetFlash", 0.2f);

        bulletsLeft--;
        bulletsShot++;

        //text.text = bulletsLeft.ToString() + " / 16";

        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }
    }

    // Handles the Rate of Fire so that the user cannot shoot too quickly.
    void ResetShot()
    {
        readyToShoot = true;
        bulletsLeft++;
        bulletsShot--;
        text.text = bulletsLeft.ToString() + " / 16";
        allowInvoke = true;
    }

    // Handles the guns reloading state, the player cannot shoot while this is active.
    void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    // Finishes the reload so the user can shoot again.
    void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        text.text = bulletsLeft.ToString() + " / 16";
        reloading = false;
    }

    //Resets the muzzle flash effect added to the guns barrel
    void ResetFlash()
    {
        muzzleFlash.SetActive(false);
    }

    //Plays the weapons animation of the slide being pulled back when the gun is shot.
    void PlayAnimation()
    {
        GetComponent<Animation>().Play("M1911@Fire");
    }
}
