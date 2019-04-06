using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class time : MonoBehaviour
{
    // Start is called before the first frame update
    public float FrozenTime;
    public float TimeUnFrozen;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey == false)
        {
            Time.timeScale = FrozenTime;
        }

        if (Input.anyKey == true)
        {
            Time.timeScale = TimeUnFrozen;
        }
    }
}
