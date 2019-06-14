using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyPadPuzzle : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    //public GameObject TextMP;
    public float score=0f;
    public GameObject puzzleGUI;
    public GameObject player;
    public GameObject KeyCollider;
    private int size = 0;
    public string password;
    public GameObject TimeCont;
    public GameObject table;
    public GameObject image5;
    public GameObject image9;
    public GameObject image1;
    public GameObject image7;
    public GameObject imageOK;
    public GameObject pressButton;
    GameObject[] images;

    
    // Start is called before the first frame update
    void Start()
    {
        //textMesh = TextMP.GetComponent<TextMeshProUGUI>();
       
       
    }

    // Update is called once per frame
    void Update()
    {
        // textMesh.text = score.ToString();

        //if (score !=50)
        //{
        if (table.gameObject.GetComponent<GlassPuzzle>().isTaken == true)
        {
            images = GameObject.FindGameObjectsWithTag("FingerPrints");
            
            foreach (GameObject img in images)
            {
                img.SetActive(true);
            }
            image5.SetActive(true);
            image9.SetActive(true);
            image1.SetActive(true);
            image7.SetActive(true);
            imageOK.SetActive(true);

        }
            score++;
        //}
        
    }

    public void passInput(string key)
    {
        if (size < 4)
        {
            password += key;
            textMesh.text = password;
            size++;
        }
        
    }
    public void solvePuzzle()
    {
        
        if (textMesh.text == "5917" )
        {
            Destroy(puzzleGUI);
            Destroy(gameObject);
            Destroy(KeyCollider);
            player.SetActive(true);
            TimeCont.SetActive(true);
            pressButton.SetActive(false);

        }
        else
        {
            password = "";
            size=0;
            textMesh.text = password;
        }
    }

    public void backToGame()
    {
        puzzleGUI.SetActive(false);
        player.SetActive(true);
        TimeCont.SetActive(true);
        //Destroy(KeyCollider);
        password = "";
        size = 0;
        textMesh.text = password;
    }

    public void deletekey()
    {
        string pass1=password.Substring(0, password.Length - 1);
        password = pass1;
        textMesh.text = password;

        size--;
    }
}
