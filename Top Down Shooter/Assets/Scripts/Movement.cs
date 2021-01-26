using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float movementSpeed = 5f;

    public Rigidbody2D rigidbody;
    public Animator animator;

    public GameObject ProjectilePrefab;
    public float ProjectileSpeed = 5f;
    public float DestroyTime = 5f;

    public Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        // Get movement direction value
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // This stops the animation to go back to the "default DOWN" spite. Since the Speed is left at > 0 (the previous frame) the spite will be left at what it last was
        if (!(movement.x == 0 & movement.y == 0)) {
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }

        // Shooting code
        if (Input.GetKeyDown("left")) {
            // Create new projectile
            GameObject projectile = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
            // Give new projective velocity
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(ProjectileSpeed * -1, 0.0f);
            Destroy(projectile, DestroyTime);
        }
        if (Input.GetKeyDown("right")) {
            // Create new projectile
            GameObject projectile = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
            // Give new projective velocity
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(ProjectileSpeed, 0.0f);
            Destroy(projectile, DestroyTime);
        }
        if (Input.GetKeyDown("up")) {
            // Create new projectile
            GameObject projectile = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
            // Give new projective velocity
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, ProjectileSpeed);
            Destroy(projectile, DestroyTime);
        }
        if (Input.GetKeyDown("down")) {
            // Create new projectile
            GameObject projectile = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
            // Give new projective velocity
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, ProjectileSpeed * -1);
            Destroy(projectile, DestroyTime);
        }
    }

    void FixedUpdate()
    {
       rigidbody.MovePosition(rigidbody.position + movement * movementSpeed * Time.fixedDeltaTime);
    }
}
