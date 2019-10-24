using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricCameraScript : MonoBehaviour
{
    public GameObject player;

    public float height;
    public float zDisp;
    [HideInInspector]
    public float camShake;

    public float cameraSpeed = 1.0f;
    private Vector3 newCamPos;

    // Use this for initialization
    void Start()
    {
        transform.position = new Vector3(transform.position.x, height, transform.position.z - zDisp);
    }

    // Update is called once per frame
    void Update()
    {

        //If Player Alive...
        if (player)
        {
            CameraMovement();
        }
    }


    //Camera Pans (Lerps) towards position above player avatar
    void CameraMovement()
    {

        newCamPos = player.transform.position;

        newCamPos.y = player.transform.position.y + height;
        newCamPos.z = player.transform.position.z - zDisp + Random.Range(-camShake, camShake);
        newCamPos.x = player.transform.position.x - zDisp + Random.Range(-camShake, camShake);

        transform.position = Vector3.Lerp(transform.position, newCamPos, cameraSpeed * Time.deltaTime);

        camShake = 0f;
    }
}
