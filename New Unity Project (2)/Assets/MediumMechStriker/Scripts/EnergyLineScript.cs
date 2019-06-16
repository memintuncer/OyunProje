using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyLineScript : MonoBehaviour
{
    public GameObject target;          // Reference to the target GameObject
    LineRenderer lr;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the GameObjects are not null
        if (target != null)
        {
            // Update position of the two vertex of the Line Renderer
            lr.SetPosition(0, new Vector3(transform.position.x, transform.position.y + 2.0f, transform.position.z));
            lr.SetPosition(1, new Vector3(target.transform.position.x , target.transform.position.y, target.transform.position.z));
        }

    }
}
