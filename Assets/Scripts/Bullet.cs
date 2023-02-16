using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    EnemyController enemyController;
    public int wallDamage = 27;


    //Handles the various hit box options of the enemy capsule, if the bullet hits a different area, different damage
    //will be applied to the enemy.
    //Handles the damage that will be done to the green walls on level 4 that can be destroyed after being shot at.
    private void OnCollisionEnter(Collision collision)
    {
        
     
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SetEnemyController(collision);
            enemyController.SetHealth();
        }
        else if (collision.gameObject.CompareTag("Enemy Head"))
        {
            EnemyController enemyController = collision.gameObject.GetComponentInParent<EnemyController>();
            enemyController.HeadShotSetHealth();

        }else if (collision.gameObject.CompareTag("ShootWall"))
        {
            DestructibleWall dw = collision.gameObject.GetComponent<DestructibleWall>();
            dw.SetHealth(wallDamage);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void SetEnemyController(Collision collision)
    {
        enemyController = collision.gameObject.GetComponent<EnemyController>();
    }
}
