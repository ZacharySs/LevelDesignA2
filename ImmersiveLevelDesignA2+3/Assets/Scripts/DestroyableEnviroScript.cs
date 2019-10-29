using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DestroyableEnviroScript : MonoBehaviour
{
    GameManagerScript gameManagerScript;

    public float health = 10.0f;
    public bool isHardWall = true;

    bool isDamaged = false;
    Vector3 initialPos;
    float damageShakeMagnitude = 0.2f;

    // If this script is attached to a Hallway Door
    GameObject sparksEffect;
    HallwayDoorScript hallwayDoorScript;
    SingleDoorScript singleDoorScript;
    GameObject lockLight;

    Color baseEmissiveColor;

    void Start()
    {
        initialPos = transform.position;

        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        sparksEffect = gameManagerScript.lockLightSparksEffect;

        if (GetComponentInParent<HallwayDoorScript>())
            hallwayDoorScript = GetComponentInParent<HallwayDoorScript>();
        else if (GetComponentInParent<SingleDoorScript>())
            singleDoorScript = GetComponentInParent<SingleDoorScript>();

        if (GetComponentInChildren<LockLightScript>())
        {
            lockLight = GetComponentInChildren<LockLightScript>().gameObject;
        }

        StartCoroutine(SetColorsCoroutine());
    }



    IEnumerator SetColorsCoroutine()
    {
        if (!isHardWall)
        {
            if (SceneManager.GetActiveScene().name == "Level2")
                baseEmissiveColor = gameManagerScript.L2_softWallEmissionColor;
            else if (SceneManager.GetActiveScene().name == "Level3")
                baseEmissiveColor = gameManagerScript.L3_softWallEmissionColor;

            StartCoroutine(UpdateStrobingColor());
        }
        else
        {
            if (SceneManager.GetActiveScene().name == "Level2")
                baseEmissiveColor = gameManagerScript.L2_hardWallEmissionColor;
            else if (SceneManager.GetActiveScene().name == "Level3")
                baseEmissiveColor = gameManagerScript.L3_hardWallEmissionColor;


            Renderer[] renderers = GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                if (!renderer.gameObject.GetComponent<LockLightScript>()) // Only set child renderers if they are not LockLights. 
                {
                    Material[] sharedMaterials = renderer.sharedMaterials;
                    for (int i = 0; i < sharedMaterials.Length; i++)
                    {
                        if (sharedMaterials[i].IsKeywordEnabled("_EMISSION"))
                        {
                            if (SceneManager.GetActiveScene().name == "Level2")
                            {
                                renderer.materials[i].SetColor("_EmissionColor", baseEmissiveColor);
                            }
                            else if (SceneManager.GetActiveScene().name == "Level3")
                            {
                                renderer.materials[i].SetColor("_EmissionColor", baseEmissiveColor);
                            }
                        }
                    }
                }
                else // Makes sure the LockLight retains the emissive color it starts with, which prevents red LockLights from becoming something else.
                {
                    Material[] sharedMaterials = renderer.sharedMaterials;
                    for (int i = 0; i < sharedMaterials.Length; i++)
                    {
                        if (sharedMaterials[i].IsKeywordEnabled("_EMISSION"))
                        {
                            baseEmissiveColor = renderer.materials[i].GetColor("_EmissionColor");
                            renderer.materials[i].SetColor("_EmissionColor", baseEmissiveColor);
                        }
                    }
                }
            }


            if (!GetComponentInChildren<LockLightScript>())
            {
                if (GetComponentInChildren<Light>())
                {
                    Light[] wallLights = GetComponentsInChildren<Light>();
                    for (int i = 0; i < wallLights.Length; i++)
                    {
                        if (SceneManager.GetActiveScene().name == "Level2")
                            wallLights[i].color = gameManagerScript.L2_hardWallLightColor;
                        else if (SceneManager.GetActiveScene().name == "Level3")
                            wallLights[i].color = gameManagerScript.L3_hardWallLightColor;
                    }
                }
            }
        }

        yield return null;

    }

    IEnumerator UpdateStrobingColor()
    {
        while (true)
        {
            Renderer[] renderers = GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                Material[] sharedMaterials = renderer.sharedMaterials;
                for (int i = 0; i < sharedMaterials.Length; i++)
                {
                    if (renderer.gameObject.GetComponent<LockLightScript>())
                    {
                        if (sharedMaterials[i].IsKeywordEnabled("_EMISSION"))
                        {
                            LockLightScript lockLightScript = GetComponentInChildren<LockLightScript>();
                            if (lockLightScript.isLocked)
                            {
                                renderer.materials[i].SetColor("_EmissionColor", lockLightScript.lockLightMaterial[0].GetColor("_EmissionColor") * gameManagerScript.strobeColorIntensity);
                            }
                            else
                            {
                                renderer.materials[i].SetColor("_EmissionColor", lockLightScript.lockLightMaterial[1].GetColor("_EmissionColor") * gameManagerScript.strobeColorIntensity);
                            }
                        }
                    }
                    else
                    {
                        if (sharedMaterials[i].IsKeywordEnabled("_EMISSION"))
                        {
                            renderer.materials[i].SetColor("_EmissionColor", baseEmissiveColor * gameManagerScript.strobeColorIntensity);
                        }
                    }
                }

                Light[] wallLights = GetComponentsInChildren<Light>();
                for (int i = 0; i < wallLights.Length; i++)
                {
                    Color newLightColor = wallLights[i].color;

                    newLightColor.a = gameManagerScript.strobeColorIntensity;

                    wallLights[i].color = newLightColor;
                }

                yield return new WaitForSeconds(0.025f);
            }
        }
    }

    IEnumerator DamageCoroutine()
    {
        transform.position = new Vector3(transform.position.x + Random.Range(-1f, 1f) * damageShakeMagnitude,
                         transform.position.y + Random.Range(-1f, 1f) * damageShakeMagnitude,
                         transform.position.z + Random.Range(-1f, 1f) * damageShakeMagnitude);
        yield return new WaitForSeconds(0.0333f);
        transform.position = initialPos;
    }

    public void takeDamage(float thisDamage)
    {
        if (!isHardWall)
        {
            health -= thisDamage;

            if (!isDamaged)
            {
                if (hallwayDoorScript)
                {

                    hallwayDoorScript.StopDoorAnim();

                    if (lockLight)
                    {
                        Vector3 lockLightEuler = lockLight.transform.rotation.eulerAngles;

                        Instantiate(sparksEffect, lockLight.transform.position - (lockLight.transform.up * 0.1f), Quaternion.Euler(lockLightEuler.x, lockLightEuler.y - 90, lockLightEuler.z), lockLight.transform);

                        Instantiate(sparksEffect, lockLight.transform.position + (lockLight.transform.up * 0.6f), Quaternion.Euler(lockLightEuler.x, lockLightEuler.y + 90, lockLightEuler.z), lockLight.transform);
                        Debug.Log("Sparks Instantiated.");
                    }
                }
                else if (singleDoorScript)
                {
                    singleDoorScript.StopDoorAnim();

                    if (lockLight)
                    {
                        Vector3 lockLightEuler = lockLight.transform.rotation.eulerAngles;

                        Instantiate(sparksEffect, lockLight.transform.position - (lockLight.transform.up * 0.0f), Quaternion.Euler(lockLightEuler.x - 90, lockLightEuler.y, lockLightEuler.z), lockLight.transform);

                        Instantiate(sparksEffect, lockLight.transform.position + (lockLight.transform.up * 0.1f), Quaternion.Euler(lockLightEuler.x + 90, lockLightEuler.y, lockLightEuler.z), lockLight.transform);
                        Debug.Log("Sparks Instantiated.");
                    }
                }

                isDamaged = true;
            }

            StartCoroutine(DamageCoroutine());
        }

        /*
        transform.position = new Vector3(transform.position.x + Mathf.Sign(Random.Range(-1, 1)) * damageShakeMagnitude,
                                 transform.position.y + Mathf.Sign(Random.Range(-1, 1)) * damageShakeMagnitude,
                                 transform.position.z + Mathf.Sign(Random.Range(-1, 1)) * damageShakeMagnitude);
        */
    }

    private void OnDrawGizmos()
    {
        if (!isHardWall)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.up * 5);
        }
    }
}
