using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableEnviroScript : MonoBehaviour
{
    public float health = 10.0f;
    bool isDamaged = false;
    Vector3 initialPos;
    float damageShakeMagnitude = 0.2f;

    public GameObject sparksEffect;

    // If this script is attached to a Hallway Door
    HallwayDoorScript hallwayDoorScript;
    GameObject lockLight;

    void Start()
    {
        initialPos = transform.position;

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

        /*
        transform.position = new Vector3(transform.position.x + Mathf.Sign(Random.Range(-1, 1)) * damageShakeMagnitude,
                                 transform.position.y + Mathf.Sign(Random.Range(-1, 1)) * damageShakeMagnitude,
                                 transform.position.z + Mathf.Sign(Random.Range(-1, 1)) * damageShakeMagnitude);
        */
    }
}
