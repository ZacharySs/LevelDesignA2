using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerCutsceneScript : MonoBehaviour
{
    IsometricCameraScript isoCamScript;

    public Text subtitleText;

    public string[] cutsceneSubtitles;
    int cutsceneNum = -1;

    void Start()
    {
        isoCamScript = Camera.main.GetComponent<IsometricCameraScript>();
        subtitleText.text = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (subtitleText)
        {
            if (other.tag == "Cutscene1Trigger" && cutsceneNum < 0)
            {
                isoCamScript.CutsceneFocus((GameObject.Find("WindowTeleportLocation").transform.position));
                cutsceneNum = 0;
                StartCoroutine(SubtitleCoroutine());
            }
            if (other.tag == "Cutscene2Trigger" && cutsceneNum < 1)
            {
                isoCamScript.CutsceneFocus((GameObject.FindGameObjectWithTag("Elevator").transform.position));
                cutsceneNum = 1;
                StartCoroutine(SubtitleCoroutine());
            }






        }
        else
        {
            Debug.LogError("MISSING SUBTITLE TEXT.");
        }
    }

    IEnumerator SubtitleCoroutine()
    {
        subtitleText.text = cutsceneSubtitles[cutsceneNum];

        yield return new WaitForSeconds(5f);

        subtitleText.text = null;
    }
}
