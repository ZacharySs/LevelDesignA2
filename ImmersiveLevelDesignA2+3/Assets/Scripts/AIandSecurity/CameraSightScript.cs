using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSightScript : MonoBehaviour
{

    public bool playerInSight;

    private GameObject player;
    private Renderer playerRend;
    private Color storedColor;


    void Awake()
    {
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
