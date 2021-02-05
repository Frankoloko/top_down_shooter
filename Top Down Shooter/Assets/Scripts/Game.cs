using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public int score = 0;
    public Text scoreLabel;

    public GameObject E_Divide_L;
    public GameObject E_Brown;
    public GameObject E_Green;
    public List<GameObject> AllEnemies;

    // static Random random = new Random();
    public System.Random random = new System.Random();

    private List<Wave> waves;

    void Start()
    {
        // Turn the cursor off so that it isn't in the way
        Cursor.visible = false;
        
        // If no enemies, just stop the code
        if (NoEnemies) {
            return;
        }

        // Set up the all enemies list
        AllEnemies = new List<GameObject>(){ E_Divide_L, E_Brown, E_Green };

        // If endless mode, run the endless mode only
        if (EndlessMode) {
            StartCoroutine(Endless());
            return;
        }

        // Setup the arrays of enemies
        SetupLists();

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
        // Set up the wave lists
        waves = new List<Wave>(){
            new Wave(){
                spawn_time_seconds = 30,
                spawn_order = new GameObject[]{
                    E_Divide_L, E_Divide_L, E_Divide_L
                },
            },
            new Wave(){
                spawn_time_seconds = 60,
                spawn_order = new GameObject[]{
                    E_Divide_L, E_Divide_L, E_Brown, E_Divide_L, E_Brown, E_Divide_L, E_Divide_L,E_Divide_L, E_Brown, E_Brown
                },
            }
        };
    }

    IEnumerator SpawnEnemy(GameObject p_enemy, float p_spawn_increment_seconds)
    {
        // Wait an X amount of time before creating the enemy
        yield return new WaitForSeconds(p_spawn_increment_seconds);
        // Create new enemy
        GameObject enemy = Instantiate(p_enemy);
        enemy.name = enemy.name + p_spawn_increment_seconds;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene("Menu");
        }

        scoreLabel.text = score.ToString();
    }

    IEnumerator Endless()
    {
        // In endless mode we start by creating an enemy every 4 seconds
        // With every enemy created we decrease the create time

        float spawn_increment_seconds = 4;
        while (spawn_increment_seconds > 0)
        {
            // Create a random enemy
            GameObject randomEnemy = AllEnemies[random.Next(AllEnemies.Count)];
            GameObject enemy = Instantiate(randomEnemy);
            enemy.name = enemy.name + spawn_increment_seconds;

            // Wait the code
            WaitForSeconds wait = new WaitForSeconds(spawn_increment_seconds);

            // Decrease the spawn time
            spawn_increment_seconds -= 0.10f;
            yield return wait ;
        }
    }

}
