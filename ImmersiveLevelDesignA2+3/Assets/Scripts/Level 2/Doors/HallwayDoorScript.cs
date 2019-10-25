using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayDoorScript : MonoBehaviour
{
    Animator animator;

    LockLightScript lockLightScript;

    public bool isLocked;

    void Start()
    {
        animator = GetComponent<Animator>();

        lockLightScript = GetComponentInChildren<LockLightScript>();

        lockLightScript.ChangeDoorLock(isLocked);

    }

    public void ChangeDoorLock()
    {
        isLocked = !isLocked;
        lockLightScript.ChangeDoorLock(isLocked);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isLocked)
        {
            if (other.tag == "Player" || other.tag == "Enemy")
            {
                animator.SetTrigger("CycleHallwayDoor");
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!isLocked)
        {
            if (other.tag == "Player" || other.tag == "Enemy")
            {
                animator.SetTrigger("CycleHallwayDoor");
            }
        }
    }
}
