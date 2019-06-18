using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadNextSceneAsync());
    }

    IEnumerator LoadNextSceneAsync()
    {

        //Create async operation
        //That operation will load the next scene in the background
        AsyncOperation nextScene = SceneManager.LoadSceneAsync(StaticLevelInfo.NextSceneToLoad);
        Debug.Log(StaticLevelInfo.NextSceneToLoad);


        while (!nextScene.isDone)
        {
            //If we had a loading bar showing the percentage
            //We would want to have it here
            //Like follows

            //progressBar.fillAmount = nextScene.progress

            //Stop a bit
            yield return new WaitForEndOfFrame();

            //But since our loading bar is constantly rolling its animation
            //This piece of code is unnecessary
        }
        
    }
}
