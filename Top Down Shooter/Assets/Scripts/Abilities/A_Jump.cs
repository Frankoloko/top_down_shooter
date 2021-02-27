using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_Jump
{
    static bool inTheAir = false;
    static float jumpSpeed = 1.5f;

    static public GameObject shadowPrefab;
    static GameObject shadow;
    static Vector3 originalScale;
    static bool jumping = false;

    static public void Update_Jump()
    {
        Transform playerTransform = GameObject.Find("Player").transform;

        // If not jumping already, start the jumping (stop the player from moving & save the originalScale)
        if (!jumping) {
            jumping = true;
            originalScale = playerTransform.localScale;
            Settings.player.stopMovement = true;
        }

        // Prepare to jump up, scale the unit down
        if (!inTheAir) {
            playerTransform.localScale = new Vector3(playerTransform.localScale.x, playerTransform.localScale.y * 0.9975f, playerTransform.localScale.z);
        }

        // Check if the player is small enough to jump up
        if (playerTransform.localScale.y < originalScale.y * 0.40f) {
            Debug.Log("Jump up!");
            inTheAir = true;
        }

        // Jump up
        // if (inTheAir) {
        //     shadow = Instantiate(GameObject.Find("E_Jump_Shadow"), playerTransform.position, Quaternion.identity);
        //     // shadow.GetComponent<E_Jump_Shadow>().jumper = gameObject;
        // }

        // Stay floating till the player lets go of the button

        // Smash down
    }

    static public void StopJump()
    {
        Transform playerTransform = GameObject.Find("Player").transform;
        
        // Let the player move again
        Settings.player.stopMovement = false;

        // Return to original scale
        playerTransform.localScale = originalScale;

        // Stop the jumping
        jumping = false;
    }
}