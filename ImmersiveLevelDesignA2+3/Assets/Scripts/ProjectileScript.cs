using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float speed = 30.0f;
    public float damage = 5.0f;

    public float lifeTime = 1.5f;

    //Effects
    public GameObject hitEffect;
    public GameObject destroyEffect;

    [HideInInspector]
    public GameObject fireLocationOrigin;

    private void Start()
    {
        Destroy(this.gameObject, lifeTime);

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit rayHit, Time.fixedDeltaTime * (speed + 3)))
        {
            if (rayHit.transform.tag == "Enemy")
            {
                rayHit.transform.GetComponent<PatrollingEnemyScript>().takeDamage(damage);
                if (hitEffect != null)
                    Instantiate(hitEffect, rayHit.point, transform.rotation);

                Destroy(this.gameObject);
            }
            else if (rayHit.transform.tag == "Player")
            {
            }
            else
            {
                if (hitEffect != null)
                    Instantiate(hitEffect, rayHit.point, transform.rotation);

                if (rayHit.transform.GetComponent<DestroyableEnviroScript>())
                {
                    rayHit.transform.GetComponent<DestroyableEnviroScript>().takeDamage(damage);
                    if (rayHit.transform.GetComponent<DestroyableEnviroScript>().health <= 0)
                    {
                        Destroy(rayHit.transform.gameObject);
                        Instantiate(destroyEffect, rayHit.point, rayHit.transform.rotation);
                        Debug.Log("Explosion!");
                    }
                }
                else
                {
                    Destroy(this.gameObject);
                }
                Destroy(this.gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit rayHit, Time.fixedDeltaTime * (speed + 3)))
        {
            if (rayHit.transform.tag == "Enemy")
            {
                rayHit.transform.GetComponent<PatrollingEnemyScript>().takeDamage(damage);
                if (hitEffect != null)
                    Instantiate(hitEffect, rayHit.point, transform.rotation);

                Destroy(this.gameObject);
            }
            else if (rayHit.transform.tag == "Player")
            {
            }
            else
            {
                if (hitEffect != null)
                    Instantiate(hitEffect, rayHit.point, transform.rotation);

                if (rayHit.transform.GetComponent<DestroyableEnviroScript>())
                {
                    rayHit.transform.GetComponent<DestroyableEnviroScript>().takeDamage(damage);
                    if (rayHit.transform.GetComponent<DestroyableEnviroScript>().health <= 0)
                    {
                        Destroy(rayHit.transform.gameObject);
                        Instantiate(destroyEffect, rayHit.point, rayHit.transform.rotation);
                        Debug.Log("Explosion!");
                    }
                }
                else
                {
                    Destroy(this.gameObject);
                }
                Destroy(this.gameObject);
            }
        }
        transform.position += Time.fixedDeltaTime * speed * transform.forward;
    }
    /*
    private void OnTriggerEnter(Collider otherObject)
    {
        if (otherObject.tag == "Enemy")
        {
            otherObject.GetComponent<PatrollingEnemyScript>().takeDamage(damage);
            if (hitEffect)
                Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
        else if (otherObject.tag == "Environment")
        {
            if (hitEffect)
                Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
    */
}
