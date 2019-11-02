using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MansionDoorOpenLScript : MonoBehaviour
{
    [HideInInspector]
    public bool openL;

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Enemy")
        {
            openL = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Enemy")
        {
            openL = true;
        }
    }
}
