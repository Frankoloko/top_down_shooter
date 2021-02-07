using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound: MonoBehaviour
{
    public AudioSource soundDestroy;
    public AudioSource soundGameOver;
    public AudioSource soundHit;
    public AudioSource soundSelect;
    public AudioSource soundSelected;
    public AudioSource soundShoot;
    public AudioSource soundTeleport;

    void Awake() {
        // This stops this GameObject from getting destroyed when the scene switches (so that the sounds are still accessible)
        DontDestroyOnLoad(this.gameObject);
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

    public void PlayGameOver()
    {
        soundGameOver.Play();
    }

    public void PlaySelect()
    {
        soundSelect.Play();
    }

    public void PlaySelected()
    {
        soundSelected.Play();
    }
}
