using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Shoot : MonoBehaviour, E_BaseInterface
{
    public GameObject bulletPrefab;
    Settings settings;
    float health;

    void Start()
    {
        settings = GameObject.Find("Settings").GetComponent<Settings>();
        health = settings.e_Shoot.health;

        Transform temp = transform;
        E_BASE.SpawnOutsideCamera(ref temp);
        transform.position = temp.position;

        GameObject created_bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Bullet bullet = created_bullet.GetComponent<Bullet>();
        bullet.shooter = gameObject;

        // Give new bullet the correct velocity
        GameObject player = GameObject.Find("Player");
        bullet.velocity = player.transform.position;
    }

    void Update()
    {
        Transform temp = transform;
        E_BASE.MoveTowardsPlayer(ref temp, settings.e_Shoot.movementSpeed);
        transform.position = temp.position;
    }

    public void GotHit()
    {
        GameObject temp = gameObject;
        E_BASE.BasicDamage(ref health, ref temp);
    }
}