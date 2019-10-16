using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSightScript : MonoBehaviour
{
    public bool playerInSight;

    private GameObject player;
    private BoxCollider col;
    private Renderer playerRend;
    private Color storedColor;
    private LineRenderer lineRenderer;
    
    // HOW TO USE //
    // Drag and Drop the turret into the world
    // Editing the length of the Box Collider will also
    // alter the length of the line renderer. So if you wish to make
    // the detection range longer, just change the box collider length
    // in the inspector.

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRend = player.GetComponent<MeshRenderer>();
        storedColor = playerRend.material.GetColor("_Color");
        col = GetComponent<BoxCollider>();

        lineRenderer = this.GetComponent<LineRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer.useWorldSpace = true;
        lineRenderer.SetPosition(0, transform.position + transform.up);
        lineRenderer.SetPosition(1, transform.forward * col.size.z + (transform.position + transform.up));
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
            playerRend.material.SetColor("_Color", storedColor);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInSight = true;
            playerRend.material.SetColor("_Color", Color.red);

        }    
    }
}
