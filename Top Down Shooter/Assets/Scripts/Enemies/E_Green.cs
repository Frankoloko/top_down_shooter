using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Green : MonoBehaviour, E_BaseInterface
{
    float health;

    void Start()
    {
        health = BalancingSettings.e_Green.health;

        Transform temp = transform;
        E_BASE.SpawnOutsideCamera(ref temp);
        transform.position = temp.position;
    }

    void Update()
    {
        Transform temp = transform;
        E_BASE.MoveTowardsPlayer(ref temp, BalancingSettings.e_Green.movementSpeed);
        transform.position = temp.position;
    }

    public void GotHit()
    {
        GameObject temp = gameObject;
        E_BASE.BasicDamage(ref health, ref temp);
    }
}