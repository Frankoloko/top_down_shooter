using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Divide_S : MonoBehaviour, E_BaseInterface
{
    float health;

    void Start()
    {
        health = BalancingSettings.e_Divide.S_Health;
    }

    void Update()
    {
        Transform temp = transform;
        E_BASE.MoveTowardsPlayer(ref temp, BalancingSettings.e_Divide.S_MovementSpeed);
        transform.position = temp.position;
    }

    public void GotHit()
    {
        GameObject temp = gameObject;
        E_BASE.BasicDamage(ref health, ref temp);
    }
}