using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsFadeIn : MonoBehaviour
{
    [HideInInspector]
    public bool startFading;
    private Image reloadButton;
    private Text reloadText;
    private Image menuButton;
    private Text menuText;
    Color temp;

    // Start is called before the first frame update
    void Start()
    {
        GameObject reloadObject = transform.GetChild(0).gameObject;
        GameObject menuObject = transform.GetChild(1).gameObject;
        startFading = false;

        reloadButton = reloadObject.GetComponent<Image>();
        reloadText = reloadObject.transform.GetChild(0).gameObject.GetComponent<Text>();

        menuButton = menuObject.gameObject.GetComponent<Image>();
        menuText = menuObject.transform.GetChild(0).gameObject.GetComponent<Text>();

        

    }

    // Update is called once per frame
    void Update()
    {
        if (startFading)
        {
            StartCoroutine(Fading());
        }
    }

    IEnumerator Fading()
    {
        while (temp.a < 1)
        {
            temp = reloadButton.color;
            temp.a += Time.deltaTime / 2;
            reloadButton.color = temp;

            temp = reloadText.color;
            temp.a += Time.deltaTime / 2;
            reloadText.color = temp;

            temp = menuButton.color;
            temp.a += Time.deltaTime / 2;
            menuButton.color = temp;

            temp = menuText.color;
            temp.a += Time.deltaTime / 2;
            menuText.color = temp;

            yield return new WaitForEndOfFrame();
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void mainMenuButton()
    {
        StaticLevelInfo.NextSceneToLoad = 0;
        SceneManager.LoadScene(1);

    }

    public void tryAgainButton()
    {
        StaticLevelInfo.NextSceneToLoad = StaticLevelInfo.PreviousScene;
        SceneManager.LoadScene(1);
    }
}
