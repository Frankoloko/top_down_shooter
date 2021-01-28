using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRed : MonoBehaviour
{
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
