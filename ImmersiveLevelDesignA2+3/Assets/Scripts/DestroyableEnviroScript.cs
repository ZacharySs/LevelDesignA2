using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableEnviroScript : MonoBehaviour
{
    public float health = 10.0f;
    bool isDamaged = false;
    Vector3 initialPos;
    float damageShakeMagnitude = 0.2f;

    [HideInInspector]
    public GameObject destroyEffect;

    // If this script is attached to a Hallway Door
    HallwayDoorScript hallwayDoorScript;

    void Start()
    {
        initialPos = transform.position;

        destroyEffect = GameObject.Find("GameManager").GetComponent<GameManagerScript>().tileDestructionEffect;

        StartCoroutine(HealthCheckCoroutine());

        if (GetComponentInParent<HallwayDoorScript>())
        {
            hallwayDoorScript = GetComponentInParent<HallwayDoorScript>();
        }
        else
        {
            hallwayDoorScript = null;
        }
    }

    IEnumerator HealthCheckCoroutine()
    {
        while (true)
        {
            if (health <= 0)
            {
                Instantiate(destroyEffect, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }

            yield return new WaitForSeconds(0.0333f);
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
