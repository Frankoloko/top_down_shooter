using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRed : MonoBehaviour
{
    public float health = 5f;

    void Start()
    {
        // Here we randomize the border the spawn will occur
        int spawnSide = Random.Range(1, 5);

        // Get the camera's borders
        float maxCameraHeight = Camera.main.orthographicSize + 2;
        float maxCameraWidth = Camera.main.orthographicSize * Camera.main.aspect + 2;

        // Create the spawn points (either outside the top border, or outside the side border)
        float spawnHeight = 0f;
        float spawnWidth = 0f;

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

    void MoveEnemy()
    {
        GameObject player = GameObject.Find("Player");

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 5f * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
    }
}
