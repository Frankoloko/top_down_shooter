using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface E_BaseInterface
{
    void GotHit();
}

public class E_BASE
{
    static Game game;
    static Settings settings;
    static Sound sound;

    void Start()
    {
        settings = GameObject.Find("Settings").GetComponent<Settings>();
        game = GameObject.Find("Game").GetComponent<Game>();
        sound = GameObject.Find("Sound").GetComponent<Sound>();
    }
    
    static public void SpawnOutsideCamera(Transform transform)
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
            spawnHeight = Random.Range(maxCameraHeight * -1, maxCameraHeight);
            spawnWidth = maxCameraWidth * -1;
        }
        if (spawnSide == 2) {
            // Right
            spawnHeight = Random.Range(maxCameraHeight * -1, maxCameraHeight);
            spawnWidth = maxCameraWidth;
        } 
        if (spawnSide == 3) {
            // Top
            spawnHeight = maxCameraHeight;
            spawnWidth = Random.Range(maxCameraWidth * -1, maxCameraWidth);
        }
        if (spawnSide == 4) {
            // Bottom
            spawnHeight = maxCameraHeight * -1;
            spawnWidth = Random.Range(maxCameraWidth * -1, maxCameraWidth);
        } 

        // Transform to outside of camera view
        transform.position = new Vector2(spawnWidth, spawnHeight);
    }

    static public void MoveTowardsPlayer(Transform transform, float movementSpeed)
    {
        GameObject player = GameObject.Find("Player");
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
    }

    static public void BasicDamage(float health, GameObject enemy)
    {
        if (health == 1) {
            // If it's health is 1, then it will die now
            sound.PlayDestroy();
            UnityEngine.Object.Destroy(enemy); // Exactly the same as Destroy()

            // Increase the player's score by 1
            game.score += 1;
            settings.player.shootCooldown *= 0.9f;
        } else {
            // If its health is not 1, then it can take more hits
            sound.PlayHit();
            health = health - 1;
        }
    }
}
