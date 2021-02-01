using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [Header("Objects")]
    public new Rigidbody2D rigidbody;
    public Animator animator;
    public GameObject ProjectilePrefab;

    [Space]
    [Header("Settings")]
    public float movementSpeed = 7f;
    public float ProjectileSpeed = 18f;
    public float DestroyTime = 5f;

    [Space]
    [Header("Watchers")]
    public Vector2 movement;

    public enum Ability1{Flash, Invisible, TimeStop}
    public Ability1 ability1;

    float moveBackFromScreenBorder = 0.5f;
    float maxCameraHeight;
    float maxCameraWidth;

    void Start()
    {
        maxCameraHeight = Camera.main.orthographicSize - moveBackFromScreenBorder;
        maxCameraWidth = Camera.main.orthographicSize * Camera.main.aspect - moveBackFromScreenBorder;
    }

    void Update()
    {
        // Check for flash function
        if (Input.GetKeyDown("e")) {
            Flash();
        } else {
            // Normal sideways movement
            Vector2 newPosition = rigidbody.position + movement * movementSpeed * Time.fixedDeltaTime;

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
        SoundController soundController = GameObject.Find("SoundController").GetComponent<SoundController>();
        soundController.PlayTeleport();
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
                velocity = new Vector2(ProjectileSpeed * -1, 0.0f);
            }
            if (Input.GetKeyDown("d")) {
                velocity = new Vector2(ProjectileSpeed, 0.0f);
            }
            if (Input.GetKeyDown("w")) {
                velocity = new Vector2(0.0f, ProjectileSpeed);
            }
            if (Input.GetKeyDown("s")) {
                velocity = new Vector2(0.0f, ProjectileSpeed * -1);
            }

            // Create new projectile
            GameObject projectile = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
            Bullet bullet = projectile.GetComponent<Bullet>();
            // Give new projective velocity
            bullet.velocity = velocity;
            bullet.shooter = gameObject;
            // Destory the new projectile after an X amount of time
            Destroy(projectile, DestroyTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // If anything touches the player, end the game
        Time.timeScale = 0;
        Destroy(gameObject);
        // EditorApplication.isPlaying = false;
    }
}
