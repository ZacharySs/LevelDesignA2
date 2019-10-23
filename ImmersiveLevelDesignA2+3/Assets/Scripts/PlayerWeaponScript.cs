using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponScript : MonoBehaviour
{
    public GameObject projectile;
    private float fireTimer;
    private float fireTime = 0.1f;
    public GameObject fireLocation;

    int weaponNum = 0;
    static bool hasMP5 = false;
    float spreadMP5 = 3f;
    static bool hasM4 = false;
    float spreadM4 = 1f;
    float newWeaponSpread;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && weaponNum > 0)
        {
            if (hasM4 || hasMP5)
                FireProjectile();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && hasMP5)
        {
            weaponNum = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && hasM4)
        {
            weaponNum = 2;
        }
    }

    void FireProjectile()
    {

        Vector3 targetPoint;

        // Generate a plane that intersects the transform's position with an upwards normal.
        Plane playerPlane = new Plane(Vector3.up, transform.position);

        // Generate a ray from the cursor position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Determine the point where the cursor ray intersects the plane.
        float hitdist = 0.0f;
        // If the ray is parallel to the plane, Raycast will return false.
        if (playerPlane.Raycast(ray, out hitdist))
        {
            // Get the point along the ray that hits the calculated distance.
            targetPoint = ray.GetPoint(hitdist);

            fireLocation.transform.LookAt(targetPoint);

            if (weaponNum == 1)
            {
                newWeaponSpread = Random.Range(-spreadMP5, spreadMP5);
            }
            else if (weaponNum == 2)
            {
                newWeaponSpread = Random.Range(-spreadM4, spreadM4);
            }

            fireLocation.transform.eulerAngles = new Vector3(fireLocation.transform.eulerAngles.x,
                                                             fireLocation.transform.eulerAngles.y + newWeaponSpread,
                                                             fireLocation.transform.eulerAngles.z);

            if (Time.time > fireTimer)
            {

                Instantiate(projectile, fireLocation.transform.position + (fireLocation.transform.forward * 0.25f), fireLocation.transform.rotation);

                fireTimer = Time.time + fireTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MP5Pickup")
        {
            hasMP5 = true;
        }

        if (other.tag == "M4Pickup")
        {
            hasM4 = true;
        }
    }
}
