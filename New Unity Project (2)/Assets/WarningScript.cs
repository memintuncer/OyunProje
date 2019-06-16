using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WarningScript : MonoBehaviour
{
    public GameObject buttonPanel;
    public TextMeshProUGUI textMesh;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" )
        {
            textMesh.text = "No Power";

            buttonPanel.SetActive(true);


        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {


            buttonPanel.SetActive(false);

        }
    }
}
