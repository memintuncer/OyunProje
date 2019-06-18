using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused;
    private GameObject PauseUI;
    public GameObject TimeController;
    bool isQuiting = false;
    private GameObject player;
    private GameObject playerCanvases;

    // Start is called before the first frame update
    void Start()
    {
        /*Screen.lockCursor = true;
        Cursor.visible = false;*/
        player = GameObject.FindGameObjectWithTag("Player");
        playerCanvases = player.transform.GetChild(1).gameObject;
        PauseUI = transform.GetChild(0).gameObject;
        GamePaused = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {


            if (GamePaused)
            {
                playerCanvases.SetActive(false);
                player.GetComponent<FirstPersonController>().enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Resume();
                
            }
            else
            {
                Pause();
            }
        }
        
    }





    public void Resume()
    {
        Debug.Log("call");
        //Screen.lockCursor = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GamePaused = false;

        
        

        PauseUI.SetActive(false);

        if (TimeController.gameObject.activeInHierarchy != true)
        {
           TimeController.SetActive(true);
        }

        playerCanvases.SetActive(true);
        player.GetComponent<FirstPersonController>().enabled = true;
        
        Time.timeScale = 1f;
        
        
    }

    

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (PauseUI.gameObject.activeInHierarchy != true)
        {
            PauseUI.SetActive(true);
        }
        //PauseUI.SetActive(true);
        //player.SetActive(false);
        if (TimeController.gameObject.activeInHierarchy != false)
        {
            TimeController.SetActive(false);
        }

        playerCanvases.SetActive(false);
        player.GetComponent<FirstPersonController>().enabled = false;
        
        Time.timeScale = 0f;
        GamePaused = true;
       
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void MainMenu()
    {
        //PauseUI.SetActive(false);
        //if (TimeController.gameObject.activeInHierarchy != false)
        //{
        //    TimeController.SetActive(false);
        //}

        Time.timeScale = 1f;
        StaticLevelInfo.NextSceneToLoad = 0;
        SceneManager.LoadScene(1);
    }
}
