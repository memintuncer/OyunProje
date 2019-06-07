using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;
    public GameObject PauseUI;
    public GameObject TimeController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused)
            {
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
        PauseUI.SetActive(false);
        if (TimeController.gameObject.activeInHierarchy != true)
        {
            TimeController.SetActive(true);
        }
        Time.timeScale = 1f;
        GamePaused = false;
        
    }

    public void Pause()
    {
        PauseUI.SetActive(true);
        if (TimeController.gameObject.activeInHierarchy != false)
        {
            TimeController.SetActive(false);
        }
        
        //Time.timeScale = 0f;
        GamePaused = true;
        //Cursor.visible = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
