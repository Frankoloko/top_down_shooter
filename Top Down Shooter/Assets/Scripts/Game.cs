using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wave
{
    public int spawn_time_seconds;
    public GameObject[] spawn_order;
}

public class Game : MonoBehaviour
{
    public bool NoEnemies = false;
    public bool EndlessMode = false;
    public int NextWave = 5;
    public GameObject Enemy_Red;
    public GameObject Enemy_Brown;
    public GameObject Enemy_Green;
    public List<GameObject> AllEnemies;

    // static Random random = new Random();
    public System.Random random = new System.Random();

    private List<Wave> waves;

    void Start()
    {
        if (NoEnemies) {
            return;
        }

        SetupLists();

        if (EndlessMode) {
            StartCoroutine(Endless());
            return;
        }

        // This gets the increments that enemies will spawn at
        float spawn_increment_seconds = waves[NextWave].spawn_time_seconds / waves[NextWave].spawn_order.Length;
        
        // Here we start StartCoroutine instances, all at the start time, but in the SpawnEnemy we wait X amount of time before creating the enemies
        float total = 0;
        foreach (GameObject item in waves[NextWave].spawn_order) {
            StartCoroutine(SpawnEnemy(item, total));
            total += spawn_increment_seconds;
        }
    }

    void SetupLists()
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

        AllEnemies = new List<GameObject>(){ Enemy_Red, Enemy_Brown, Enemy_Green };
    }

    IEnumerator SpawnEnemy(GameObject p_enemy, float p_spawn_increment_seconds)
    {
        // Wait an X amount of time before creating the enemy
        yield return new WaitForSeconds(p_spawn_increment_seconds);
        // Create new projectile
        print("enemy created");
        GameObject enemy = Instantiate(p_enemy);
        enemy.name = enemy.name + p_spawn_increment_seconds;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    IEnumerator Endless()
    {
        float spawn_increment_seconds = 4;
        
        while (spawn_increment_seconds > 0)
        {
            // Create a random enemy
            GameObject randomEnemy = AllEnemies[random.Next(AllEnemies.Count)];
            GameObject enemy = Instantiate(randomEnemy);
            enemy.name = enemy.name + spawn_increment_seconds;

            // Wait the code
            WaitForSeconds wait = new WaitForSeconds( spawn_increment_seconds ) ;
            spawn_increment_seconds -= 0.10f;
            yield return wait ;
        }
    }

}
