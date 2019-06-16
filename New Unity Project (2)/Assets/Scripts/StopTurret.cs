using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class StopTurret : MonoBehaviour
{
    public GameObject Turret;
    public GameObject openPanel = null;
    public TextMeshProUGUI textMesh;
    public bool isDisabled=false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOpenPanelActive)
        {
            if (Input.GetKey(KeyCode.F)&& textMesh.text == "Stop Turret")
            {
                isDisabled = true;
                Turret.GetComponent<GatlingGun>().DisableTurret();
                
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player"&& isDisabled==false)
        {
            textMesh.text = "Stop Turret";
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
