using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemyScript : MonoBehaviour
{

    public float health = 10.0f;

    void Start()
    {
        
    }

    void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void takeDamage(float thisDamage)
    {

        health -= thisDamage;
    }
}
