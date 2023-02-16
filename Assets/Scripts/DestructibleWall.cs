using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleWall : MonoBehaviour
{
    //Functions that allow the user to shoot and destroy the green walls on level 4
    //The destruction of these walls will also update the NavMesh so the AI can now navigate the open areas.
    
    public int health = 300;

    private void Update()
    {
        if(health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public int GetHealth()
    {
        return health;
    }

    public void SetHealth(int damage)
    {
        health -= damage;
    }
}
