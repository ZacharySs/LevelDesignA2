using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorConsoleScript : MonoBehaviour
{
    GameObject[] doors;
    float closestDoorDistance = Mathf.Infinity;

    [HideInInspector]
    public GameObject doorToUnlock;
    private HallwayDoorScript doorScriptToUnlock;

    void Start()
    {
        StartCoroutine(FindClosestDoorCoroutine());
    }

    IEnumerator FindClosestDoorCoroutine()
    {
        doors = GameObject.FindGameObjectsWithTag("HallwayDoor");
        foreach (GameObject door in doors)
        {
            if (door.GetComponent<HallwayDoorScript>().isConsoleUnlockable)
            {
                if (Vector3.Distance(door.transform.position, transform.position) < closestDoorDistance)
                {
                    doorToUnlock = door;
                    closestDoorDistance = Vector3.Distance(door.transform.position, transform.position);
                }
            }
        }

        if (doorToUnlock)
        {
            doorScriptToUnlock = doorToUnlock.GetComponent<HallwayDoorScript>();
        }
        yield return null;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            UnlockDoor();
        }
    }

    private void UnlockDoor()
    {
        doorScriptToUnlock.UnlockDoor();
    }
}
