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
    }

    // Update is called once per frame
    void Update()
    {
        Switch(false);
    }

    IEnumerator Reactivate()
    {
        yield return new WaitForSeconds(DisbaleTime);
        Switch(true);      
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

    private void Switch(bool active)
    {
        if ((playerInRange == true & Input.GetKeyDown(KeyCode.E) & active == false) | active == true)
        {
            for (int i = 0; i < securityCameras.Length; i++)
            {
                securityCameras[i].GetComponent<SphereCollider>().enabled = active;
                securityCameras[i].GetComponentInChildren<Light>().enabled = active;
            }

            for (int i = 0; i < turrets.Length; i++)
            {
                turrets[i].GetComponent<BoxCollider>().enabled = active;
                turrets[i].GetComponent<LineRenderer>().enabled = active;
            }

            CameraDisabled = !active;
            TurretDisabled = !active;
            if(active == false)
            {
                Debug.Log("jjj");
                StartCoroutine(Reactivate());
            }            
        }
    }
}
