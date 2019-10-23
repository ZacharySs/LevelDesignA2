using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float speed = 10.0f;
    public float damage = 5.0f;

    public float lifeTime = 1.5f;

    //Effects
    public GameObject hitEffect;

    private void Start()
    {
        Destroy(this.gameObject, lifeTime);
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        transform.position += Time.deltaTime * speed * transform.forward;
    }

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
}
