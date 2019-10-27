using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableEnviroScript : MonoBehaviour
{
    GameManagerScript gameManagerScript;

    public float health = 10.0f;
    public bool isHardWall = true;

    bool isDamaged = false;
    Vector3 initialPos;
    float damageShakeMagnitude = 0.2f;

    // If this script is attached to a Hallway Door
    public GameObject sparksEffect;
    HallwayDoorScript hallwayDoorScript;
    GameObject lockLight;

    void Start()
    {
        initialPos = transform.position;

        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();

        if (GetComponentInParent<HallwayDoorScript>())
        {
            hallwayDoorScript = GetComponentInParent<HallwayDoorScript>();
        }
        else
        {
            hallwayDoorScript = null;
        }

        if (GetComponentInChildren<LockLightScript>())
        {
            lockLight = GetComponentInChildren<LockLightScript>().gameObject;
        }

        Renderer renderer = GetComponent<Renderer>();
        Material[] sharedMaterials = renderer.sharedMaterials;
        for (int i = 0; i < sharedMaterials.Length; i++)
        {
            if (sharedMaterials[i] == gameManagerScript.softWallMat || sharedMaterials[i] == gameManagerScript.hardWallMat)
            {
                if (isHardWall)
                {
                    renderer.materials[i].SetColor("_EmissionColor", gameManagerScript.hardWallMat.GetColor("_EmissionColor"));
                }
                else
                {
                    renderer.materials[i].SetColor("_EmissionColor", gameManagerScript.softWallMat.GetColor("_EmissionColor"));
                }
            }
        }

        Light[] wallLights = GetComponentsInChildren<Light>();
        for (int i = 0; i < wallLights.Length; i++)
        {
            if (isHardWall)
                wallLights[i].color = gameManagerScript.hardWallLightColor;
            else
                wallLights[i].color = gameManagerScript.softWallLightColor;
        }
    }

    IEnumerator DamageCoroutine()
    {
        transform.position = new Vector3(transform.position.x + Random.Range(-1f, 1f) * damageShakeMagnitude,
                         transform.position.y + Random.Range(-1f, 1f) * damageShakeMagnitude,
                         transform.position.z + Random.Range(-1f, 1f) * damageShakeMagnitude);
        yield return new WaitForSeconds(0.0333f);
        transform.position = initialPos;
    }

    public void takeDamage(float thisDamage)
    {
        if (!isHardWall)
        {
            health -= thisDamage;

            if (hallwayDoorScript && !isDamaged)
            {
                hallwayDoorScript.StopDoorAnim();

                if (lockLight)
                {
                    Vector3 lockLightEuler = lockLight.transform.rotation.eulerAngles;

                    Instantiate(sparksEffect, lockLight.transform.position, Quaternion.Euler(lockLightEuler.x, lockLightEuler.y - 90, lockLightEuler.z), lockLight.transform);
                    Instantiate(sparksEffect, lockLight.transform.position + (Vector3.forward * 0.5f), Quaternion.Euler(lockLightEuler.x, lockLightEuler.y + 90, lockLightEuler.z), lockLight.transform);
                    Debug.Log("Sparks Instantiated.");
                }

                isDamaged = true;
            }

            StartCoroutine(DamageCoroutine());
        }

        /*
        transform.position = new Vector3(transform.position.x + Mathf.Sign(Random.Range(-1, 1)) * damageShakeMagnitude,
                                 transform.position.y + Mathf.Sign(Random.Range(-1, 1)) * damageShakeMagnitude,
                                 transform.position.z + Mathf.Sign(Random.Range(-1, 1)) * damageShakeMagnitude);
        */
    }
}
