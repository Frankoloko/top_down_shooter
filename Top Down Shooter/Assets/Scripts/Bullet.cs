using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 velocity = new Vector2(0.0f, 0.0f);
    public GameObject shooter;

    void Update() {
        // Get the bullet's current position and new position (next frame position)
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 newPosition = currentPosition + velocity * Time.deltaTime;

        // Cast a ray between the current position and the new position, and check if there are any objects it runs in to
        RaycastHit2D[] hits = Physics2D.LinecastAll(currentPosition, newPosition);

        foreach(RaycastHit2D hit in hits) {
            GameObject other = hit.collider.gameObject;
            if (other != shooter) {
                if (other.CompareTag("Enemy")) {
                    Destroy(gameObject);
                    GameObject enemyObject = GameObject.Find(other.name);
                    EnemyBase enemy = enemyObject.GetComponent<EnemyBase>();
                    if (enemy.health == 1) {
                        Destroy(enemyObject);
                        break;
                    }
                    enemy.health = enemy.health - 1;
                    break;
                }

                if (other.CompareTag("Prop")) {
                    Destroy(gameObject);
                    break;
                }
            }
        }

        transform.position = newPosition;
    }
}
