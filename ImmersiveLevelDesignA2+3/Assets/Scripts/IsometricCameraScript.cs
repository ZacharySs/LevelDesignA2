using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class IsometricCameraScript : MonoBehaviour
{
    public GameObject player;

    public float height;
    public float zDisp;
    [HideInInspector]
    public float camShake; // Camera shake magnitude is controlled by PlayerWeaponScript

    public float cameraSpeed = 1.0f;
    private Vector3 newCamPos;
    public bool inCutscene;
    static int cutsceneNum;

    // Use this for initialization
    void Start()
    {
        transform.position = new Vector3(transform.position.x, height, transform.position.z - zDisp);

        if (SceneManager.GetActiveScene().name == "Level1")
        {
            cutsceneNum = 1;
        }
    }

    public void CutsceneFocus(Vector3 focusPos)
    {
        StartCoroutine(CutsceneCoroutine(focusPos));
    }
    IEnumerator CutsceneCoroutine(Vector3 focusPos)
    {
        inCutscene = true;
        newCamPos = focusPos;
        yield return new WaitForSeconds(5f);
        inCutscene = false;
    }

    // Update is called once per frame
    void Update()
    {

        //If Player Alive...
        if (player)
        {
            CameraMovement();
        }
    }


    //Camera Pans (Lerps) towards position above player avatar
    void CameraMovement()
    {
        if (!inCutscene)
        {
            newCamPos = player.transform.position;

            newCamPos.y = player.transform.position.y + height;
            newCamPos.z = player.transform.position.z - zDisp + Random.Range(-camShake, camShake);
            newCamPos.x = player.transform.position.x - zDisp + Random.Range(-camShake, camShake);
        }

        transform.position = Vector3.Lerp(transform.position, newCamPos, cameraSpeed * Time.deltaTime);

        camShake = 0f;
    }
}
