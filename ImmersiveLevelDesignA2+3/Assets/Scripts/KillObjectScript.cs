using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillObjectScript : MonoBehaviour
{
    public float lifeTime = 3f;
    private void Start()
    {
        Destroy(this.gameObject, lifeTime);
    }
}
