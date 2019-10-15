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
    [SerializeField]
    private float currentTimer;

    // Start is called before the first frame update
    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.freezeRotation = true;
        target = wayPoints[currentWaypoint];
        currentTimer = pauseTimer;
    }

    // Update is called once per frame
    void Update()
    {
        navMesh.acceleration = speed;
        navMesh.stoppingDistance = stopDistance;

        float distance = Vector3.Distance(transform.position, target.position);

        // Movement
        if (distance > stopDistance && wayPoints.Length > 0)
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
}
