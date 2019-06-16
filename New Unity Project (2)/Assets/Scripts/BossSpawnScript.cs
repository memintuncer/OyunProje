using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnScript : MonoBehaviour
{
    public GameObject Boss;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Boss.SetActive(true);
        }
    }
}
