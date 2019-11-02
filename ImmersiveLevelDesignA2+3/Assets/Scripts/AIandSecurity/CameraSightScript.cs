using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSightScript : MonoBehaviour
{

    public bool playerInSight;

    private GameObject playerModel;
    private Renderer playerRend;
    private Color storedColor;

    // HOW TO USE//
    // Drag and Drop camera prefab
    // Be sure to link it with the control panel that relates to it
    // Changing the size and dimensions of the sphere collider will change
    // the camera's are of detection. Make sure it is set to trigger.

    void Awake()
    {
        playerModel = GameObject.FindGameObjectWithTag("PlayerModel");
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
        if (other.gameObject == playerModel)
        {
            playerInSight = false;
            playerRend.material.SetColor("_Color", storedColor);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerModel)
        {
            playerInSight = true;
            playerRend.material.SetColor("_Color", Color.red);

        }
    }
}
