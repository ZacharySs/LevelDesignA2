using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && Input.GetKey(KeyCode.LeftShift))
        {
            SceneManager.LoadScene("Level1");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && Input.GetKey(KeyCode.LeftShift))
        {
            SceneManager.LoadScene("Level2");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && Input.GetKey(KeyCode.LeftShift))
        {
            SceneManager.LoadScene("Level3");
        }

    }
}
