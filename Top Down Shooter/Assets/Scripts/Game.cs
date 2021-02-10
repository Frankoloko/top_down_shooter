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
    public GameObject E_Divide_L;
    public GameObject E_Movement;
    public GameObject E_Green;
    public GameObject E_Shoot;
    public GameObject E_Teleport;

    [HideInInspector]
    public int score = 0;
    Text scoreLabel;
    public enum FightMode { NoEnemies, RandomEndless, Waves, E_Divide_L, E_Movement, E_Green, E_Shoot, E_Teleport }

    List<GameObject> AllEnemies;
    System.Random random = new System.Random();
    List<Wave> waves;

    void Start()
    {
        GameObject.Find("UnlockPopup").transform.localScale = new Vector3(0f, 0f, 0f);
        scoreLabel = GameObject.Find("Score").GetComponent<Text>();

        GameObject.Find("WaveNumber").GetComponent<Text>().text = Settings.progress.nextWave.ToString();

        // Turn the cursor off so that it isn't in the way
        Cursor.visible = false;
        
        // If no enemies, just stop the code
        if (fightMode == FightMode.NoEnemies) {
            return;
        }

        // Set up the all enemies list (THIS HAS TO HAPPEN BEFORE  THE ENDLESS() FUNCTION IS CALLED)
        AllEnemies = new List<GameObject>(){ E_Divide_L, E_Movement, E_Green, E_Shoot, E_Teleport };

        // If endless mode, run the endless mode only
        if (fightMode == FightMode.RandomEndless) {
            UnlockEverything();
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
            if (fightMode == FightMode.E_Movement) {
                selected_enemy = E_Movement;
            }
            if (fightMode == FightMode.E_Green) {
                selected_enemy = E_Green;
            }
            if (fightMode == FightMode.E_Shoot) {
                selected_enemy = E_Shoot;
            }
            if (fightMode == FightMode.E_Teleport) {
                selected_enemy = E_Teleport;
            }

            UnlockEverything();
            StartCoroutine(Endless(selected_enemy));
            return;
        }

        // If the code reaches here, then we are doing the "Waves" fight mode
        scoreLabel.enabled = false;

        // Setup the arrays of enemies
        SetupLists();

        // This gets the increments that enemies will spawn at
        float spawn_increment_seconds = waves[Settings.progress.nextWave].spawn_time_seconds / waves[Settings.progress.nextWave].spawn_order.Length;
        
        // Here we start StartCoroutine instances, all at the start time, but in the SpawnEnemy we wait X amount of time before creating the enemies
        float total = 0;
        foreach (GameObject item in waves[Settings.progress.nextWave].spawn_order) {
            StartCoroutine(SpawnEnemy(item, total));
            total += spawn_increment_seconds;
        }
    }

    void UnlockEverything()
    {
        // Unlock everything
        Settings.progress.e_Divide_FirstKill = true;
        Settings.progress.e_Movement_FirstKill = true;
        Settings.progress.e_Shoot_FirstKill = true;
        Settings.progress.e_Teleport_FirstKill = true;

        // Assign first abilities
        Settings.progress.q_Ability = "Divide";
        Settings.progress.e_Ability = "Teleport";

        // Hide/Unhide labels
        GameObject.Find("Score").GetComponent<Text>().enabled = true;
        GameObject.Find("Wave").GetComponent<Text>().enabled = false;
        GameObject.Find("WaveNumber").GetComponent<Text>().enabled = false;
    }

    void SetupLists()
    {
        // Set up the wave lists
        waves = new List<Wave>(){
            // Wave [0] is used for testing purposes. Do whatever you want with it
            new Wave(){
                spawn_time_seconds = 10,
                spawn_order = new GameObject[]{
                    E_Teleport
                },
            },
            new Wave(){
                spawn_time_seconds = 2,
                spawn_order = new GameObject[]{
                    E_Shoot, E_Shoot
                },
            },
            new Wave(){
                spawn_time_seconds = 30,
                spawn_order = new GameObject[]{
                    E_Movement, E_Movement, E_Movement, E_Movement
                },
            },
            new Wave(){
                spawn_time_seconds = 40,
                spawn_order = new GameObject[]{
                    E_Movement, E_Shoot, E_Movement, E_Shoot, E_Movement, E_Shoot, E_Movement, E_Shoot
                },
            },
            new Wave(){
                spawn_time_seconds = 60,
                spawn_order = new GameObject[]{
                    E_Movement, E_Shoot, E_Divide_L, E_Movement, E_Shoot, E_Divide_L, E_Movement, E_Shoot, E_Divide_L
                },
            },
            new Wave(){
                spawn_time_seconds = 80,
                spawn_order = new GameObject[]{
                    E_Movement, E_Shoot, E_Divide_L, E_Teleport, E_Movement, E_Shoot, E_Divide_L, E_Teleport, E_Movement, E_Shoot, E_Divide_L, E_Teleport
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
        // Unlock popup: Check if the unlock popup is open (when it is open it pauses the game)
        if (Time.timeScale == 0) {
            // if (Input.anyKey) { // This is not working because it triggers since the player is already pressing a direction to shoot/move
            if (Input.GetKeyDown(KeyCode.Return)) {
                GameObject.Find("UnlockPopup").transform.localScale = new Vector3(0f, 0f, 0f);
                Time.timeScale = 1;
            }
            return;
        }
        // R: Reset game
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Settings.ResetStatics();
        }
        // ESC: Quit game
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene("Menu");
        }
        // Update score label
        scoreLabel.text = score.ToString();
        // Check if wave is finished
        if (fightMode == FightMode.Waves) {
            if (score == waves[Settings.progress.nextWave].spawn_order.Length) {
                SceneManager.LoadScene("Menu");
                Settings.progress.nextWave += 1;
            }
        }
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
