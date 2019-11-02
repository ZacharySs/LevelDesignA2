using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MansionDoorOpenRScript : MonoBehaviour
{
    [HideInInspector]
    public bool openR;

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Enemy")
        {
            openR = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Enemy")
        {
            openR = true;
        }
    }
}
