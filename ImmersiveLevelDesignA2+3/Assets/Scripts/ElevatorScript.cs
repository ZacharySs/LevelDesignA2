using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorScript : MonoBehaviour
{
    Animator elevatorAnimator;

    void Start()
    {
        elevatorAnimator = GetComponent<Animator>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            elevatorAnimator.SetBool("OpenElevatorDoors", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            elevatorAnimator.SetBool("OpenElevatorDoors", false);
        }
    }
}
