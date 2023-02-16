using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    // Controls the status of the player and checks whether they have been "stabbed"/collided with the enemy.
    //Also controls when the player has collided with a GOAP enemy who does varibale damage depending on if they have a weapon.

    int health = 100;

    private void Update()
    {
        if (Dead())
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("Death Screen");
        }
    }

    // Applies damage to the player
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health -= 52;
        } else if (collision.gameObject.CompareTag("GOAPenemy"))
        {
            GOAPEnemy enemyScript = collision.gameObject.GetComponent<GOAPEnemy>();
            health -= enemyScript.damage;
        }
    }

    // Returns if the user is dead or not.
    bool Dead()
    {
        return health <= 0;
    }
}
