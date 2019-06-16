using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpaceShipScript : MonoBehaviour
{
    public GameObject LevelEndCollider;
    //Rolls the credits
    private bool canMove;
    private bool coroutineStarted;
    private RectTransform rect;
    public GameObject creditsCam;
    // Start is called before the first frame update
    void Start()
    {
        canMove = false;
        coroutineStarted = false;
        rect = GameObject.FindGameObjectWithTag("Credits").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
          rect.transform.Translate(new Vector3(0,0.25f,-.5f) * Time.unscaledDeltaTime * 0.75f , Space.World);
            if (!coroutineStarted)
            {
                StartCoroutine(WaitAndLoadNextScene());
                coroutineStarted = true;

            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(GameObject.FindGameObjectWithTag("Player").gameObject);
            Destroy(LevelEndCollider.gameObject);
            GameObject.FindGameObjectWithTag("Credits").GetComponent<MeshRenderer>().enabled = true ;
            creditsCam.GetComponent<Camera>().enabled = true;
            GetComponent<Animator>().enabled = true;

            StartCoroutine(Wait());

            
           
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);        
        canMove = true;
    }

    IEnumerator WaitAndLoadNextScene()
    {
        yield return new WaitForSeconds(35);
        StaticLevelInfo.NextSceneToLoad = 0;
        SceneManager.LoadScene(1);
    }
}
