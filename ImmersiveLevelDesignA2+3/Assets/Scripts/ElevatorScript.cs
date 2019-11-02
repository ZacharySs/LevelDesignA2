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
            if (!other.GetComponent<PlayerKeycardScript>().isLoadingLevel)
            {
                elevatorAnimator.SetBool("OpenElevatorDoors", true);
            }
            else
            {
                elevatorAnimator.SetBool("OpenElevatorDoors", false);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            elevatorAnimator.SetBool("OpenElevatorDoors", false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(new Vector3(transform.position.x + 1.5f, transform.position.y + 2f, transform.position.z - 0.5f), new Vector3(4, 6, 4));
    }
}
