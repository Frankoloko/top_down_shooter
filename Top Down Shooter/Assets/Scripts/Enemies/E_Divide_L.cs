using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Divide_L : MonoBehaviour, E_BaseInterface
{
    public GameObject e_Divide_M;
    float health;

    void Start()
    {
        health = BalancingSettings.e_Divide.L_Health;

        Transform temp = transform;
        E_BASE.SpawnOutsideCamera(ref temp);
        transform.position = temp.position;
    }

    void Update()
    {
        Transform temp = transform;
        E_BASE.MoveTowardsPlayer(ref temp, BalancingSettings.e_Divide.L_MovementSpeed);
        transform.position = temp.position;
    }

    public void GotHit()
    {
        GameObject temp = gameObject;
        bool dead = E_BASE.BasicDamage(ref health, ref temp, false);

        // If E_Divide_L dies, we need to spawn 2 of E_Divide_M
        if (dead) {
            GameObject created_enemy;
            System.Guid newGuid;

            // Create new enemy
            created_enemy = Instantiate(e_Divide_M);
            created_enemy.transform.position = transform.position + new Vector3(-2f, 2f, 0f);
            newGuid = System.Guid.NewGuid();
            created_enemy.name += newGuid;
            // Create new enemy
            created_enemy = Instantiate(e_Divide_M);
            created_enemy.transform.position = transform.position + new Vector3(2f, -2f, 0f);
            newGuid = System.Guid.NewGuid();
            created_enemy.name += newGuid;
        }
    }
}