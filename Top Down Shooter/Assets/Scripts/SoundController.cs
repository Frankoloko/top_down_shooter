using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource soundShoot;
    public AudioSource soundHit;
    public AudioSource soundDestroy;
    public AudioSource soundTeleport;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayShoot()
    {
        soundShoot.Play();
    }

    public void PlayHit()
    {
        soundHit.Play();
    }

    public void PlayDestroy()
    {
        soundDestroy.Play();
    }

    public void PlayTeleport()
    {
        soundTeleport.Play();
    }
}
