using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public new Rigidbody2D rigidbody;
    public Animator animator;
    public GameObject ProjectilePrefab;
    Settings settings;
    float maxCameraHeight;
    float maxCameraWidth;
    Vector2 movement;

    public float BulletDestroyTime = 5f;
    float moveBackFromScreenBorder = 0.5f;
    bool cloneActive = false;

    // public enum Ability1{Flash, Invisible, TimeStop}
    // public Ability1 ability1;

    void Start()
    {
        settings = GameObject.Find("Settings").GetComponent<Settings>();

        maxCameraHeight = Camera.main.orthographicSize - moveBackFromScreenBorder;
        maxCameraWidth = Camera.main.orthographicSize * Camera.main.aspect - moveBackFromScreenBorder;
    }

    void Update()
    {
        if (Input.GetKeyDown("e")) {
            Flash();
        } else if (Input.GetKeyDown("q")) {
            Clone();
        } else {
            // Normal sideways movement
            Vector2 newPosition = rigidbody.position + movement * settings.player.movementSpeed * Time.fixedDeltaTime;

            // Only move the player if they aren't already outside the borders
            if (!(newPosition[1] > maxCameraHeight ^ newPosition[1] < maxCameraHeight * -1 ^ newPosition[0] > maxCameraWidth ^ newPosition[0] < maxCameraWidth * -1)) {
                rigidbody.MovePosition(newPosition);
            }
        }

        MovePlayer();
        ShootProjectile();
    }

    void FixedUpdate()
    {

    }

    void Flash()
    {
        float flashDistance = 10f;
        if (animator.GetFloat("Vertical") > 0) {
            // Up
            float new_y = transform.position.y + flashDistance;
            if (new_y > maxCameraHeight) {
                new_y = maxCameraHeight;
            }
            transform.position = new Vector2(transform.position.x, new_y);
        }
        if (animator.GetFloat("Vertical") < 0) {
            // Down
            float new_y = transform.position.y - flashDistance;
            if (new_y < maxCameraHeight * -1) {
                new_y = maxCameraHeight * -1;
            }
            transform.position = new Vector2(transform.position.x, new_y);
        }
        if (animator.GetFloat("Horizontal") > 0) {
            // Right
            float new_x = transform.position.x + flashDistance;
            if (new_x > maxCameraWidth) {
                new_x = maxCameraWidth;
            }
            transform.position = new Vector2(new_x, transform.position.y);
        }
        if (animator.GetFloat("Horizontal") < 0) {
            // Left
            float new_x = transform.position.x - flashDistance;
            if (new_x < maxCameraWidth * -1) {
                new_x = maxCameraWidth * -1;
            }
            transform.position = new Vector2(new_x, transform.position.y);
        }
        Sound sound = GameObject.Find("Sound").GetComponent<Sound>();
        sound.PlayTeleport();
    }

    void MovePlayer()
    {
        // Get movement direction value
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // This IF stops the animation to go back to the "default DOWN" spite. Since the Speed is left at > 0 (the previous frame) the spite will be left at what it last was
        if (!(movement.x == 0 & movement.y == 0)) {
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
    }

    void ShootProjectile()
    {
        // Shooting code
        if (Input.GetKeyDown("a") ^ Input.GetKeyDown("d") ^ Input.GetKeyDown("s") ^ Input.GetKeyDown("w")) {
            // Get the velocity (direction)
            Vector2 velocity = new Vector2(0.0f, 0.0f);
            if (Input.GetKeyDown("a")) {
                velocity = new Vector2(settings.player.bulletSpeed * -1, 0.0f);
            }
            if (Input.GetKeyDown("d")) {
                velocity = new Vector2(settings.player.bulletSpeed, 0.0f);
            }
            if (Input.GetKeyDown("w")) {
                velocity = new Vector2(0.0f, settings.player.bulletSpeed);
            }
            if (Input.GetKeyDown("s")) {
                velocity = new Vector2(0.0f, settings.player.bulletSpeed * -1);
            }

            // Create new projectile
            GameObject projectile = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
            Bullet bullet = projectile.GetComponent<Bullet>();
            // Give new projective velocity
            bullet.velocity = velocity;
            bullet.shooter = gameObject;
            // Destory the new projectile after an X amount of time
            Destroy(projectile, BulletDestroyTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Get the Player (not the Clone) cloneActive value
        bool tempCloneActive = false;
        if (gameObject.name != "Player") {
            // THIS current function is running from the Clone, so get the original Player's cloneActive value
            Player player = GameObject.Find("Player").GetComponent<Player>();
            tempCloneActive = player.cloneActive;
        } else {
            // THIS current function is running from the Player, so just use its cloneActive value
            tempCloneActive = cloneActive;
        }

        // Check if the Clone is active, if true, destory it, if false, end the game
        if (tempCloneActive) {
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
            SceneManager.LoadScene("Menu");
        }
    }

    void Clone()
    {
        // This if stops the player from making multiple clones
        // It also stops the clone from making more clones
        if (cloneActive ^ gameObject.name != "Player") {
            return;
        }
        cloneActive = true;
        GameObject clone = Instantiate(gameObject, new Vector3(transform.position.x - 10f, transform.position.y, transform.position.z), Quaternion.identity);
        StartCoroutine(DestoryClone(clone));
    }

    IEnumerator DestoryClone(GameObject clone)
    {
        yield return new WaitForSeconds(settings.clone.duration);
        Destroy(clone);
        cloneActive = false;
    }
}
