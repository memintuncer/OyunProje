using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;
    public GameObject PauseUI;
    public GameObject TimeController;
    bool isQuiting = false;
    public GameObject player;
    public GameObject crosshair;
    // Start is called before the first frame update
    void Start()
    {
        /*Screen.lockCursor = true;
        Cursor.visible = false;*/
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

           
            if (GamePaused)
            {
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
        //Screen.lockCursor = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GamePaused = false;
       
        PauseUI.SetActive(false);

        if (TimeController.gameObject.activeInHierarchy != true)
        {
           TimeController.SetActive(true);
        }
        crosshair.SetActive(true);
        player.GetComponent<FirstPersonController>().enabled = true;
        
        Time.timeScale = 1f;
        
        
    }

    

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        crosshair.SetActive(false);
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
        PauseUI.SetActive(false);
        if (TimeController.gameObject.activeInHierarchy != false)
        {
            TimeController.SetActive(false);
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex- 1);
    }
}
