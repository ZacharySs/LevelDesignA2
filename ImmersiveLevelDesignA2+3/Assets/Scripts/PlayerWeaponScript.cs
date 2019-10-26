using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerWeaponScript : MonoBehaviour
{
    private bool canFire = true;
    private float fireTime = 0.05f;

    GameObject[] fireLocations;
    GameObject[] muzzleFlashEffects;
    public GameObject projectile;

    int weaponNum = 0;
    static int ammo = 500;
    static int weapons = 0;
    float spreadSingle = 5f;
    float spreadDouble = 12f;
    float spreadTriple = 25f;
    float newWeaponSpread;

    AudioSource gunAudio;
    public AudioClip[] gunSounds;
    bool isFiring = false;

    IsometricCameraScript isoCamScript;
    Animator isoCamAnimator;
    public float camShakeMagnitude = 0.5f;
    float camShakeMult = 1f;

    GameObject hudCanvas;
    Text ammoText;

    Text weaponText;
    Vector2 weaponTextPos;
    Vector3 weaponTextRot;
    int weaponTextFontSize;

    GameObject redoutImage;
    Animator redoutAnimator;

    void Start()
    {
        InitialiseVars();
    }

    void InitialiseVars()
    {
        gunAudio = GetComponent<AudioSource>();

        isoCamScript = Camera.main.GetComponent<IsometricCameraScript>();
        isoCamAnimator = Camera.main.GetComponent<Animator>();

        hudCanvas = GameObject.Find("HUDCanvas");

        ammoText = GameObject.Find("AmmoText").GetComponent<Text>();

        weaponText = GameObject.Find("WeaponText").GetComponent<Text>();
        weaponTextPos = weaponText.rectTransform.anchoredPosition;
        weaponTextRot = weaponText.rectTransform.rotation.eulerAngles;
        weaponTextFontSize = weaponText.fontSize;

        redoutImage = GameObject.Find("RedoutImage");
        redoutAnimator = redoutImage.GetComponent<Animator>();

        fireLocations = new GameObject[] { transform.Find("FireLocation1").gameObject,
                                           transform.Find("FireLocation2").gameObject,
                                           transform.Find("FireLocation3").gameObject };

        muzzleFlashEffects = new GameObject[] { fireLocations[0].GetComponentInChildren<ParticleSystem>().gameObject, 
                                                fireLocations[1].GetComponentInChildren<ParticleSystem>().gameObject,
                                                fireLocations[2].GetComponentInChildren<ParticleSystem>().gameObject };

        for (int i = 0; i < muzzleFlashEffects.Length; i++)
        {
            muzzleFlashEffects[i].SetActive(false);
        }

        SelectWeapon(1);
    }

    void Update()
    {
        if (Time.timeScale > 0)
        {
            if (Input.GetMouseButton(0))
            {
                if (canFire && ammo > 0)
                {
                    FireProjectile();
                    canFire = false;

                    if (!isFiring)
                    {
                        redoutAnimator.SetBool("BoolFiring", true);
                        isoCamAnimator.SetBool("BoolFiring", true);
                        StartFiring();
                    }
                }
                else if (isFiring && ammo < 1)
                {
                    gunAudio.PlayOneShot(gunSounds[2], 0.8f);
                    redoutAnimator.SetBool("BoolFiring", false);
                    isoCamAnimator.SetBool("BoolFiring", false);
                    isFiring = false;

                    StartCoroutine(StopFiringCoroutine());
                }
            }
            if (Input.GetMouseButtonUp(0) && isFiring)
            {
                gunAudio.PlayOneShot(gunSounds[2], 0.8f);
                redoutAnimator.SetBool("BoolFiring", false);
                isoCamAnimator.SetBool("BoolFiring", false);

                StartCoroutine(StopFiringCoroutine());
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SelectWeapon(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && weapons > 0)
            {
                SelectWeapon(2);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && weapons > 1)
            {
                SelectWeapon(3);
            }
        }
    }

    IEnumerator StopFiringCoroutine()
    {
        for (int i = 0; i <= weaponNum; i++)
        {
            muzzleFlashEffects[i].SetActive(false);
        }

        yield return new WaitForSeconds(0.2f);
        gunAudio.Stop();
        isFiring = false;
    }
    void StartFiring()
    {
        isFiring = true;
        gunAudio.Stop();
        gunAudio.PlayOneShot(gunSounds[0], 1f);
        gunAudio.PlayOneShot(gunSounds[1], 1f);

        for (int i = 0; i <= weaponNum; i++)
        {
            muzzleFlashEffects[i].SetActive(true);
        }
    }

    void SelectWeapon(int selectedWeapon)
    {
        if (selectedWeapon == 1)
        {
            fireLocations[1].SetActive(false);
            fireLocations[2].SetActive(false);

            weaponNum = 0;
            gunAudio.volume = 0.25f;
            camShakeMult = 1f;

            weaponText.rectTransform.anchoredPosition = weaponTextPos;
            weaponText.rectTransform.rotation = Quaternion.Euler(weaponTextRot);
            weaponText.fontSize = weaponTextFontSize;
            weaponText.text = "AK-74";
        }
        else if (selectedWeapon == 2)
        {
            fireLocations[1].SetActive(true);
            fireLocations[2].SetActive(false);

            weaponNum = 1;
            gunAudio.volume = 0.4f;
            camShakeMult = 2f;

            weaponText.rectTransform.anchoredPosition = new Vector2(weaponTextPos.x - 10, weaponTextPos.y + 24);
            weaponText.rectTransform.rotation = Quaternion.Euler(new Vector3(weaponTextRot.x, weaponTextRot.y, weaponTextRot.z - 5));
            weaponText.fontSize = weaponTextFontSize + 1;
            weaponText.text = "Dual-wield AK-74";
        }
        else if (selectedWeapon == 3)
        {
            fireLocations[1].SetActive(true);
            fireLocations[2].SetActive(true);

            weaponNum = 2;
            gunAudio.volume = 0.65f;
            camShakeMult = 3f;

            weaponText.rectTransform.anchoredPosition = new Vector2(weaponTextPos.x - 4, weaponTextPos.y + 20);
            weaponText.rectTransform.rotation = Quaternion.Euler(new Vector3(weaponTextRot.x, weaponTextRot.y, weaponTextRot.z + 10));
            weaponText.fontSize = weaponTextFontSize + 2;
            weaponText.text = "Triple-wield AK-74!!!";
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

            // Determine bullet spread based on weapon selection
            if (weaponNum == 0)
            {
                for (int i = 0; i <= weaponNum; i++)
                {
                    newWeaponSpread = Random.Range(-spreadSingle, spreadSingle);

                    Vector3 fireDirection = new Vector3 (fireLocations[i].transform.eulerAngles.x,
                                                         fireLocations[i].transform.eulerAngles.y + newWeaponSpread,
                                                         fireLocations[i].transform.eulerAngles.z);

                    Instantiate(projectile, 
                                fireLocations[i].transform.position + (fireLocations[i].transform.forward * 0.3f), 
                                Quaternion.Euler(fireDirection));

                    ammo--;
                }
            }
            else if (weaponNum == 1)
            {
                for (int i = 0; i <= weaponNum; i++)
                {
                    newWeaponSpread = Random.Range(-spreadDouble, spreadDouble);

                    Vector3 fireDirection = new Vector3(fireLocations[i].transform.eulerAngles.x,
                                                         fireLocations[i].transform.eulerAngles.y + newWeaponSpread,
                                                         fireLocations[i].transform.eulerAngles.z);

                    Instantiate(projectile,
                                fireLocations[i].transform.position + (fireLocations[i].transform.forward * 0.3f),
                                Quaternion.Euler(fireDirection));

                    ammo--;
                }
            }
            else if (weaponNum == 2)
            {
                for (int i = 0; i <= weaponNum; i++)
                {
                    newWeaponSpread = Random.Range(-spreadTriple, spreadTriple);

                    Vector3 fireDirection = new Vector3(fireLocations[i].transform.eulerAngles.x,
                                                         fireLocations[i].transform.eulerAngles.y + newWeaponSpread,
                                                         fireLocations[i].transform.eulerAngles.z);

                    Instantiate(projectile,
                                fireLocations[i].transform.position + (fireLocations[i].transform.forward * 0.3f),
                                Quaternion.Euler(fireDirection));

                    ammo--;
                }
            }
            StartCoroutine(FirerateCoroutine());

            // Update ammo
            UpdateAmmoCounter();

            // Create camera shake
            isoCamScript.camShake = camShakeMagnitude * camShakeMult;
        }
    }

    IEnumerator FirerateCoroutine()
    {
        yield return new WaitForSeconds(fireTime);

        canFire = true;
    }

    void UpdateAmmoCounter()
    {
        ammo = Mathf.Clamp(ammo, 0, 9999);

        char[] ammoChars = ammo.ToString().ToCharArray();

        // All of this is specialised for the Royal Fighter font, which convers '0' into a lowercase 'o', since a '0' isn't included in the font pack
        string ammoString = "";
        for (int i = 0; i < ammoChars.Length; i++)
        {
            char ammoChar = ammoChars[i];
            if (ammoChar == '0')
            {
                ammoChar = 'o';
                ammoChars[i] = ammoChar;
            }

            ammoString += ammoChars[i];
        }

        ammoText.text = ammoString + " / A LOT";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "AKPickup")
        {
            weapons++;
            Destroy(other.gameObject);
        }

        if (other.tag == "AmmoPickup")
        {
            ammo += 100;
            Destroy(other.gameObject);
        }
    }
}
