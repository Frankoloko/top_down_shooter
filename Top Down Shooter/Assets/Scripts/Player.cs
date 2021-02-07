using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public new Rigidbody2D rigidbody;
    public GameObject ProjectilePrefab;
    float maxCameraHeight;
    float maxCameraWidth;
    Vector2 movement;

    public float BulletDestroyTime = 5f;
    float moveBackFromScreenBorder = 0.5f;
    bool cloneActive = false;
    bool shootOnCooldown = false;

    Sound sound;

    void Start()
    {
        sound = GameObject.Find("Sound").GetComponent<Sound>();

        maxCameraHeight = Camera.main.orthographicSize - moveBackFromScreenBorder;
        maxCameraWidth = Camera.main.orthographicSize * Camera.main.aspect - moveBackFromScreenBorder;
    }

    void Update()
    {
        // Get movement direction value
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.E)) {
            Flash();
        } else if (Input.GetKeyDown(KeyCode.Q)) {
            Clone();
        }

        MovePlayer();
        ShootProjectile();
    }

    void Flash()
    {
        // Up
        if (Input.GetKey(KeyCode.UpArrow)) {
            float new_y = transform.position.y + BalancingSettings.flash.distance;
            if (new_y > maxCameraHeight) {
                new_y = maxCameraHeight;
            }
            transform.position = new Vector2(transform.position.x, new_y);
        }
        // Down
        if (Input.GetKey(KeyCode.DownArrow)) {
            float new_y = transform.position.y - BalancingSettings.flash.distance;
            if (new_y < maxCameraHeight * -1) {
                new_y = maxCameraHeight * -1;
            }
            transform.position = new Vector2(transform.position.x, new_y);
        }
        // Right
        if (Input.GetKey(KeyCode.RightArrow)) {
            float new_x = transform.position.x + BalancingSettings.flash.distance;
            if (new_x > maxCameraWidth) {
                new_x = maxCameraWidth;
            }
            transform.position = new Vector2(new_x, transform.position.y);
        }
        // Left
        if (Input.GetKey(KeyCode.LeftArrow)) {
            float new_x = transform.position.x - BalancingSettings.flash.distance;
            if (new_x < maxCameraWidth * -1) {
                new_x = maxCameraWidth * -1;
            }
            transform.position = new Vector2(new_x, transform.position.y);
        }

        // Play teleport sound
        sound.PlayTeleport();
    }

    void MovePlayer()
    {
        // Only do something if the player is actually giving movement input
        if (!(movement.x == 0 & movement.y == 0)) {
            // Normal sideways movement
            Vector2 newPosition = rigidbody.position + movement * BalancingSettings.player.movementSpeed * Time.fixedDeltaTime;

            // Only move the player if they aren't outside the camera borders
            if (newPosition[1] > maxCameraHeight) {
                newPosition[1] = maxCameraHeight;
            }
            if (newPosition[1] < maxCameraHeight * -1) {
                newPosition[1] = maxCameraHeight * -1;
            }
            if (newPosition[0] > maxCameraWidth) {
                newPosition[0] = maxCameraWidth;
            }
            if (newPosition[0] < maxCameraWidth * -1) {
                newPosition[0] = maxCameraWidth * -1;
            }

            rigidbody.MovePosition(newPosition);
        }
    }

    void ShootProjectile()
    {
        // This is just a timer that delays the player form shooting bullets as fast as they can click
        if (shootOnCooldown) {
            return;
        }

        // Check if any of the shoot codes are being pressed
        if (Input.GetKey(KeyCode.A) ^ Input.GetKey(KeyCode.D) ^ Input.GetKey(KeyCode.S) ^ Input.GetKey(KeyCode.W)) {

            // Get the velocity (direction) of the bullet
            Vector2 velocity = new Vector2(0.0f, 0.0f);

            if (Input.GetKey(KeyCode.A)) {
                velocity = new Vector2(BalancingSettings.player.bulletSpeed * -1, 0.0f);
            }
            if (Input.GetKey(KeyCode.D)) {
                velocity = new Vector2(BalancingSettings.player.bulletSpeed, 0.0f);
            }
            if (Input.GetKey(KeyCode.W)) {
                velocity = new Vector2(0.0f, BalancingSettings.player.bulletSpeed);
            }
            if (Input.GetKey(KeyCode.S)) {
                velocity = new Vector2(0.0f, BalancingSettings.player.bulletSpeed * -1);
            }

            // Create new projectile
            GameObject projectile = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
            Bullet bullet = projectile.GetComponent<Bullet>();

            // Give new bullet the correct velocity
            bullet.velocity = velocity;
            bullet.shooter = gameObject;

            // Start the shoot timeout delay
            shootOnCooldown = true;
            StartCoroutine(ShootOffTimeout());

            // Destory the new projectile after an X amount of time
            Destroy(projectile, BulletDestroyTime);
        }
    }

    IEnumerator ShootOffTimeout()
    {
        yield return new WaitForSeconds(BalancingSettings.player.shootCooldown);
        shootOnCooldown = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Get the REAL Player's (not the Clone's) cloneActive value
        bool tempCloneActive = false;
        if (gameObject.name != "Player") {
            // This current function is running from the Clone, so get the original Player's cloneActive value
            Player player = GameObject.Find("Player").GetComponent<Player>();
            tempCloneActive = player.cloneActive;
        } else {
            // This current function is running from the Player, so just use its cloneActive value
            tempCloneActive = cloneActive;
        }

        // Check if the Clone is active, if true, destory it, if false, end the game
        if (tempCloneActive) {
            // A clone is alive, so kill the clone object
            GameObject cloneObject = GameObject.Find("Player(Clone)");
            if (gameObject.name == "Player") {
                // If the Player was collided with, kill the clone but move the Player to the clone's position
                transform.position = cloneObject.transform.position;
                Destroy(cloneObject);
                cloneActive = false;
            } else {
                // If the Clone was collided with, kill it, but turn the cloneActive to true on the Player object
                Destroy(cloneObject);
                Player player = GameObject.Find("Player").GetComponent<Player>();
                player.cloneActive = false;
            }
        } else {
            // No clone alive so just end the game
            sound.PlayGameOver(); // Doesn't play because the object gets destroyed when the scene ends
            SceneManager.LoadScene("Menu");
        }
    }

    void Clone()
    {
        // This if stops the player from making multiple clones. We only allow the original "Player" object to create clones
        if (cloneActive ^ gameObject.name != "Player") {
            return;
        }

        // No clone is alive, so create a new one
        cloneActive = true;
        GameObject clone = Instantiate(gameObject, new Vector3(transform.position.x - 10f, transform.position.y, transform.position.z), Quaternion.identity);

        // Destory the clone after an x amount of time
        StartCoroutine(DestoryClone(clone));
    }

    IEnumerator DestoryClone(GameObject clone)
    {
        // Destroy the clone after x amount of time
        yield return new WaitForSeconds(BalancingSettings.clone.duration);
        Destroy(clone);
        cloneActive = false;
    }
}
