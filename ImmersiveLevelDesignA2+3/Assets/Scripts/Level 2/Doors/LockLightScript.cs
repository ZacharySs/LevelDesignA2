using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockLightScript : MonoBehaviour
{
    public Material[] lockLightMaterial; //[0] is red light, [1] is blue light
    [HideInInspector]
    public bool isLocked = false; // Used in DestroyableEnviroScript to decide which lockLightMaterial to use.
    MeshRenderer meshRenderer;
    Light[] lockLights;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        lockLights = GetComponentsInChildren<Light>();
    }

    public void ChangeDoorLock(bool newLockState)
    {
        if (newLockState)
        {
            isLocked = true;
            foreach (var lockLight in lockLights)
            {
                lockLight.color = lockLightMaterial[0].GetColor("_EmissionColor");
            }
            Material[] oldMaterials = meshRenderer.materials;
            Material[] newMaterials = { oldMaterials[0], lockLightMaterial[0] };

            meshRenderer.materials = newMaterials;
        }
        else
        {
            isLocked = false;
            foreach (var lockLight in lockLights)
            {
                lockLight.color = lockLightMaterial[1].GetColor("_EmissionColor");
            }
            Material[] oldMaterials = meshRenderer.materials;
            Material[] newMaterials = { oldMaterials[0], lockLightMaterial[1] };

            meshRenderer.materials = newMaterials;
        }
    }

}
