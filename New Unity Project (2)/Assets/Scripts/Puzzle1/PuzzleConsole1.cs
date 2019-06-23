using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleConsole1 : MonoBehaviour
{

    public GameObject openPanel = null;
    public GameObject Reqconsole0;
    public GameObject console4;
    public GameObject console2;
    public GameObject console5;
    // Start is called before the first frame update
    void Update()
    {
        if (IsOpenPanelActive)
        {
            if (Input.GetKey(KeyCode.F))
            {
                if (Reqconsole0.GetComponent<consoleScript>().Activate == true)
                {
                    gameObject.GetComponent<consoleScript>().Activate = true;

                }
                else if (Reqconsole0.GetComponent<consoleScript>().Activate == false)
                {
                    Reqconsole0.GetComponent<consoleScript>().Activate = false;
                    console4.GetComponent<consoleScript>().Activate = false;
                    console2.GetComponent<consoleScript>().Activate = false;
                    console5.GetComponent<consoleScript>().Activate = false;
                }

            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && gameObject.GetComponent<consoleScript>().Activate == false)
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
