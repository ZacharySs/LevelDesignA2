using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrollingAIMovementScript : MonoBehaviour
{
    NavMeshAgent navMesh;
    Rigidbody rigidBody;
    
    public Transform target;
    public Transform[] wayPoints;
    public int currentWaypoint;
    public float speed;
    public float stopDistance;
    public float pauseTimer;
    private float currentTimer;

    private PlayerWeaponScript playerWeaponScript;
    private bool localisFiring;
    private bool alerted;
    private GameObject player;
    private GameObject playerModel;
    private Transform playerPos;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerModel = GameObject.FindGameObjectWithTag("PlayerModel");
        playerPos = player.transform;
        playerWeaponScript = player.GetComponent<PlayerWeaponScript>();
        navMesh = GetComponent<NavMeshAgent>();
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.freezeRotation = true;
        target = wayPoints[currentWaypoint];
        currentTimer = pauseTimer;
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerPos();
        Alerted();
        navMesh.acceleration = speed;
        navMesh.stoppingDistance = stopDistance;

        float distance = Vector3.Distance(transform.position, target.position);

        if(alerted == true)
        {
            target = playerPos;
        }

        // Movement
        else if (distance > stopDistance && wayPoints.Length > 0)
        {
            target = wayPoints[currentWaypoint];
        }

        else if (distance <= stopDistance && wayPoints.Length > 0)
        {
            if (currentTimer > 0)
            {
                currentTimer -= 0.1f;
            }

            if (currentTimer <= 0)
            {
                currentWaypoint++;
                if (currentWaypoint >= wayPoints.Length)
                {
                    currentWaypoint = 0;
                }
                target = wayPoints[currentWaypoint];
                currentTimer = pauseTimer;
            }
        }
        navMesh.SetDestination(target.position);
    }

    private void CheckPlayerPos()
    {
        playerPos = player.transform;
    }
    
    private void Alerted()
    {
        if(playerWeaponScript.isFiring == true)
        {
            alerted = true;
        }
        
    }
}
