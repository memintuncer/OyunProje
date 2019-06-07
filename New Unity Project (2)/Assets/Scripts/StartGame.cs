using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public GameObject girl;
    public GameObject Camera;
    private bool ispanel = false;
    public float timeleft;
    public GameObject TimeMan;
    Coroutine coroutine;
    void Start()
    {
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
            Camera.SetActive(false);
            player.SetActive(true);
            TimeMan.SetActive(true);
            }
        }


    }


    IEnumerator StarttheGame()
    {
        
        yield return new WaitForSeconds(timeleft);

        girl.SetActive(false);
        Camera.SetActive(false);
        player.SetActive(true);
        TimeMan.SetActive(true);




    }
}
