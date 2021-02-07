using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Shoot : MonoBehaviour, E_BaseInterface
{
    public GameObject bulletPrefab;
    float health;
    GameObject player;

    void Start()
    {
        health = BalancingSettings.e_Shoot.health;
        player = GameObject.Find("Player");

        Transform temp = transform;
        E_BASE.SpawnOutsideCamera(ref temp);
        transform.position = temp.position;

        InvokeRepeating("ShootBullet", 0f, BalancingSettings.e_Shoot.shotDelay);
    }

    void ShootBullet()
    {
        // Create new projectile
        GameObject created_bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        E_Shoot_Bullet bullet = created_bullet.GetComponent<E_Shoot_Bullet>();
        Vector2 velocity = player.transform.position - transform.position;
        bullet.velocity = velocity.normalized;
        bullet.shooter = gameObject;
    }

    void Update()
    {
        Transform temp = transform;
        E_BASE.MoveTowardsPlayer(ref temp, BalancingSettings.e_Shoot.movementSpeed);
        transform.position = temp.position;
    }

    public void GotHit()
    {
        GameObject temp = gameObject;
        bool dead =  E_BASE.BasicDamage(ref health, ref temp);

        if (dead) {
            if (!BalancingSettings.e_Shoot.firstKill) {
                // First kill, unlock the unit
                BalancingSettings.e_Shoot.firstKill = true;
                GameObject.Find("UnlockPopup").transform.localScale = new Vector3(1f, 1f, 1f);
                print(GameObject.Find("UnlockImage").GetComponent<UnityEngine.UI.Image>().sprite);
                Time.timeScale = 0;
            }
        }
    }
}