using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public GameObject avatar;

    //Movement
    public float moveSpeed = 5;
    private Vector3 playerPosition;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
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
        playerPosition = transform.position;

        //Forwards and Back
        if (Input.GetKey("w"))
        {
            playerPosition.z = playerPosition.z - moveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey("s"))
        {
            playerPosition.z = playerPosition.z + moveSpeed * Time.deltaTime;
        }

        //Strafing 
        if (Input.GetKey("a"))
        {
            playerPosition.x = playerPosition.x + moveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey("d"))
        {
            playerPosition.x = playerPosition.x - moveSpeed * Time.deltaTime;
        }

        transform.position = playerPosition;
        rb.velocity = new Vector3(0, 0, 0);   //Freeze velocity
    }
}
