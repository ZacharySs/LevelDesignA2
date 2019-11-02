using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class PlayerCutsceneScript : MonoBehaviour
{
    IsometricCameraScript isoCamScript;

    AudioSource camAudio;

    public Text subtitleText;

    public AudioClip[] cutsceneAudioClips;

    public AudioClip[] detectionAudioClips;
    string[] detectionSubtitles = { "Hyde, I've got movement. You might've been detected.",
                                        "Keep the noise down, Hyde. I've got contacts moving to your location.",
                                        "What are you doing down, Hyde? I've got movement near your location." };

    public AudioClip[] missionStateAudioClips;

    int cutsceneNum = -1;

    void Start()
    {
        isoCamScript = Camera.main.GetComponent<IsometricCameraScript>();
        camAudio = Camera.main.GetComponent<AudioSource>();
        subtitleText.text = null;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (subtitleText)
        {
            if (other.GetComponent<CutsceneTriggerScript>())
            {
                int triggerCutsceneNum = other.GetComponent<CutsceneTriggerScript>().thisCutsceneNum;
                if (cutsceneNum < triggerCutsceneNum)
                {
                    cutsceneNum = triggerCutsceneNum;
                    StartCoroutine("SubtitleCoroutine" + triggerCutsceneNum.ToString());
                    //camAudio.PlayOneShot(cutsceneAudioClips[triggerCutsceneNum]);
                }
            }
        }
        else
        {
            Debug.LogError("MISSING SUBTITLE TEXT.");
        }
    }

    public void DetectionSubtitle()
    {

    }
    IEnumerator SubtitleCoroutineDetection()
    {
        int subtitleToPlay = Random.Range(0, 4);
        subtitleText.text = detectionSubtitles[subtitleToPlay];
        camAudio.PlayOneShot(detectionAudioClips[subtitleToPlay]);

        yield return new WaitForSeconds(4f);

        subtitleText.text = null;
    }

    public void LoadingNextLevelSubtitle()
    {
        StartCoroutine(SubtitleCoroutineWin());
    }
    IEnumerator SubtitleCoroutineWin()
    {
        subtitleText.text = "Good work, Hyde. Looks like no one heard a thing.";
        camAudio.PlayOneShot(missionStateAudioClips[0]);

        yield return new WaitForSeconds(3f);

        subtitleText.text = null;
    }

    public void RestartLevelSubtitle()
    {
        StartCoroutine(SubtitleCoroutineDeath());
    }
    IEnumerator SubtitleCoroutineDeath()
    {
        subtitleText.text = "Hyde? Hyde, are you there? God damn it!";
        camAudio.PlayOneShot(missionStateAudioClips[1]);

        yield return new WaitForSeconds(3f);

        subtitleText.text = null;
    }


    IEnumerator SubtitleCoroutine0()
    {
        subtitleText.text = "Hyde, this is Alex. You copy?";

        yield return new WaitForSeconds(2f);

        subtitleText.text = "I know this one's personal for you, but try to stay professional.";

        yield return new WaitForSeconds(4f);

        subtitleText.text = "Conserve your ammunition, place your shots, and stay undetected.";

        yield return new WaitForSeconds(4f);

        subtitleText.text = "Do that, and you'll get through this just fine.";

        yield return new WaitForSeconds(3f);

        subtitleText.text = null;
    }
    IEnumerator SubtitleCoroutine1()
    {
        subtitleText.text = "Hyde, it looks like they've got the front door locked down. You'll have to find another way inside.";
        isoCamScript.CutsceneFocus(GameObject.Find("WindowInteract").transform.position);

        yield return new WaitForSeconds(5f);

        subtitleText.text = null;
    }
    IEnumerator SubtitleCoroutine2()
    {
        subtitleText.text = "Good, you're in. The target's going to be on one of the lower levels, so you'll need to find a keycard for the elevator.";
        isoCamScript.CutsceneFocus(GameObject.FindGameObjectWithTag("KeycardPickup").transform.position);

        yield return new WaitForSeconds(6f);

        subtitleText.text = null;
    }
    IEnumerator SubtitleCoroutine3()
    {
        subtitleText.text = "Ok, you found the keycard. Now get over to the elevator.";
        isoCamScript.CutsceneFocus(GameObject.FindGameObjectWithTag("Elevator").transform.position);

        yield return new WaitForSeconds(3.5f);

        subtitleText.text = null;
    }
    IEnumerator SubtitleCoroutine4()
    {
        subtitleText.text = "Ok... seems like you'll need to go down another level. There's a keycard on this level, but it's behind a locked door.";

        yield return new WaitForSeconds(6f);

        subtitleText.text = "You'll need to find a way around.";

        yield return new WaitForSeconds(2f);

        subtitleText.text = null;
    }
    IEnumerator SubtitleCoroutine5()
    {
        subtitleText.text = "Hmm... Three big, locked doors? Well, you know the drill, Hyde.";

        yield return new WaitForSeconds(5f);

        subtitleText.text = "Three doors, three keycards. They've gotta be around here somewhere.";

        yield return new WaitForSeconds(3f);

        subtitleText.text = null;
    }
    IEnumerator SubtitleCoroutine6()
    {
        subtitleText.text = "This is it... Take 'em out, Hyde.";

        yield return new WaitForSeconds(3f);

        subtitleText.text = null;
    }

}
