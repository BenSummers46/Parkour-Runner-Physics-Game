using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    //Class that provides the enemy AI's different states depending on what is happening in game.
    //Another small GOAP as the enemies have tasks to be carried out and points to go to.

    [SerializeField] GameObject player;

    [SerializeField] GameObject navPoint;
    [SerializeField] GameObject navPoint1;
    [SerializeField] GameObject navPoint2;
    [SerializeField] GameObject navPoint3;
    [SerializeField] GameObject navPoint4;

    [SerializeField] GameObject coverPoint;
    [SerializeField] GameObject coverPoint1;
    [SerializeField] GameObject coverPoint2;
    [SerializeField] GameObject coverPoint3;
    

    NavMeshAgent agent;

    List<GameObject> navPoints = new List<GameObject>();
    List<GameObject> coverPoints = new List<GameObject>();

    public int health = 100;

    // Adding patrol and cover points that the AI could use.
    void Start()
    {
        navPoints.Add(navPoint);
        navPoints.Add(navPoint1);
        navPoints.Add(navPoint2);
        navPoints.Add(navPoint3);
        navPoints.Add(navPoint4);

        coverPoints.Add(coverPoint);
        coverPoints.Add(coverPoint1);
        coverPoints.Add(coverPoint2);
        coverPoints.Add(coverPoint3);

        agent = GetComponent<NavMeshAgent>();
        agent.destination = Patrol();

       
    }

    // Manages the various states of the AI.
    void Update()
    {

        if (EnemyDead())
        {
            Destroy(gameObject);
        }
        else if (HeavilyInjured())
        {
            if(agent.remainingDistance < 0.5f)
            {
                agent.destination = InjuredPatrol(); 
            }
        }
        else if (Alerted())
        {

            if (agent.remainingDistance < 0.5f)
            {
                agent.destination = AlertedPatrol();
            }
            
        }
        else if (PlayerInSight())
        {
            agent.destination = player.transform.position;
        }
        else
        {
            NewPatrolPoint();
        }
        
    }

    void NewPatrolPoint()
    {
        if (agent.remainingDistance < 0.5f)
        {
            agent.destination = Patrol();
        }
    }

    // Chooses a patrol point from a set list of points, to simulate a guard patrolling
    Vector3 Patrol()
    {
        GameObject randomPoint = GetRandomPatrol();
        Vector3 destination = randomPoint.transform.position;

        NavMeshHit navHit;

        NavMesh.SamplePosition(destination, out navHit, 50.0f, NavMesh.AllAreas);

        return destination;
    }

    // If the guard is alerted a completely random patrol will be made.
    Vector3 AlertedPatrol()
    {
        Vector3 destination = transform.position;
        Vector2 randomDirection = Random.insideUnitCircle * 8.0f;

        destination.x += randomDirection.x;
        destination.z += randomDirection.y;

        NavMeshHit navHit;

        NavMesh.SamplePosition(destination, out navHit, 25.0f, NavMesh.AllAreas);

        return navHit.position;
    }

    // If the guard is injured they will navigate towards a cover point.
    Vector3 InjuredPatrol()
    {
        GameObject randomPoint = GetRandomCover();
        Vector3 destination = randomPoint.transform.position;

        NavMeshHit navHit;

        NavMesh.SamplePosition(destination, out navHit, 50.0f, NavMesh.AllAreas);

        return destination;
    }

    //Checks to see if the player is in sight, player must be within an FOV of 80 degrees
    // to be spotted.
    bool PlayerInSight()
    {
        
        Vector3 rayPos = transform.position;
        Vector3 rayDir = (player.transform.position - rayPos).normalized;

        RaycastHit info;
        if(Physics.Raycast(rayPos, rayDir, out info))
        {
            if (info.transform.CompareTag("PlayerCapsule"))
            {
                Vector3 targetDirection = info.transform.position - transform.position;
                Vector3 inFront = transform.forward;
                float angle = Vector3.Angle(targetDirection, inFront); 
                if(angle <= 80)
                {
                    return true;
                }
            }
        }
        return false;
    }

    // Returns a patrol point from the list of set patrol points.
    GameObject GetRandomPatrol()
    {
        int randomIndex = Random.Range(0, navPoints.Count); //was Capacity - 1

        return navPoints[randomIndex];
    }

    // Returns a point of cover for the AI to navigate too
    GameObject GetRandomCover()
    {
        int randomIndex = Random.Range(0, coverPoints.Count); // was Capacity - 1

        return coverPoints[randomIndex];
    }

    // Checks if the AI is alerted.
    bool Alerted()
    {
        return GetHealth() < 100;
    }

    //Checks if the AI is heavily injured
    bool HeavilyInjured()
    {
        return GetHealth() < 30;
    }

    // Setter for the AI's health
    public void SetHealth()
    {
        health -= 34;
    }

    // Getter for the enemies health.
    public int GetHealth()
    {
        return health;
    }

    // Checks if the enemy is dead
    bool EnemyDead()
    {
        return health <= 0;
    }

    // Applies damage for the head hitbox
    public void HeadShotSetHealth()
    {
        health -= 101;
    }
}
