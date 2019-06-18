using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TurretScript1 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Turret;
    public GameObject openPanel = null;
    public Text hint;
    public bool isDisabled = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsOpenPanelActive)
        {
            if (Input.GetKey(KeyCode.F) && hint.text == "Stop Turret")
            {
                isDisabled = true;
                Turret.GetComponent<GatlingGun>().DisableTurret();

            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && isDisabled == false)
        {
            hint.text = "Stop Turret";
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
