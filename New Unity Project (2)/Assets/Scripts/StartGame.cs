using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public GameObject girl;
    public GameObject Camera;
    public GameObject StartPanel = null;
    private bool ispanel = false;
    public float timeleft;
    void Start()
    {
        StartCoroutine("StarttheGame");
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (ispanel == true)
        {
           

                girl.SetActive(false);
                Camera.SetActive(false);
                //StartPanel.SetActive(false);
                player.SetActive(true);
            
        }



    }


    IEnumerator StarttheGame()
    {
        
        yield return new WaitForSeconds(timeleft);
        //StartPanel.SetActive(true);
        ispanel = true;

        


    }
}
