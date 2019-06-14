﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPadScript : MonoBehaviour
{
    public GameObject KeyPadGUI;
    public GameObject PressGUI;
    public GameObject player;
    public GameObject TimeCont;
    
    public bool puzzle = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (puzzle)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                
                player.SetActive(false);
                TimeCont.SetActive(false);
                KeyPadGUI.SetActive(true);
                Cursor.visible = true;
                Screen.lockCursor = false;
            }
            
        }
        else
        {
           
            Cursor.visible = false;
            Screen.lockCursor = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player"&& KeyPadGUI.activeInHierarchy != true) { 
}
        {
            PressGUI.SetActive(true);
            puzzle = true;
            //Cursor.visible = true;


        }
    }
    private void OnTriggerExit(Collider other)
    {
        puzzle = false;
        PressGUI.SetActive(false);
    }




}
