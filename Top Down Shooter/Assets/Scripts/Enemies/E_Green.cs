using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Green : MonoBehaviour, E_BaseInterface
{
    Settings settings;
    public float health;

    void Start()
    {
        settings = GameObject.Find("Settings").GetComponent<Settings>();
        health = settings.e_Green.health;
        E_BASE.SpawnOutsideCamera(transform);
    }

    void Update()
    {
        E_BASE.MoveTowardsPlayer(transform, settings.e_Green.movementSpeed);
    }

    public void GotHit()
    {
        E_BASE.BasicDamage(health, gameObject);
    }
}