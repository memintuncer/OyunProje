using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveWeaponScript : MonoBehaviour
{
    
    public bool pushed=false;
    public GameObject weapon;
    //public GameObject Tb;

    public GameObject openPanel = null;
    //public GameObject weapon;
    private bool ispanel = false;
    //private GunInventory gi;

   
    void  Start()
    {
       
    }

    
    void Update()
    {
        if (IsOpenPanelActive)
        {
            if (Input.GetKey(KeyCode.F))
            {
                pushed = true;
                if (weapon.activeInHierarchy == false)
                {
                    weapon.SetActive(true);
                }
                
  
            }

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            openPanel.SetActive(true);
            ispanel = true;


        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            openPanel.SetActive(false);
            ispanel = false;
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
