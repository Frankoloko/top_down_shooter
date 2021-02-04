using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 velocity = new Vector2(0.0f, 0.0f);
    public GameObject shooter;
    Sound sound;
    Game game;
    Settings settings;

    void Start() {
        settings = GameObject.Find("Settings").GetComponent<Settings>();
        game = GameObject.Find("Game").GetComponent<Game>();
        sound = GameObject.Find("Sound").GetComponent<Sound>();
        
        sound.PlayShoot();
    }

    void Update() {
        // Get the bullet's current position and new position (next frame position)
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 newPosition = currentPosition + velocity * Time.deltaTime;

        // Cast a ray between the current position and the new position, and check if there are any objects it runs in to
        RaycastHit2D[] hits = Physics2D.LinecastAll(currentPosition, newPosition);

        // Check each object that was hit
        foreach(RaycastHit2D hit in hits) {
            GameObject other = hit.collider.gameObject;

            // If the object is not the player it self
            if (other != shooter) {

                // If the object is tagged enemy
                if (other.CompareTag("Enemy")) {
                    
                    // Destory the bullet it self
                    Destroy(gameObject);

                    // Get the enemy's object so we can subtract health from it
                    GameObject enemyObject = GameObject.Find(other.name);
                    E_BaseInterface enemy = enemyObject.GetComponent<E_BaseInterface>();
                    enemy.GotHit();
                }
            }
        }

        transform.position = newPosition;
    }
}
