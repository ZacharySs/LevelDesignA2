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
    
    private Renderer playerRend;
    private Color storedColor;

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        col = GetComponent<SphereCollider>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerRend = player.GetComponent<MeshRenderer>();
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
            Debug.Log("Not Detected");

            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            if (angle < fieldOfViewAngle * 0.5f)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius))
                {
                    
                    if (hit.collider.gameObject == player)
                    {
                        playerInSight = true;
                        playerRend.material.SetColor("_Color", Color.red);
                        Debug.Log("Detected");  
                    }
                }
            }
        }
    }
}
