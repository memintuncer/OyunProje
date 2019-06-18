using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SpawnMouseOnSceneLoad : MonoBehaviour
{
    void OnEnable()
    {

        Debug.Log("OnEnable");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    

    // called when the game is terminated
    void OnDisable()
    {
        Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
