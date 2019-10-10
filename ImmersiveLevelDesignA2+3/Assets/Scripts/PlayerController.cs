using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 5.0f;
    private Rigidbody playerRB;

    private Vector3 moveInput;
    private Vector3 moveVelocity;

    private Camera playerCam;

    //public GunController gun;

    public bool isSprinting;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerCam = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Sprint();
        Movement();

        Ray cameraRay = playerCam.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    gun.isFiring = true;
        //}

        //if (Input.GetMouseButtonUp(0))
        //{
        //    gun.isFiring = false;
        //}

    }

    private void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && isSprinting == false)
        {
            isSprinting = true;
        }

        else if (Input.GetKeyDown(KeyCode.LeftShift) && isSprinting == true)
        {  
            isSprinting = false;
        }        
    }

    private void Movement()
    {
        if (isSprinting == true)
        {
            moveSpeed = 10.0f;
        }

        else if (isSprinting == false)
        {
            moveSpeed = 5.0f;
        }
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * moveSpeed;      
    }

    void FixedUpdate()
    {
        playerRB.velocity = moveVelocity;    
    }
}
