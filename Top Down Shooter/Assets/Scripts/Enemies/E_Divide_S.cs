using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class E_Divide_S : MonoBehaviour, E_BaseInterface
{
    Game game;
    float health;

    void Start()
    {
        game = GameObject.Find("Game").GetComponent<Game>();

        health = Settings.e_Divide.S_Health;
    }

    void Update()
    {
        Transform temp = transform;
        E_BASE.MoveTowardsPlayer(ref temp, Settings.e_Divide.S_MovementSpeed);
        transform.position = temp.position;
    }

    public void GotHit()
    {
        GameObject temp = gameObject;
        bool dead = E_BASE.BasicDamage(ref health, ref temp, false);

        if (dead) {
            // This makes sure that 8 of these units are killed before it counts as a point
            Settings.e_Divide.killCounter -= 1;
            if (Settings.e_Divide.killCounter == 0) {
                Settings.e_Divide.killCounter = 8;
                game.score += 1;
                Settings.player.shootCooldown *= 0.9f;

                if (!Settings.progress.e_Divide_FirstKill) {
                    // First kill, unlock the unit
                    Settings.progress.e_Divide_FirstKill = true;
                    GameObject.Find("UnlockPopup").transform.localScale = new Vector3(1f, 1f, 1f);
                    Sprite sprite = Resources.Load<Sprite>("Enemies/E_Divide_L");
                    GameObject.Find("UnlockImage").GetComponent<Image>().sprite = sprite;
                    Time.timeScale = 0;
                }
            }
        }
    }
}