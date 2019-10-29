using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeycardScript : MonoBehaviour
{
    [HideInInspector]
    public int totalKeycards = 0;

    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "KeycardPickup")
        {
            totalKeycards++;
            Destroy(other.gameObject);
        }
    }
}
