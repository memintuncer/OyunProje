using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    PlayerScript ps;

    // Start is called before the first frame update
    void Start()
    {
        ps = null;
    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(new Vector3(0, Time.unscaledDeltaTime * 100, 0));    
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GetComponent<AudioSource>().Play();
            ps = other.GetComponent<PlayerScript>();
            ps.bulletsInTheGun += 5;
            Destroy(gameObject,.5f);
        }
    }
}
