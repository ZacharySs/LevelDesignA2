using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanelScript : MonoBehaviour
{
    public bool playerInRange;
    public bool CameraDisabled;
    public bool TurretDisabled;
    public int DisbaleTime;
    

    public GameObject securityCamera;
    public GameObject turret;

    private BoxCollider turretTrigger;
    private LineRenderer turretLine;

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
        TurretDisabled = false;
        securityCameraTrigger = securityCamera.GetComponent<SphereCollider>();
        securityCameraLight = securityCamera.GetComponentInChildren<Light>();
        turretTrigger = turret.GetComponent<BoxCollider>();
        turretLine = turret.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Disable();   
    }

    IEnumerator Reactivate()
    {
        yield return new WaitForSeconds(DisbaleTime);
        Enable();
    }

    private void Enable()
    {
        securityCameraTrigger.enabled = true;
        securityCameraLight.enabled = true;
        turretTrigger.enabled = true;
        turretLine.enabled = true;
        CameraDisabled = false;
        TurretDisabled = false;
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

    private void Disable()
    {
        if (playerInRange == true && Input.GetKeyDown(KeyCode.E) && securityCameraTrigger.enabled == true && turretTrigger.enabled == true)
        {
            securityCameraTrigger.enabled = false;
            securityCameraLight.enabled = false;
            turretLine.enabled = false;
            turretTrigger.enabled = false;
            CameraDisabled = true;
            TurretDisabled = true;
            StartCoroutine(Reactivate());
        }
    }
}
