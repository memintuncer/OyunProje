using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
   Actions act;
   public float health = 50f;
   public void RemoveHealth(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            act.Death();
            //gameObject.active = false;
        }
    }
}
