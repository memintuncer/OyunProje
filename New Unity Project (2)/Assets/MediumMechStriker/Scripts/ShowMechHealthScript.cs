using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMechHealthScript : MonoBehaviour
{
    GameObject hs;
    Slider fill;
    // Start is called before the first frame update
    void Start()
    {
       
        fill = GetComponent<Slider>();
        fill.transform.parent.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        hs = GameObject.FindGameObjectWithTag("Enemy");
        if(fill && hs)
        {
            fill.value = hs.GetComponent<HealthScript>().healthPoints;
        }
        
     
    }
}
