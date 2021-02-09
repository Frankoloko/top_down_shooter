using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class E_Teleport : MonoBehaviour, E_BaseInterface
{
    public GameObject bulletPrefab;
    float health;
    GameObject player;
    bool standStill;
    float maxCameraHeight;
    float maxCameraWidth;
    float moveBackFromScreenBorder = 0.5f;
    Vector2 moveToPosition;
    Sound sound;

    void Start()
    {
        sound = GameObject.Find("Sound").GetComponent<Sound>();

        maxCameraHeight = Camera.main.orthographicSize - moveBackFromScreenBorder;
        maxCameraWidth = Camera.main.orthographicSize * Camera.main.aspect - moveBackFromScreenBorder;

        health = Settings.e_Teleport.health;
        player = GameObject.Find("Player");

        Transform temp = transform;
        E_BASE.SpawnOutsideCamera(ref temp);
        transform.position = temp.position;

        InvokeRepeating("RepeatingPattern", 0f, Settings.e_Teleport.pauseBeforeShoot + Settings.e_Teleport.pauseAfterShoot);
    }

    void RepeatingPattern()
    {
        // Here we repeat 3 things:
            // 1: Teleport
            // 2: Wait x amount of time before shooting
            // 3: Shoot
            // 4: Wait x amount of time after shooting

        // To do all this waiting, we need to start it in a Coroutine
        StartCoroutine(WaitCode());
    }
 
    IEnumerator WaitCode()
    {
        // 1: Teleport
            float randomX = Random.Range(-maxCameraWidth, maxCameraWidth);
            float randomY = Random.Range(-maxCameraHeight, maxCameraHeight);
            transform.position = new Vector2(randomX, randomY);

        // 2: Create the projectile (it starts the scale growing by it self)
            GameObject created_bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            E_Teleport_Bullet bullet = created_bullet.GetComponent<E_Teleport_Bullet>();
            bullet.shooter = gameObject;

        // 2: Wait x amount of time before shooting
            yield return new WaitForSeconds(Settings.e_Teleport.pauseBeforeShoot);
        
        // 3: Shoot the bullet
            sound.PlayShoot();
            Vector2 velocity = player.transform.position - transform.position;
            bullet.velocity = velocity.normalized;

        // 4: Wait x amount of time after shooting
            yield return new WaitForSeconds(Settings.e_Teleport.pauseAfterShoot);
    }

    public void GotHit()
    {
        // Take basic damage when hit
        GameObject temp = gameObject;
        bool dead =  E_BASE.BasicDamage(ref health, ref temp);

        // If this unit dies, check if it is the first time it is getting hit
        if (dead) {
            if (!Settings.progress.e_Teleport_FirstKill) {
                // First kill, unlock the unit
                Settings.progress.e_Teleport_FirstKill = true;
                GameObject.Find("UnlockPopup").transform.localScale = new Vector3(1f, 1f, 1f);
                Sprite sprite = Resources.Load<Sprite>("Enemies/E_Teleport");
                GameObject.Find("UnlockImage").GetComponent<Image>().sprite = sprite;
                Time.timeScale = 0;
            }
        }
    }
}