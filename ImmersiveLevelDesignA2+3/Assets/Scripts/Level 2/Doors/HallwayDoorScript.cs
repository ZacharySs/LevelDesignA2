using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayDoorScript : MonoBehaviour
{
    Animator animator;

    LockLightScript lockLightScript;

    public bool isLocked;

    public bool isConsoleUnlockable;

    public bool isKeycardUnlockable;
    public int requiredKeycards;

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

    /// <summary>
    /// This should be called with a door is damaged by a player weapon. It will permanently stop all door animations and lock the door.
    /// </summary>
    public void StopDoorDamaged()
    {
        animator.StopPlayback();
        animator.enabled = false;
        isLocked = true;
        if (lockLightScript)
            lockLightScript.ChangeDoorLock(isLocked);
    }

    /// <summary>
    ///  This should be called when the player approaches with the necessary number of keycards. OR, if this is a Console door, when the player interacts with the console.
    /// </summary>
    public void UnlockDoor()
    {
        isLocked = false;
        if (lockLightScript)
            lockLightScript.ChangeDoorLock(isLocked);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isLocked && other.tag == "Player" || other.tag == "Enemy")
        {
            animator.SetBool("HallwayDoorOpen", false);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (!isLocked && other.tag == "Player" || other.tag == "Enemy")
        {
            animator.SetBool("HallwayDoorOpen", true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isLocked && isKeycardUnlockable && other.tag == "Player")
        {
            if (other.GetComponent<PlayerKeycardScript>().totalKeycards >= requiredKeycards)
            {
                UnlockDoor();
            }
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

            Gizmos.color = Color.green;
            for (int i = 0; i < requiredKeycards; i++)
            {
                Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y + 4.5f, transform.position.z - (1 * (requiredKeycards/2) - i)), 0.5f);
            }

        }
        if (!isLocked)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, 4f);
        }

    }
}
