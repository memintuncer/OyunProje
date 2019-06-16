using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AudioController:MonoBehaviour
{
    GameObject mech;
    HealthScript hs;
    AudioSource audio;
    private bool coroutineStarted;
    private void Start()
    {

        audio = GetComponent<AudioSource>();
        coroutineStarted = false;
    }
    private void Update()
    {
        //Mech is inactive at the start so cant get health script
        if(mech)
        {
            if (hs.isDead && !coroutineStarted)
            {                
                StartCoroutine(FadeOut(audio, 10.0f));
                coroutineStarted = true;
            }
        }

        else
        {
            mech = GameObject.FindGameObjectWithTag("Enemy");
            if (mech)
            {
                hs = mech.GetComponent<HealthScript>();
            }
            
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            audio.Play();
            transform.GetChild(0).gameObject.SetActive(true);
        }
        
    }

    public IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.unscaledDeltaTime / FadeTime;
            yield return null;
        }
        audioSource.Stop();
    }
    public IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        audioSource.Play();
        audioSource.volume = 0f;
        while (audioSource.volume < 1)
        {
            audioSource.volume += Time.unscaledDeltaTime / FadeTime;
            yield return null;
        }
    }
}