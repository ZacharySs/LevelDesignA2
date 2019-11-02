using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public GameObject avatar;

    public float health = 100f;

    IsometricCameraScript isoCamScript;

    //Movement
    public float moveSpeed = 50;
    private Vector3 playerPosition;
    private Rigidbody rb;

    CharacterController charController;
    Vector3 moveDir;

    bool canWindowTeleport = false;
    public GameObject interactionText;


    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        if (GameObject.Find("Spawn") && SceneManager.GetActiveScene().name != "Level1")
        {
            transform.position = GameObject.Find("Spawn").transform.position;
        }
        charController = GetComponent<CharacterController>();

        if (interactionText)
        {
            interactionText.SetActive(false);
        }

        isoCamScript = Camera.main.GetComponent<IsometricCameraScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isoCamScript.inCutscene)
        {
            Movement();
        }
    }

    public void takeDamage(float thisDamage)
    {
        health -= thisDamage;

        if (health <= 0)
        {
            GetComponent<PlayerKeycardScript>().RestartLevel();
        }
        health = Mathf.Clamp(health, 0f, 100f);
    }

    void FixedUpdate()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);

        // Generate a ray from the cursor position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Determine the point where the cursor ray intersects the plane.
        // This will be the point that the object must look towards to be looking at the mouse.
        // Raycasting to a Plane object only gives us a distance, so we'll have to take the distance,
        //   then find the point along that ray that meets that distance.  This will be the point
        //   to look at.
        float hitdist = 0.0f;
        // If the ray is parallel to the plane, Raycast will return false.
        if (playerPlane.Raycast(ray, out hitdist))
        {
            // Get the point along the ray that hits the calculated distance.
            Vector3 targetPoint = ray.GetPoint(hitdist);

            //Update GameManager mouse location
            //gameManager.cursorLocation = targetPoint;
            // Determine the target rotation.  This is the rotation if the transform looks at the target point.
            //Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

            // Smoothly rotate towards the target point.
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.time);

            transform.LookAt(targetPoint);
        }
    }

    void Movement()
    {
        //Calculating direction of movement + how fast the player should be moving
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            //playerPosition.z = playerPosition.z - moveSpeed * Time.deltaTime;
            Vector3 lateral = (Vector3.left + Vector3.back) * Input.GetAxis("Vertical");

            Vector3 vertical = (Vector3.left + Vector3.forward) * Input.GetAxis("Horizontal");

            moveDir = (lateral + vertical).normalized;


            moveDir *= moveSpeed * Mathf.Clamp01(Mathf.Abs(Input.GetAxis("Horizontal")) + Mathf.Abs(Input.GetAxis("Vertical")));

        }
        else if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            moveDir = new Vector3(0, 0, 0);
        }

        charController.SimpleMove(moveDir);

        //transform.position = playerPosition;
        //rb.velocity = new Vector3(0, 0, 0);   //Freeze velocity

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (canWindowTeleport)
            {
                if (GameObject.Find("WindowTeleportLocation"))
                    transform.position = GameObject.Find("WindowTeleportLocation").transform.position;
                canWindowTeleport = false;
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "WindowTrigger")
        {
            if (interactionText)
            {
                interactionText.SetActive(true);
            }
            canWindowTeleport = true;
        }

        if (other.GetComponent<DoorConsoleScript>())
        {
            if (interactionText)
            {
                interactionText.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "WindowTrigger")
        {
            if (interactionText)
            {
                interactionText.SetActive(false);
            }
            canWindowTeleport = true;
        }

        if (other.GetComponent<DoorConsoleScript>())
        {
            if (interactionText)
            {
                interactionText.SetActive(false);
            }
        }
    }
}
