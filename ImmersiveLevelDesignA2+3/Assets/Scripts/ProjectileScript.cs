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

    private void Start()
    {
        Destroy(this.gameObject, lifeTime);
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
                if (hitEffect)
                    Instantiate(hitEffect, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
            else if (rayHit.transform.tag == "Elevator")
            {
                if (hitEffect)
                    Instantiate(hitEffect, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
            else if (rayHit.transform.tag == "Player")
            {
                Destroy(this.gameObject);
            }
            else
            {
                if (hitEffect)
                    Instantiate(hitEffect, transform.position, transform.rotation);
                if (rayHit.transform.GetComponent<DestroyableEnviroScript>())
                {
                    rayHit.transform.GetComponent<DestroyableEnviroScript>().takeDamage(damage);
                }
                else
                {
                    Destroy(rayHit.transform.gameObject);
                }
                Destroy(this.gameObject);
            }

            Debug.Log(rayHit.transform.gameObject.name);
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
