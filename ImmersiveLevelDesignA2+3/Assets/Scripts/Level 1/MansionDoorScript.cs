using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MansionDoorScript : MonoBehaviour
{
    Animator animator;
    MansionDoorOpenLScript openLScript;
    MansionDoorOpenRScript openRScript;

    public bool isFlipped;

    void Start()
    {
        animator = GetComponent<Animator>();
        openLScript = GetComponentInChildren<MansionDoorOpenLScript>();
        openRScript = GetComponentInChildren<MansionDoorOpenRScript>();
    }

    private void Update()
    {
        if (openLScript.openL)
        {
            if (!isFlipped)
            {
                animator.SetBool("MansionDoorOpenL", true);
            }
            else
            {
                animator.SetBool("MansionDoorOpenR", true);
            }
        }
        if (openRScript.openR)
        {
            if (!isFlipped)
            {
                animator.SetBool("MansionDoorOpenR", true);
            }
            else
            {
                animator.SetBool("MansionDoorOpenL", true);
            }
        }

        if (!openRScript.openR && !openLScript.openL)
        {
            animator.SetBool("MansionDoorOpenL", false);
            animator.SetBool("MansionDoorOpenR", false);
        }
    }
}
