using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockLightScript : MonoBehaviour
{
    public Material[] lockLightMaterial; //[0] is red light, [1] is blue light
    int lockLightMatIndex;
    HallwayDoorScript hallwayDoorScript;
    [HideInInspector]
    public bool isLocked = false; // Used in DestroyableEnviroScript to decide which lockLightMaterial to use.
    MeshRenderer meshRenderer;
    Light[] lockLights;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        lockLights = GetComponentsInChildren<Light>();
        hallwayDoorScript = GetComponentInParent<HallwayDoorScript>();
    }

    public void ChangeDoorLock(bool newLockState)
    {
        if (newLockState)
        {
            //isLocked = true;
            if (hallwayDoorScript.isConsoleUnlockable)
                lockLightMatIndex = 0;
            else if (hallwayDoorScript.isKeycardUnlockable)
                lockLightMatIndex = 1;
            else if (hallwayDoorScript.isLocked)
                lockLightMatIndex = 2;

            foreach (var lockLight in lockLights)
            {
                lockLight.color = lockLightMaterial[lockLightMatIndex].GetColor("_EmissionColor") * 1;
            }
            
            /*
            Material[] oldMaterials = meshRenderer.materials;
            Material[] newMaterials = { oldMaterials[0], lockLightMaterial[0] };

            meshRenderer.materials = newMaterials;
            */
        }
        else
        {
            //isLocked = false;
            lockLightMatIndex = 3;

            foreach (var lockLight in lockLights)
            {
                lockLight.color = lockLightMaterial[lockLightMatIndex].GetColor("_EmissionColor") * 1;
            }
            /*
            Material[] oldMaterials = meshRenderer.materials;
            Material[] newMaterials = { oldMaterials[0], lockLightMaterial[1] };

            meshRenderer.materials = newMaterials;
            */
        }
    }

}
