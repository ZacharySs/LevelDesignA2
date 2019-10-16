using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanelScript : MonoBehaviour
{
    public bool playerInRange;
    public bool CameraDisabled;
    public int CameraDisbaleTime;
    

    public GameObject securityCamera;

    private SphereCollider securityCameraTrigger;
    private Light securityCameraLight;

    // HOW TO USE //
    // Drag and drop the control panel prefab in the world
    // In the inspector you will need to drag the camera object
    // to the Security Camera property in the inspector to link them
    // together.
    // **IMPORTANT** - Do not override the control panel prefab as this will link
    // all control panels to the same camera.
    // You can also alter how long the camera will be disabled for via the 
    // Camera Disable Time property in the inspector

    // Start is called before the first frame update
    void Start()
    {
        CameraDisabled = false;
        securityCameraTrigger = securityCamera.GetComponent<SphereCollider>();
        securityCameraLight = securityCamera.GetComponentInChildren<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        DisableCamera();   
    }

    IEnumerator ReactivateCamera()
    {
        yield return new WaitForSeconds(CameraDisbaleTime);
        EnableCamera();
    }

    private void EnableCamera()
    {
        securityCameraTrigger.enabled = true;
        securityCameraLight.enabled = true;
        CameraDisabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInRange = false;
        }
    }

    private void DisableCamera()
    {
        if(playerInRange == true & Input.GetKeyDown(KeyCode.E) & securityCameraTrigger.enabled == true)
        {
            securityCameraTrigger.enabled = false;
            securityCameraLight.enabled = false;
            CameraDisabled = true;
            StartCoroutine(ReactivateCamera());
        }
    }
}
