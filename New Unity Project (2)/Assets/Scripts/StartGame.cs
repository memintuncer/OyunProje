using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;


public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject girl;
    private GameObject player;
    public GameObject playerCam;
    public GameObject cinematicCam;
    private bool ispanel = false;
    public float timeleft;
    public GameObject TimeMan;
    Coroutine coroutine;
    //public GameObject crosshair;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        girl.SetActive(true);

        player.GetComponent<FirstPersonController>().enabled = false;

        playerCam.GetComponent<Camera>().enabled = false;
        cinematicCam.SetActive(true);

        TimeMan.SetActive(false);
        coroutine = StartCoroutine("StarttheGame");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            if (coroutine != null)
            { 
            StopCoroutine(coroutine);
            girl.SetActive(false);

            player.GetComponent<FirstPersonController>().enabled = true;

                cinematicCam.SetActive(false);
                playerCam.GetComponent<Camera>().enabled = true;

            //player.SetActive(true);
            TimeMan.SetActive(true);
            //crosshair.SetActive(true);
            }
        }


    }


    IEnumerator StarttheGame()
    {
        
        yield return new WaitForSeconds(timeleft);

        girl.SetActive(false);

        player.GetComponent<FirstPersonController>().enabled = true;

        playerCam.GetComponent<Camera>().enabled = true;
        cinematicCam.SetActive(false);


        //player.SetActive(true);
        TimeMan.SetActive(true);
        //crosshair.SetActive(true);




    }
}
