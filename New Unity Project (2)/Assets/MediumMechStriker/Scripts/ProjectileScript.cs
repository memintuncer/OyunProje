using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField]
    private float secondsBeforeHoming;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float rotationAmount;
    [SerializeField]
    private float projectileSpeed;

    private bool shouldFollow;
    private GameObject target;

    public ParticleSystem explosionParticle;
    [Tooltip("Blood prefab particle this bullet will create upoon hitting enemy")]
    public GameObject bloodEffect;
    //Audio
    public AudioClip launchAudio;
    public AudioClip explosionAudio;
    private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        shouldFollow = false;
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(WaitBeforeHoming());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (shouldFollow)
        {
            Vector3 direction = target.transform.position - rb.position;
            direction.Normalize();
            Vector3 rotationAmount = Vector3.Cross(transform.forward, direction);
            rb.angularVelocity = rotationAmount * rotationSpeed;
            rb.velocity = transform.forward * projectileSpeed;
        }
    }

    IEnumerator WaitBeforeHoming()
    {
        audio.PlayOneShot(launchAudio);
        rb.AddForce(Vector3.up * 15, ForceMode.Impulse);
        yield return new WaitForSeconds(secondsBeforeHoming);
        shouldFollow = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<MeshCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        audio.PlayOneShot(explosionAudio);
        explosionParticle.Play();
        if (other.tag == "Crystal")
        {
            other.GetComponent<LineRenderer>().enabled = false;
        }
        else if (other.tag == "Player" || other.tag == "Enemy")
        {
            HealthScript h = other.gameObject.GetComponent<HealthScript>();
            h.Damage(1);
            Instantiate(bloodEffect, this.transform.position, other.transform.rotation);
        }
        else if (other.gameObject.tag == "Barrel")
        {
            BarrelScript bs = other.gameObject.GetComponent<BarrelScript>();
            bs.Boom();

        }
        Destroy(gameObject, 0.5f);

    }
}
