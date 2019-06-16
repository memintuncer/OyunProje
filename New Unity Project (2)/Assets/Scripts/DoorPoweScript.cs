using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorPoweScript : MonoBehaviour
{
    public GameObject buttonUI;
    public TextMeshProUGUI textMesh;
    private Light myLight;
    public bool isPowered = false;
    public bool here = false;
    public GameObject Light;
    // Start is called before the first frame update
    void Start()
    {
        myLight = Light.gameObject.GetComponent<Light>();
        myLight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (here)
        {
            if (Input.GetKeyDown(KeyCode.F)&& textMesh.text == "Give Power to Door")
            {
                isPowered = true;
                myLight.enabled = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player"&& isPowered==false)
        {
            textMesh.text = "Give Power to Door";
            here = true;
            buttonUI.SetActive(true);


        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            here = false;
            buttonUI.SetActive(false);

        }
    }
}
