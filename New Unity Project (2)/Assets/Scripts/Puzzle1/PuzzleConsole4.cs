using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleConsole4 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject openPanel = null;
    // Start is called before the first frame update
    void Update()
    {
        if (IsOpenPanelActive)
        {
            if (Input.GetKey(KeyCode.F))
            {
                gameObject.GetComponent<consoleScript>().Activate = true;


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
