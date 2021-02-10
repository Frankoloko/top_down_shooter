using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Teleport_Bullet : MonoBehaviour
{
    public Vector2 velocity = new Vector2(0.0f, 0.0f);
    public GameObject shooter;
    Game game;
    float bulletGrowAmountPerFrame = Settings.e_Teleport.bulletSize / Settings.e_Teleport.pauseBeforeShoot / 200;

    void Start() {
        game = GameObject.Find("Game").GetComponent<Game>();
    }

    void Update() {
        // Grow the bullet
        float bulletScale = gameObject.transform.localScale.x;
        if (bulletScale < Settings.e_Teleport.bulletSize) {
            gameObject.transform.localScale = new Vector3(bulletScale + bulletGrowAmountPerFrame, bulletScale + bulletGrowAmountPerFrame, bulletScale + bulletGrowAmountPerFrame);
        }

        // Check if the shooter has died before the ball started moving
        if (shooter == null & velocity == new Vector2(0f, 0f)) {
            Destroy(gameObject);
        }

        // Get the bullet's current position and new position (next frame position)
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 newPosition = currentPosition + velocity * Settings.e_Shoot.bulletSpeed * Time.deltaTime;

        // Cast a ray between the current position and the new position, and check if there are any objects it runs in to
        RaycastHit2D[] hits = Physics2D.LinecastAll(currentPosition, newPosition);

        // Check each object that was hit
        foreach(RaycastHit2D hit in hits) {
            GameObject other = hit.collider.gameObject;

            // If the object is not the player it self
            if (other != shooter) {

                // If the object is tagged enemy
                if (other.CompareTag("Player")) {
                    
                    // Destory the bullet it self
                    Destroy(gameObject);

                    // Get the enemy's object so we can subtract health from it
                    GameObject playerObject = GameObject.Find(other.name);
                    Destroy(playerObject);
                }
            }
        }

        transform.position = newPosition;
    }

    
}
