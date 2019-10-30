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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "KeycardPickup")
        {
            totalKeycards++;
            Destroy(other.gameObject);
        }

        if (other.tag == "LevelTransfer" && totalKeycards > 0)
        {
            if (SceneManager.GetActiveScene().name == "Level1")
            {
                SceneManager.LoadSceneAsync("Level2");
                loadingTextAnimator.SetBool("LoadingTextVisible", true);
                isLoadingLevel = true;
            }
            else if (SceneManager.GetActiveScene().name == "Level2")
            {
                SceneManager.LoadSceneAsync("Level3");
                loadingTextAnimator.SetBool("LoadingTextVisible", true);
                isLoadingLevel = true;
            }
        }
    }
}
