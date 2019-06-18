using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StaticLevelInfo.NextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(1);
        }
    }
}
