using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class E_Shoot : MonoBehaviour, E_BaseInterface
{
    public GameObject bulletPrefab;
    float health;
    GameObject player;
    bool standStill;
    float maxCameraHeight;
    float maxCameraWidth;
    float moveBackFromScreenBorder = 0.5f;
    Vector2 moveToPosition;

    void Start()
    {
        maxCameraHeight = Camera.main.orthographicSize - moveBackFromScreenBorder;
        maxCameraWidth = Camera.main.orthographicSize * Camera.main.aspect - moveBackFromScreenBorder;

        health = Settings.e_Shoot.health;
        player = GameObject.Find("Player");

        Transform temp = transform;
        E_BASE.SpawnOutsideCamera(ref temp);
        transform.position = temp.position;

        InvokeRepeating("RepeatingPattern", 0f, Settings.e_Shoot.moveTime + Settings.e_Shoot.pauseBeforeShoot + Settings.e_Shoot.pauseAfterShoot);
    }

    void RepeatingPattern()
    {
        // Here we repeat 3 things:
            // 1: Move for x amount of time in a random direction
            // 2: Wait x amount of time before shooting
            // 3: Shoot
            // 4: Wait x amount of time after shooting

        // To do all this waiting, we need to start it in a Coroutine
        StartCoroutine(WaitCode());
    }
 
    IEnumerator WaitCode()
    {
        // 1: Move for x amount of time in a random direction. We don't actually move the unit here, we just set its direction. It gets moved in the update() method
            standStill = false;

            // Get a random direction to move in
            float randomX = Random.Range(-maxCameraWidth, maxCameraWidth);
            float randomY = Random.Range(-maxCameraHeight, maxCameraHeight);
            moveToPosition = new Vector2(randomX, randomY);
            yield return new WaitForSeconds(Settings.e_Shoot.moveTime);

        // 2: Wait x amount of time before shooting
            standStill = true;
            yield return new WaitForSeconds(Settings.e_Shoot.pauseBeforeShoot);
        
        // 3: Shoot
            ShootBullet();

        // 4: Wait x amount of time after shooting
            yield return new WaitForSeconds(Settings.e_Shoot.pauseAfterShoot);
    }

    void ShootBullet()
    {
        // This function is in charge of shooting the bullet at the player

        // Create new projectile
        GameObject created_bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        E_Shoot_Bullet bullet = created_bullet.GetComponent<E_Shoot_Bullet>();
        // Set its direction
        Vector2 velocity = player.transform.position - transform.position;
        bullet.velocity = velocity.normalized;
        bullet.shooter = gameObject;
    }

    void Update()
    {
        if (!standStill) {
            transform.position = Vector2.MoveTowards(transform.position, moveToPosition, Settings.e_Shoot.movementSpeed * Time.deltaTime);
        }
    }

    public void GotHit()
    {
        // Take basic damage when hit
        GameObject temp = gameObject;
        bool dead =  E_BASE.BasicDamage(ref health, ref temp);

        // If this unit dies, check if it is the first time it is getting hit
        if (dead) {
            if (!Settings.progress.e_Shoot_FirstKill) {
                // First kill, unlock the unit
                Settings.progress.e_Shoot_FirstKill = true;
                GameObject.Find("UnlockPopup").transform.localScale = new Vector3(1f, 1f, 1f);
                Sprite sprite = Resources.Load<Sprite>("Enemies/E_Shoot");
                GameObject.Find("UnlockImage").GetComponent<Image>().sprite = sprite;
                Time.timeScale = 0;
            }
        }
    }
}