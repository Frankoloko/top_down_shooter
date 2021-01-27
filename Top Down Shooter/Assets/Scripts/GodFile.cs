using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave
{
    public int spawn_time_seconds;
    public string[] spawn_order;
}

public class GodFile : MonoBehaviour
{
    public int NextWave = 5;
    public GameObject Enemy;
    List<Wave> waves = new List<Wave>(){
        new Wave(){
            spawn_time_seconds = 30,
            spawn_order = new string[]{
                "1_Melee", "2_Melee", "3_Melee"
            },
        },
        new Wave(){
            spawn_time_seconds = 60,
            spawn_order = new string[]{
                "1_Melee", "2_Melee", "3_Melee", "4_Melee", "5_Melee", "6_Melee"
            },
        }
    };

    void Start()
    {
        print(waves[NextWave].spawn_time_seconds);
        print(string.Join(", ", waves[NextWave].spawn_order));
        print("GAME GO!!!");

        // This gets the increments that enemies will spawn at
        float spawn_increment_seconds = waves[NextWave].spawn_time_seconds / waves[NextWave].spawn_order.Length;
        
        // Here we start StartCoroutine instances, all at the start time, but in the SpawnEnemy we wait X amount of time before creating the enemies
        float total = 0;
        foreach (string item in waves[NextWave].spawn_order) {
            StartCoroutine(SpawnEnemy(item, total));
            total += spawn_increment_seconds;
        }
    }

    IEnumerator SpawnEnemy(string enemy, float spawn_increment_seconds) {
        yield return new WaitForSeconds(spawn_increment_seconds);
        print(enemy);
        print(spawn_increment_seconds);

        // Create new projectile
        Instantiate(Enemy);
    }

}
