using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMusic : MonoBehaviour
{
    public GameObject player;
    AudioSource Music;
    bool playAudio = false;
    // Start is called before the first frame update
    void Start()
    {
        Music= GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
        /*if (player.GetComponent<PlayerMovementScript>().isSpotted == 1)
        {
            
            playAudio = true;
               
        }
        else
        {
            Music.Stop();
        }
        if (playAudio)
        {
            Music.Play();
            playAudio = false;
        }*/

    }
}
