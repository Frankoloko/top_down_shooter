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
    public FightMode fightMode;
    public int NextWave = 5;
    public GameObject E_Divide_L;
    public GameObject E_Brown;
    public GameObject E_Green;
    public GameObject E_Shoot;

    [HideInInspector]
    public int score = 0;
    Text scoreLabel;
    public enum FightMode { NoEnemies, RandomEndless, Waves, E_Divide_L, E_Brown, E_Green, E_Shoot }

    List<GameObject> AllEnemies;
    System.Random random = new System.Random();
    List<Wave> waves;

    void Start()
    {
        GetObjects();

        // Turn the cursor off so that it isn't in the way
        Cursor.visible = false;
        
        // If no enemies, just stop the code
        if (fightMode == FightMode.NoEnemies) {
            return;
        }

        // Set up the all enemies list
        AllEnemies = new List<GameObject>(){ E_Divide_L, E_Brown, E_Green, E_Shoot };

        // If endless mode, run the endless mode only
        if (fightMode == FightMode.RandomEndless) {
            StartCoroutine(Endless());
            return;
        }

        // If not waves mode either, then an enemy was selected, so spawn that enemy endlessly
        if (fightMode != FightMode.Waves) {
            GameObject selected_enemy = null;

            // Assign the selected enemy
            if (fightMode == FightMode.E_Divide_L) {
                selected_enemy = E_Divide_L;
            }
            if (fightMode == FightMode.E_Brown) {
                selected_enemy = E_Brown;
            }
            if (fightMode == FightMode.E_Green) {
                selected_enemy = E_Green;
            }
            if (fightMode == FightMode.E_Shoot) {
                selected_enemy = E_Shoot;
            }

            StartCoroutine(Endless(selected_enemy));
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

    void GetObjects()
    {
        scoreLabel = GameObject.Find("Score").GetComponent<Text>();
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
            BalancingSettings.ResetStatics();
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene("Menu");
        }

        
        scoreLabel.text = score.ToString();
    }

    IEnumerator Endless(GameObject selected_enemy = null)
    {
        // In endless mode we start by creating an enemy every 4 seconds
        // With every enemy created we decrease the create time

        float spawn_increment_seconds = 4;
        while (spawn_increment_seconds > 0)
        {
            GameObject createEnemy;
            if (selected_enemy) {
                // Enemy selected, use it
                createEnemy = selected_enemy;
            } else {
                // No enemy selected, create a random enemy
                createEnemy = AllEnemies[random.Next(AllEnemies.Count)];
            }

            // Create the enemy and give it a unique name
            GameObject enemy = Instantiate(createEnemy);
            enemy.name = enemy.name + spawn_increment_seconds;

            // Wait the code
            WaitForSeconds wait = new WaitForSeconds(spawn_increment_seconds);

            // Decrease the spawn time
            spawn_increment_seconds -= 0.10f;
            yield return wait ;
        }
    }

}
