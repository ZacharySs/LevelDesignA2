using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GameObject tileDestructionEffect;

    public Color L2_hardWallEmissionColor;
    public Color L2_softWallEmissionColor;
    public Color L3_hardWallEmissionColor;
    public Color L3_softWallEmissionColor;

    public Color L2_hardWallLightColor;
    public Color L2_softWallLightColor;
    public Color L3_hardWallLightColor;
    public Color L3_softWallLightColor;

    public float strobeColorIntensity = 1;

    public GameObject lockLightSparksEffect;

    GameObject hudCanvas;
    GameObject pauseCanvas;

    public Texture2D mouseTexture;


    bool paused = true;

    private void Start()
    {
        InitialiseVariables();

        CyclePauseState();
    }
    void InitialiseVariables()
    {
        hudCanvas = GameObject.Find("HUDCanvas");
        pauseCanvas = GameObject.Find("PauseCanvas");

        Cursor.SetCursor(mouseTexture, new Vector2(mouseTexture.width / 2, mouseTexture.height / 2), CursorMode.Auto);
    }

    public void CyclePauseState()
    {
        paused = !paused;

        if (paused)
        {
            //Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
            pauseCanvas.SetActive(true);
            hudCanvas.SetActive(false);
        }
        else
        {
            //Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
            Time.timeScale = 1f;
            pauseCanvas.SetActive(false);
            hudCanvas.SetActive(true);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            CyclePauseState();
        }

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
