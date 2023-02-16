using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GOAPEnemy : MonoBehaviour
{
    //Script that controls the GOAP enemies
    //Tasks to do such as getting a weapon to increase their damage
    //Player can distract enemies from their patrols using the alarm.
    
    [SerializeField] GameObject player;
    [SerializeField] GameObject alarm;
    [SerializeField] GameObject alarmPoint;
    [SerializeField] Transform weaponStation;

    public bool haveWeapon;
    
    AudioSource audioSource;
    AlarmScript alarmScript;

    NavMeshAgent agent;
    Vector3 startPos;

    public int damage = 20;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = alarm.GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();

        startPos = transform.position;
        alarmScript = FindObjectOfType<AlarmScript>();

        agent.destination = Patrol();
    }

    // Update is called once per frame
    void Update()
    {
        if (!haveWeapon)
        {
            agent.destination = weaponStation.position;
            if (agent.remainingDistance < 0.5f)
            {
                haveWeapon = true;

                damage = 50;
            }
        }
        else if (audioSource.isPlaying == true)
        {
            agent.destination = alarmPoint.transform.position;
            if (agent.remainingDistance < 0.5f)
            {
                audioSource.Stop();
                agent.destination = startPos;
                alarmScript.playerTriggered = false;

            }
        }else if (PlayerInSight())
        {
            agent.destination = player.transform.position;
        }
        else if (Searching())
        {
            if(agent.remainingDistance < 0.5f)
            {
                agent.destination = Patrol();
            }
        }
        
    }


    Vector3 Patrol()
    {
        Vector3 destination = transform.position;
        Vector2 randomDirection = Random.insideUnitCircle * 30.0f;

        destination.x += randomDirection.x;
        destination.z += randomDirection.y;

        NavMeshHit navHit;

        NavMesh.SamplePosition(destination, out navHit, 30.0f, NavMesh.AllAreas);

        return navHit.position;
    }

    bool Searching()
    {
        return !PlayerInSight();
    }

    bool PlayerInSight()
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
