using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface E_BaseInterface
{
    void GotHit();
}

public class E_BASE
{    
    static public void SpawnOutsideCamera(ref Transform transform)
    {
        // Get the camera's borders
        float maxCameraHeight = Camera.main.orthographicSize + 2;
        float maxCameraWidth = Camera.main.orthographicSize * Camera.main.aspect + 2;
        
        // Create the spawn points (either outside the top border, or outside the side border)
        float spawnHeight = 0f;
        float spawnWidth = 0f;

        // Here we randomize the border the spawn will occur
        int spawnSide = Random.Range(1, 5);

        // Create the spawn point
        if (spawnSide == 1) {
            // Left
            spawnWidth = maxCameraWidth * -1;
            if (Settings.progress.nextWave == 2) {
                // This is the wave that movement is unlocked, so spawn the enemies to the side of the player so they can shoot the enemies without moving
                spawnHeight = 0f;
            } else {        
                // Normal spawn
                spawnHeight = Random.Range(maxCameraHeight * -1, maxCameraHeight);
            }
        }
        if (spawnSide == 2) {
            // Right
            spawnWidth = maxCameraWidth;
            if (Settings.progress.nextWave == 2) {
                // This is the wave that movement is unlocked, so spawn the enemies to the side of the player so they can shoot the enemies without moving
                spawnHeight = 0f;
            } else {
                // Normal spawn
                spawnHeight = Random.Range(maxCameraHeight * -1, maxCameraHeight);
            }
        } 
        if (spawnSide == 3) {
            // Top
            spawnHeight = maxCameraHeight;
            if (Settings.progress.nextWave == 2) {
                // This is the wave that movement is unlocked, so spawn the enemies to the side of the player so they can shoot the enemies without moving
                spawnWidth = 0f;
            } else {
                // Normal spawn
                spawnWidth = Random.Range(maxCameraHeight * -1, maxCameraHeight);
            }
        }
        if (spawnSide == 4) {
            // Bottom
            spawnHeight = maxCameraHeight * -1;
            if (Settings.progress.nextWave == 2) {
                // This is the wave that movement is unlocked, so spawn the enemies to the side of the player so they can shoot the enemies without moving
                spawnWidth = 0f;
            } else {
                // Normal spawn
                spawnWidth = Random.Range(maxCameraHeight * -1, maxCameraHeight);
            }
        } 

        // Check if we are on the first wave where we unlock shooting
        if (Settings.progress.nextWave == 1) {
            if (GameObject.Find("E_Shoot(Clone)1")) {
                // This is the spawn for the second shooter
                spawnHeight = (maxCameraHeight / 3);
                spawnWidth = maxCameraWidth;
            } else {
                // This is the spawn for the first shooter
                spawnHeight = -(maxCameraHeight / 3);
                spawnWidth = -maxCameraWidth;
            }
        }

        // Spawn unit
        transform.position = new Vector2(spawnWidth, spawnHeight);
    }

    static public void MoveTowardsPlayer(ref Transform transform, float movementSpeed)
    {
        GameObject player = GameObject.Find("Player");
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
    }

    static public bool BasicDamage(ref float health, ref GameObject enemy, bool increaseScore = true)
    {
        Game game = GameObject.Find("Game").GetComponent<Game>();
        Sound sound = GameObject.Find("Sound").GetComponent<Sound>();

        if (health == 1) {
            // If it's health is 1, then it will die now
            sound.PlayDestroy();
            UnityEngine.Object.Destroy(enemy); // Exactly the same as Destroy()

            // Increase the player's score by 1
            if (increaseScore) {
                game.score += 1;
                Settings.player.shootCooldown *= 0.9f;
            }
            return true;
        } else {
            // If its health is not 1, then it can take more hits
            sound.PlayHit();
            health = health - 1;
            return false;
        }
    }
}
