using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorConsoleScript : MonoBehaviour
{
    GameObject[] doors;
    GameObject closestDoor = null;

    public GameObject doorToUnlock;
    private HallwayDoorScript doorScriptToUnlock;

    void Start()
    {
        doors = GameObject.FindGameObjectsWithTag("HallwayDoor");
        foreach (GameObject door in doors)
        {
            if (door.GetComponent<HallwayDoorScript>().isConsoleUnlockable)
            {
                if (closestDoor)
                {
                }
            }
        }

        if (doorToUnlock)
        {
            doorScriptToUnlock = doorToUnlock.GetComponent<HallwayDoorScript>();
        }
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
