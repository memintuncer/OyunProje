using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManipulation : MonoBehaviour
{
    // Start is called before the first frame update
    public float slowdownFactor = 0.05f;
    public float mouseSlowdown = 0.5f;
    public float slowdownLength = 2f;

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("fixedDeltaTime");
        //Debug.Log( Time.fixedDeltaTime);
        //Debug.Log("timeScale");
        //Debug.Log(Time.timeScale);
        //Debug.Log("unscaledDeltaTime");
        //Debug.Log(Time.unscaledDeltaTime);

        if (Input.anyKey == false)
        {
            Time.timeScale = slowdownFactor;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;

        }
        /*
        else if (Input.GetAxis("Mouse X") !=0 || Input.GetAxis("Mouse Y") != 0 )
         {
             Time.timeScale = mouseSlowdown;
             Time.fixedDeltaTime = Time.timeScale * 0.02f;

         }*/
        else
        {
            Time.timeScale = 1.0f;

            Time.fixedDeltaTime = 0.02f;
        }

    }

   
}
