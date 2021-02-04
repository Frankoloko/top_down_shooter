using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Divide_M : MonoBehaviour, E_BaseInterface
{
    Settings settings;
    float health;

    void Start()
    {
        settings = GameObject.Find("Settings").GetComponent<Settings>();
        health = settings.e_Divide.M_Health;

        Transform temp = transform;
        E_BASE.SpawnOutsideCamera(ref temp);
        transform.position = temp.position;
    }

    void Update()
    {
        Transform temp = transform;
        E_BASE.MoveTowardsPlayer(ref temp, settings.e_Divide.M_MovementSpeed);
        transform.position = temp.position;
    }

    public void GotHit()
    {
        GameObject temp = gameObject;
        E_BASE.BasicDamage(ref health, ref temp);
    }
}