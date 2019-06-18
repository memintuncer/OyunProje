using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelScript : MonoBehaviour
{
    SphereCollider collider;
    MeshRenderer renderer;
    MeshRenderer[] renderers;
   private AudioSource boomAudio;

    void Start()
    {
        //Assigns the attached SphereCollider to myCollider
        
        collider = gameObject.GetComponent<SphereCollider>();
        boomAudio = GetComponent<AudioSource>();
        
        renderer = gameObject.GetComponent<MeshRenderer>();
        if(renderer == null)
        {
            renderers = gameObject.GetComponentsInChildren<MeshRenderer>();
            
        }
        



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
        collider.radius = 20;
        
        if (!renderer)
        {
            foreach (MeshRenderer mesh in renderers)
            {
                mesh.enabled = false;
            }
        }
        else
        {
            renderer.enabled = false;
        }
        boomAudio.Play();
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
        else if(collision.gameObject.tag == "Barrel")
        {
            BarrelScript barrel = collision.gameObject.GetComponent<BarrelScript>();
            barrel.Boom();
        }
    }
}
