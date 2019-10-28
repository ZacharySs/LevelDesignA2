using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDoorScript : MonoBehaviour
{
    Animator animator;

    LockLightScript lockLightScript;

    public bool isLocked;

    DestroyableEnviroScript doorScript;
    float doorInitialHealth;

    void Start()
    {
        animator = GetComponent<Animator>();

        doorScript = gameObject.transform.Find("L2_SingleDoor_Model").GetComponent<DestroyableEnviroScript>();
        doorInitialHealth = doorScript.health;

        lockLightScript = GetComponentInChildren<LockLightScript>();

        lockLightScript.ChangeDoorLock(isLocked);
    }

    public void StopDoorAnim()
    {
        animator.StopPlayback();
        animator.enabled = false;
        if (lockLightScript)
            lockLightScript.ChangeDoorLock(true);
    }

    public void ChangeDoorLock()
    {
        isLocked = !isLocked;
        if (lockLightScript)
            lockLightScript.ChangeDoorLock(isLocked);

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
