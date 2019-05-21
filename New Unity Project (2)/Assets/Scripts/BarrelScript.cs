using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelScript : MonoBehaviour
{
    SphereCollider collider;
    MeshRenderer mr;
    void Start()
    {
        //Assigns the attached SphereCollider to myCollider
        collider = gameObject.GetComponent<SphereCollider>();
        mr = gameObject.GetComponent<MeshRenderer>();
    }

    public void Boom()
    {
        var ps = GetComponentsInChildren<ParticleSystem>();
        foreach (var p in ps) 
            p.Play();
        var overlap = Physics.OverlapSphere(transform.position, 10);
        foreach (var obj in overlap)
            if (obj.GetComponent<Rigidbody>() != null)
                //obj.GetComponent<Rigidbody>().AddExplosionForce(200, transform.position + Vector3.down, 10);
        collider.radius = 10;
        mr.enabled = false;
        Destroy(gameObject, 0.6f);
        //StartCoroutine("wait");
        
    }
    //IEnumerator wait()
    //{
    //    yield return new WaitForSeconds(0.6f);
    //    Destroy(gameObject);
    //}

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            HealthScript h = collision.gameObject.GetComponent<HealthScript>();
            h.Damage(1);
        }
    }
}
