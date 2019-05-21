using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public float healthPoints = 1.0f;
    [HideInInspector] public bool isDead=false;
    public void Damage(float amount)
    {
        healthPoints -= amount;
        if (healthPoints <= 0f) isDead = true;
    }
}
