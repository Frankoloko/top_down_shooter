using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Brown : MonoBehaviour, E_BaseInterface
{
    Settings settings;
    public float health;

    void Start()
    {
        settings = GameObject.Find("Settings").GetComponent<Settings>();
        health = settings.e_Brown.health;
        E_BASE.SpawnOutsideCamera(transform);
    }

    void Update()
    {
        E_BASE.MoveTowardsPlayer(transform, settings.e_Brown.movementSpeed);
    }

    public void GotHit()
    {
        E_BASE.BasicDamage(health, gameObject);
    }
}