using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleConsole : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject openPanel = null;
    public GameObject Reqconsole;
   


    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if (IsOpenPanelActive)
        {
            if (Input.GetKey(KeyCode.F))
            {
                if(Reqconsole.GetComponent<consoleScript>().Activate == true)
                {
                    gameObject.GetComponent<consoleScript>().Activate = true;
                   
                }
                
                //platform1.transform.Translate(-8, 0, 0);

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
