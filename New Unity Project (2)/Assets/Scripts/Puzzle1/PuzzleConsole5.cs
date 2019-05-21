using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleConsole5 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject openPanel = null;
    public GameObject Reqconsole2;
    public GameObject console4;
    // Start is called before the first frame update
    void Update()
    {
        if (IsOpenPanelActive)
        {
            if (Input.GetKey(KeyCode.F))
            {
                if (Reqconsole2.GetComponent<consoleScript>().Activate == true)
                {
                    gameObject.GetComponent<consoleScript>().Activate = true;

                }
                else if(Reqconsole2.GetComponent<consoleScript>().Activate == false)
                {
                    Reqconsole2.GetComponent<consoleScript>().Activate = false;
                    console4.GetComponent<consoleScript>().Activate = false;
                }

            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            openPanel.SetActive(true);



        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
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
