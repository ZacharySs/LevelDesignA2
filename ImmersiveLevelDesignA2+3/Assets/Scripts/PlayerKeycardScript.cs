using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerKeycardScript : MonoBehaviour
{
    [HideInInspector]
    public int totalKeycards = 0;
    [HideInInspector]
    public bool isLoadingLevel = false;

    public Animator loadingTextAnimator;

    PlayerCutsceneScript playerCutsceneScript;

    AsyncOperation asyncLoad;

    private void Start()
    {
        loadingTextAnimator.SetBool("LoadingTextVisible", false);
        playerCutsceneScript = GetComponent<PlayerCutsceneScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "KeycardPickup")
        {
            totalKeycards++;
            Destroy(other.gameObject);
        }

        if (other.tag == "LevelTransfer" && totalKeycards > 0 && SceneManager.GetActiveScene().name != "Level3")
        {
            if (asyncLoad != null)
            {
                StartCoroutine(LoadLevelCoroutine());
            }
        }
    }


    public void RestartLevel()
    {
        StartCoroutine(RestartLevelCoroutine());
    }

    IEnumerator RestartLevelCoroutine()
    {
        yield return new WaitForSeconds(4f);

        if (SceneManager.GetActiveScene().name == "Level1")
        {
            asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
            playerCutsceneScript.RestartLevelSubtitle();
            isLoadingLevel = true;
        }

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    IEnumerator LoadLevelCoroutine()
    {
        yield return new WaitForSeconds(4f);

        if (SceneManager.GetActiveScene().name == "Level1")
        {
            asyncLoad = SceneManager.LoadSceneAsync("Level2");
            loadingTextAnimator.SetBool("LoadingTextVisible", true);
            playerCutsceneScript.LoadingNextLevelSubtitle();
            isLoadingLevel = true;
        }
        else if (SceneManager.GetActiveScene().name == "Level2")
        {
            asyncLoad = SceneManager.LoadSceneAsync("Level3");
            loadingTextAnimator.SetBool("LoadingTextVisible", true);
            playerCutsceneScript.LoadingNextLevelSubtitle();
            isLoadingLevel = true;
        }

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
