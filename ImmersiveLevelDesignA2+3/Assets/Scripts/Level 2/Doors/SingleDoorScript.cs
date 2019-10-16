using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDoorScript : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            animator.SetTrigger("CycleSingleDoor");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            animator.SetTrigger("CycleSingleDoor");
        }
    }
}
