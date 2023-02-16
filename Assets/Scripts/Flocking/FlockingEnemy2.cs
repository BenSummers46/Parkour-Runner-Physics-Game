using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class FlockingEnemy2 : MonoBehaviour
{
    //Function that handles the flocking zombies that have been implemented.
    
    [SerializeField] Vector3[] directionsToCheck;
    [SerializeField] GameObject player;
    
    private static readonly float mRadiusSquaredDistance = 5.0f;
    private static readonly float mMaxVelocity = 10.0f;

    private Vector3 mVelocity = new Vector3();
    private Vector3 currentObstacleAvoidanceVector;

    public float separationWeight = 0.8f;
    public float alignmentWeight = 0.5f;
    public float cohesionWeight = 0.7f;
    public float obstacleWeight = 1.5f;
    public float obstacleDistance = 5f;
    public LayerMask obstacle;

    NavMeshAgent agent;


    void Start()
    {
        mVelocity = transform.forward;
        mVelocity = Vector3.ClampMagnitude(mVelocity, mMaxVelocity);
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("PlayerCapsule");
    }

    
    //Checks if the zombies can see the player and if they can they will all flock towards.
    //Otherwise normal flocking behaviour is used.
    void Update()
    {
        if (CanSeePlayer())
        {
            agent.destination = player.transform.position;
        }
        else
        {
            mVelocity += FlockingBehaviour();

            mVelocity = Vector3.ClampMagnitude(mVelocity, mMaxVelocity);

            transform.position += mVelocity * Time.deltaTime;

            transform.forward = mVelocity.normalized;
        }

    }

    
    //Handles the flocking behaviour of the zombies.
    private Vector3 FlockingBehaviour()
    {
        List<FlockingEnemy2> theFlock = FindObjectsOfType<FlockingEnemy2>().ToList<FlockingEnemy2>();
       

        Vector3 cohesionVector = new Vector3();
        Vector3 separateVector = new Vector3();
        Vector3 alignmentVector = new Vector3();
        Vector3 obstacleVector = new Vector3();
        Vector3 boundsVector = new Vector3();

        int count = 0;

        for (int index = 0; index < theFlock.Count; index++)
        {
            boundsVector += theFlock[index].transform.position;
            if (theFlock[index] != this)
            {
                float distance = (transform.position - theFlock[index].transform.position).sqrMagnitude;

                if (distance > 0 && distance < mRadiusSquaredDistance)
                {
                    cohesionVector += theFlock[index].transform.position;
                    separateVector += theFlock[index].transform.position - transform.position;
                    alignmentVector += theFlock[index].transform.forward;

                    count++;
                }
            }
        }

        obstacleVector = CalculateObstacleVector();

        if (count == 0)
        {
            return Vector3.zero;
        }

        
        separateVector /= count;
        separateVector *= -1;

        alignmentVector /= count;

        cohesionVector /= count;
        cohesionVector += (cohesionVector - transform.position);

        boundsVector /= theFlock.Count;

        Vector3 flockingVector = ((separateVector.normalized * separationWeight) +
                                    (cohesionVector.normalized * cohesionWeight) +
                                    (alignmentVector.normalized * alignmentWeight) +
                                    (obstacleVector * obstacleWeight) +
                                    (boundsVector * 10f));

        
         return flockingVector;
        
    }

    //Checks if a boid is near an obstacle
    Vector3 CalculateObstacleVector()
    {
        var obstacleVector = Vector3.zero;
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, obstacleDistance, obstacle))
        {
            obstacleVector = FindBestDirectionToAvoidObstacle();
        }
        else
        {
            currentObstacleAvoidanceVector = Vector3.zero;
        }
        return obstacleVector;
    }

    //Tries to find the best way around that obstacle.
    Vector3 FindBestDirectionToAvoidObstacle()
    {
        if (currentObstacleAvoidanceVector != Vector3.zero)
        {
            RaycastHit hit;
            if (!Physics.Raycast(this.transform.position, this.transform.forward, out hit, obstacleDistance, obstacle))
            {
                return currentObstacleAvoidanceVector;
            }
        }
        float maxDistance = int.MinValue;
        var selectedDirection = Vector3.zero;
        for (int i = 0; i < directionsToCheck.Length; i++)
        {

            RaycastHit hit;
            var currentDirection = this.transform.TransformDirection(directionsToCheck[i].normalized);
            if (Physics.Raycast(this.transform.position, currentDirection, out hit, obstacleDistance, obstacle))
            {

                float currentDistance = (hit.point - this.transform.position).sqrMagnitude;
                if (currentDistance > maxDistance)
                {
                    maxDistance = currentDistance;
                    selectedDirection = currentDirection;
                }
            }
            else
            {
                selectedDirection = currentDirection;
                currentObstacleAvoidanceVector = currentDirection.normalized;
                return selectedDirection.normalized;
            }
        }
        return selectedDirection.normalized;
    }

    //Checks if the enemy can see the player.
    bool CanSeePlayer()
    {

        Vector3 rayPos = transform.position;
        Vector3 rayDir = (player.transform.position - rayPos).normalized;

        RaycastHit info;
        if (Physics.Raycast(rayPos, rayDir, out info))
        {
            if (info.transform.CompareTag("PlayerCapsule"))
            {
                Vector3 targetDirection = info.transform.position - transform.position;
                Vector3 inFront = transform.forward;
                float angle = Vector3.Angle(targetDirection, inFront);
                if (angle <= 80)
                {
                    return true;
                }
            }
        }
        return false;
    }

}
