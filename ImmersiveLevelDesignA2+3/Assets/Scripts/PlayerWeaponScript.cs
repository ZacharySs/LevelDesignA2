using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponScript : MonoBehaviour
{
    public GameObject projectile;
    private float fireTimer;
    private float fireTime = 0.05f;
    public GameObject fireLocation;

    int weaponNum = 0;
    float spreadSingle = 10f;

    static bool hasMP5 = false;
    float spreadMP5 = 3f;
    static bool hasM4 = false;
    float spreadM4 = 1f;
    float newWeaponSpread;

    AudioSource gunAudio;
    public AudioClip[] gunSounds;
    bool isFiring = false;

    IsometricCameraScript isoCamScript;
    public float camShakeMagnitude = 0.5f;
    float camShakeMult = 1f;

    void Start()
    {
        gunAudio = GetComponent<AudioSource>();
        isoCamScript = Camera.main.GetComponent<IsometricCameraScript>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            FireProjectile();
            if (!isFiring)
                StartFiringAudio();
        }

        if (Input.GetMouseButtonUp(0) && isFiring)
        {
            gunAudio.PlayOneShot(gunSounds[2], 0.8f);
            StartCoroutine(StopFiringAudio());
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponNum = 0;
            gunAudio.volume = 0.25f;
            camShakeMult = 1f;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && hasMP5)
        {
            weaponNum = 1;
            gunAudio.volume = 0.4f;
            camShakeMult = 2f;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && hasM4)
        {
            weaponNum = 2;
            gunAudio.volume = 0.65f;
            camShakeMult = 3f;
        }
    }

    IEnumerator StopFiringAudio()
    {
        yield return new WaitForSeconds(0.2f);
        gunAudio.Stop();
        isFiring = false;
    }
    void StartFiringAudio()
    {
        isFiring = true;
        gunAudio.Stop();
        gunAudio.PlayOneShot(gunSounds[0], 1f);
        gunAudio.PlayOneShot(gunSounds[1], 1f);
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

            if (weaponNum == 0)
            {
                newWeaponSpread = Random.Range(-spreadSingle, spreadSingle);
            }
            else if (weaponNum == 1)
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

            isoCamScript.camShake = camShakeMagnitude * camShakeMult;
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
