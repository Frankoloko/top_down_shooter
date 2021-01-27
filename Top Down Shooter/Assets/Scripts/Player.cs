using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Objects")]
    public Rigidbody2D rigidbody;
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

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        ShootProjectile();
    }

    void FixedUpdate()
    {
       rigidbody.MovePosition(rigidbody.position + movement * movementSpeed * Time.fixedDeltaTime);
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
        if (Input.GetKeyDown("left") ^ Input.GetKeyDown("right") ^ Input.GetKeyDown("down") ^ Input.GetKeyDown("up")) {
            // Get the velocity (direction)
            Vector2 velocity = new Vector2(0.0f, 0.0f);
            if (Input.GetKeyDown("left")) {
                velocity = new Vector2(ProjectileSpeed * -1, 0.0f);
            }
            if (Input.GetKeyDown("right")) {
                velocity = new Vector2(ProjectileSpeed, 0.0f);
            }
            if (Input.GetKeyDown("up")) {
                velocity = new Vector2(0.0f, ProjectileSpeed);
            }
            if (Input.GetKeyDown("down")) {
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
}
