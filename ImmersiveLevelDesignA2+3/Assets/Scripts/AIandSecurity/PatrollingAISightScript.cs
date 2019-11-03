using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrollingAISightScript : MonoBehaviour
{
    public float fieldOfViewAngle = 100f;
    public bool playerInSight;

    private NavMeshAgent nav;
    private SphereCollider col;
    private GameObject player;
    [SerializeField]
    private GameObject playerModel;

    private Renderer playerRend;
    private Color storedColor;

    /* HOW TO USE 
     * -Stationary Guards-
     * Drag and Drop into world
     * They will detect the player in front of them within a certain angle
     * 
     * -Patrolling Guards-
     * Drag and Drop into World
     * They will also need a AI Way Point prefab as well
     * The patrolling AI will travel from waypoint to waypoint 
     * in the order that you set teh way points up on the inspector.
     * To do this go to the GaurdPatrol and drag the way point objetcs
     * to the corresponding element slot in the inspector.
     * Element 0 being the first point it will travel to and and flooping through the 
     * list until the last and then repeating the list.
    */ 

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        col = GetComponentInChildren<SphereCollider>();
        //col = GetComponent<SphereCollider>();
        //Debug.Log(col);

        player = GameObject.FindGameObjectWithTag("Player");
        playerModel = GameObject.FindGameObjectWithTag("PlayerModel");
        //Debug.Log(player);
        
        //Debug.Log(playerModel);
        playerRend = playerModel.GetComponent<MeshRenderer>();
        storedColor = playerRend.material.GetColor("_Color");
    }

    // Start is called before the first frame update
    void Start()
    {

        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInSight = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInSight = false;
            playerRend.material.SetColor("_Color", storedColor);
            //Debug.Log("Not Detected");

            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            if (angle < fieldOfViewAngle * 0.5f)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position, direction.normalized, out hit, col.radius))
                {
                    
                    if (hit.collider.gameObject == player)
                    {
                        playerInSight = true;
                        playerRend.material.SetColor("_Color", Color.red);
                        //Debug.Log("Detected");  
                    }
                }
            }
        }
    }

}
