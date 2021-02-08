using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Divide_M : MonoBehaviour, E_BaseInterface
{
    public GameObject e_Divide_S;
    float health;

    void Start()
    {
        health = Settings.e_Divide.M_Health;
    }

    void Update()
    {
        Transform temp = transform;
        E_BASE.MoveTowardsPlayer(ref temp, Settings.e_Divide.M_MovementSpeed);
        transform.position = temp.position;
    }

    public void GotHit()
    {
        GameObject temp = gameObject;
        bool dead = E_BASE.BasicDamage(ref health, ref temp, false);

        // If E_Divide_L dies, we need to spawn 4 of E_Divide_S
        if (dead) {
            GameObject created_enemy;
            System.Guid newGuid;

            // Create new enemy
            created_enemy = Instantiate(e_Divide_S);
            created_enemy.transform.position = transform.position + new Vector3(0f, 0.5f, 0f);
            newGuid = System.Guid.NewGuid();
            created_enemy.name += newGuid;
            // Create new enemy
            created_enemy = Instantiate(e_Divide_S);
            created_enemy.transform.position = transform.position + new Vector3(0f, -1.5f, 0f);
            newGuid = System.Guid.NewGuid();
            created_enemy.name += newGuid;
            // Create new enemy
            created_enemy = Instantiate(e_Divide_S);
            created_enemy.transform.position = transform.position + new Vector3(1f, 0f, 0f);
            newGuid = System.Guid.NewGuid();
            created_enemy.name += newGuid;
            // Create new enemy
            created_enemy = Instantiate(e_Divide_S);
            created_enemy.transform.position = transform.position + new Vector3(-2.5f, 0f, 0f);
            newGuid = System.Guid.NewGuid();
            created_enemy.name += newGuid;
        }
    }
}