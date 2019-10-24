using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableEnviroScript : MonoBehaviour
{
    public float health = 10.0f;
    Vector3 initialPos;
    float damageShakeMagnitude = 0.2f;

    Animation anim;

    void Start()
    {
        initialPos = transform.position;
        if (GetComponent<Animation>())
            anim = GetComponent<Animation>();
        StartCoroutine(HealthCheckCoroutine());
    }

    IEnumerator HealthCheckCoroutine()
    {
        while (true)
        {
            if (health <= 0)
            {
                Destroy(this.gameObject);
            }
            else
            {
                //transform.position = initialPos;
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

        StartCoroutine(DamageCoroutine());

        /*
        transform.position = new Vector3(transform.position.x + Mathf.Sign(Random.Range(-1, 1)) * damageShakeMagnitude,
                                 transform.position.y + Mathf.Sign(Random.Range(-1, 1)) * damageShakeMagnitude,
                                 transform.position.z + Mathf.Sign(Random.Range(-1, 1)) * damageShakeMagnitude);
        */
    }
}
