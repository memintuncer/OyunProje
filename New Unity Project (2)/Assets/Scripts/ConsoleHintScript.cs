using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleHintScript : MonoBehaviour
{
    public GameObject openPanel = null;
    public Camera hintCamera;
    private GameObject player;
    private Camera mainCamera;
    private bool hintStarted =false;
    public Light spot0;
    public Light spot1;
    public Light spot2;
    public Light spot3;
    public Light spot4;
    public Light spot5;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = Camera.main;
        mainCamera.enabled = true;
        hintCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (openPanel.active)
        {
            if (Input.GetKey(KeyCode.F))
            {
                openPanel.SetActive(false);
                if (!hintStarted)
                {
                    //Enable the second Camera
                    hintCamera.enabled = true;
                    //The Main first Camera is disabled
                    mainCamera.enabled = false;
                    player.SetActive(false);
                    Debug.Log("Hint started");
                    StartCoroutine("ShowHint");

                }
               
                   

            }
        }
    }

    IEnumerator ShowHint()
    {
        yield return new WaitForSeconds(0.05f); 
        spot4.enabled = true;
        yield return new WaitForSeconds(0.05f);
        spot4.enabled = false;
        spot2.enabled = true;
        yield return new WaitForSeconds(0.05f);
        spot2.enabled = false;
        spot5.enabled = true;
        yield return new WaitForSeconds(0.05f);
        spot5.enabled = false;
        spot0.enabled = true;
        yield return new WaitForSeconds(0.05f);
        spot0.enabled = false;
        spot1.enabled = true;
        yield return new WaitForSeconds(0.05f);
        spot1.enabled = false;
        spot3.enabled = true;
        yield return new WaitForSeconds(0.05f);
        spot3.enabled = false;

        //hintStarted = true;

        //Disable the second camera
        hintCamera.enabled = false;
        //Enable the Main Camera
        mainCamera.enabled = true;
        player.SetActive(true);
        Debug.Log("Hint stopped.");




    }

    private int isTouched = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (isTouched==0)
            {
                isTouched = 1;
                openPanel.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isTouched = 0;
            openPanel.SetActive(false);
        }
    }

    private bool IsOpenPanelActive
    {
        get
        {
            return openPanel.activeInHierarchy;
        }
    }
}
