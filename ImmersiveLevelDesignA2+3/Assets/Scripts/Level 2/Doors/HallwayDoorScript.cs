﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayDoorScript : MonoBehaviour
{
    Animator animator;

    LockLightScript lockLightScript;

    public bool isLocked;

    public bool isConsoleUnlockable;

    public bool isKeycardUnlockable;

    DestroyableEnviroScript doorLScript;
    float doorLInitialHealth;
    DestroyableEnviroScript doorRScript;
    float doorRInitialHealth;

    void Start()
    {
        animator = GetComponent<Animator>();

        doorLScript = gameObject.transform.Find("L2_HallwayDoor_L").GetComponent<DestroyableEnviroScript>();
        doorLInitialHealth = doorLScript.health;
        doorRScript = gameObject.transform.Find("L2_HallwayDoor_R").GetComponent<DestroyableEnviroScript>();
        doorRInitialHealth = doorRScript.health;
        
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
        if (!isLocked && other.tag == "Player")
        {
            animator.SetTrigger("CycleHallwayDoor");
        }
        else if (other.tag == "Enemy")
        {
            animator.SetTrigger("CycleHallwayDoor");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!isLocked && other.tag == "Player")
        {
            animator.SetTrigger("CycleHallwayDoor");
        }
        else if (other.tag == "Enemy")
        {
            animator.SetTrigger("CycleHallwayDoor");
        }
    }

    private void OnDrawGizmos()
    {
        if (isLocked)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position, new Vector3(4, 4, 4));
        }
        if (isConsoleUnlockable)
        {

            Gizmos.color = new Vector4(1f, 0.5f, 0f, 1f);
            Gizmos.DrawWireCube(transform.position, new Vector3(4, 4, 4));
        }
        if (isKeycardUnlockable)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, new Vector3(4, 4, 4));
        }
        if (!isLocked)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, 4f);
        }

    }
}
