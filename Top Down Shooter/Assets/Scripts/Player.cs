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

    public enum Ability1{Teleport, Invisible, TimeStop}
    public Ability1 ability1;

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        ShootProjectile();

        if (Input.GetKeyDown("e")) {
            if (movement.y > 0) {
                // Up
                rigidbody.MovePosition(new Vector2(rigidbody.position.x, rigidbody.position.y + 10f) + movement * movementSpeed * Time.fixedDeltaTime);
            }
            if (movement.y < 0) {
                // Down
                rigidbody.MovePosition(new Vector2(rigidbody.position.x, rigidbody.position.y - 10f) + movement * movementSpeed * Time.fixedDeltaTime);
            }
            if (movement.x > 0) {
                // Right
                rigidbody.MovePosition(new Vector2(rigidbody.position.x + 10f, rigidbody.position.y) + movement * movementSpeed * Time.fixedDeltaTime);
            }
            if (movement.x < 0) {
                // Left
                rigidbody.MovePosition(new Vector2(rigidbody.position.x - 10f, rigidbody.position.y) + movement * movementSpeed * Time.fixedDeltaTime);
            }
        } else {
            rigidbody.MovePosition(rigidbody.position + movement * movementSpeed * Time.fixedDeltaTime);
        }
    }

    // void FixedUpdate()
    // {

    // }

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
