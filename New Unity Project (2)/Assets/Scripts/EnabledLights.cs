using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnabledLights : MonoBehaviour
{
    // Start is called before the first frame update
    private Light myLight;
    public GameObject console;


    void Start()
    {
        myLight = GetComponent<Light>();
        myLight.enabled = false;
    }


    void Update()
    {
        if (console.GetComponent<consoleScript>().Activate == true)
        {
            myLight.enabled =true;

        }
        if (console.GetComponent<consoleScript>().Activate != true)
        {
            myLight.enabled = false;

        }
    }
}
