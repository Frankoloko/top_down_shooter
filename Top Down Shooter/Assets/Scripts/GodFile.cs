using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave
{
    public int spawn_time_seconds;
    public GameObject[] spawn_order;
}

public class GodFile : MonoBehaviour
{
    public int NextWave = 5;
    public GameObject Enemy_Red;
    public GameObject Enemy_Brown;

    private List<Wave> waves;

    void Start()
    {
        // This HAS to happen first in this Start() method, because the functions below make use of it
        SetupWaves();

        // This gets the increments that enemies will spawn at
        float spawn_increment_seconds = waves[NextWave].spawn_time_seconds / waves[NextWave].spawn_order.Length;
        
        // Here we start StartCoroutine instances, all at the start time, but in the SpawnEnemy we wait X amount of time before creating the enemies
        float total = 0;
        foreach (GameObject item in waves[NextWave].spawn_order) {
            StartCoroutine(SpawnEnemy(item, total));
            total += spawn_increment_seconds;
        }
    }

    void SetupWaves()
    {
        waves = new List<Wave>(){
            new Wave(){
                spawn_time_seconds = 30,
                spawn_order = new GameObject[]{
                    Enemy_Red, Enemy_Red, Enemy_Red
                },
            },
            new Wave(){
                spawn_time_seconds = 60,
                spawn_order = new GameObject[]{
                    Enemy_Red, Enemy_Red, Enemy_Brown, Enemy_Red, Enemy_Brown, Enemy_Red, Enemy_Red,Enemy_Red, Enemy_Brown, Enemy_Brown
                },
            }
        };
    }

    IEnumerator SpawnEnemy(GameObject p_enemy, float p_spawn_increment_seconds)
    {
        // Wait an X amount of time before creating the enemy
        yield return new WaitForSeconds(p_spawn_increment_seconds);
        // Create new projectile
        Instantiate(p_enemy);
    }

}
