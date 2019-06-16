using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GlassPuzzle : MonoBehaviour
{
    public GameObject buttonPanel;
    public TextMeshProUGUI textMesh;
    public GameObject bino;
    
    public bool isTaken = false;
    public bool here = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (here)
        {
            if (Input.GetKeyDown(KeyCode.F)&& textMesh.text == "Take Glasses"){
                isTaken = true;
                if (bino != null)
                {
                    Destroy(bino);
                   
                }
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && isTaken==false)
        {
            textMesh.text = "Take Glasses";
            here = true;
            buttonPanel.SetActive(true);
            

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            here = false;
            buttonPanel.SetActive(false);

        }
    }
}
