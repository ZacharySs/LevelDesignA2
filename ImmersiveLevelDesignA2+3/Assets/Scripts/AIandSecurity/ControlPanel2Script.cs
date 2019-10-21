using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanel2Script : MonoBehaviour
{
    public bool playerInRange;
    public bool CameraDisabled;
    public bool TurretDisabled;
    public int DisbaleTime;


    public GameObject[] securityCameras;
    public GameObject[] turrets;

    private BoxCollider[] turretTriggers;
    private LineRenderer[] turretLines;

    private SphereCollider[] securityCameraTriggers;
    private Light[] securityCameraLights;

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

        for (int i = 0; i < securityCameras.Length; i++)
        {
            securityCameraTriggers[i] = securityCameras[i].GetComponent<SphereCollider>();
            securityCameraLights[i] = securityCameras[i].GetComponentInChildren<Light>();
        }

        for (int i = 0; i < turrets.Length; i++)
        {
            turretTriggers[i] = turrets[i].GetComponent<BoxCollider>();
            turretLines[i] = turrets[i].GetComponent<LineRenderer>();
        }


        //securityCameraTrigger = securityCameras.GetComponent<SphereCollider>();
        //securityCameraLight = securityCameras.GetComponentInChildren<Light>();
        //turretTrigger = turrets.GetComponent<BoxCollider>();
        //turretLine = turrets.GetComponent<LineRenderer>();
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
        for (int i = 0; i < securityCameras.Length; i++)
        {
            securityCameraTriggers[i].enabled = true;
            securityCameraLights[i].enabled = true;
        }

        for (int i = 0; i < turrets.Length; i++)
        {
            turretTriggers[i].enabled = true;
            turretLines[i].enabled = true;
        }

        //securityCameraTrigger.enabled = true;
        //securityCameraLight.enabled = true;
        //turretTrigger.enabled = true;
        //turretLine.enabled = true;
        CameraDisabled = false;
        TurretDisabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
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
        if (playerInRange == true & Input.GetKeyDown(KeyCode.E))
        {
            for (int i = 0; i < securityCameras.Length; i++)
            {
                securityCameraTriggers[i].enabled = false;
                securityCameraLights[i].enabled = false;
            }

            for (int i = 0; i < turrets.Length; i++)
            {
                turretTriggers[i].enabled = false;
                turretLines[i].enabled = false;
            }

            //securityCameraTrigger.enabled = false;
            //securityCameraLight.enabled = false;
            //turretLine.enabled = false;
            //turretTrigger.enabled = false;
            CameraDisabled = true;
            TurretDisabled = true;
            StartCoroutine(Reactivate());
        }
    }
}
