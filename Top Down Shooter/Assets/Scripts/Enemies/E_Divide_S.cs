using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Divide_S : MonoBehaviour, E_BaseInterface
{
    Game game;
    float health;

    void Start()
    {
        game = GameObject.Find("Game").GetComponent<Game>();

        health = BalancingSettings.e_Divide.S_Health;
    }

    void Update()
    {
        Transform temp = transform;
        E_BASE.MoveTowardsPlayer(ref temp, BalancingSettings.e_Divide.S_MovementSpeed);
        transform.position = temp.position;
    }

    public void GotHit()
    {
        GameObject temp = gameObject;
        bool dead = E_BASE.BasicDamage(ref health, ref temp, false);

        if (dead) {
            // This makes sure that 8 of these units are killed before it counts as a point
            BalancingSettings.e_Divide.killCounter -= 1;
            if (BalancingSettings.e_Divide.killCounter == 0) {
                BalancingSettings.e_Divide.killCounter = 8;
                game.score += 1;
                BalancingSettings.player.shootCooldown *= 0.9f;

                if (!BalancingSettings.e_Divide.firstKill) {
                    // First kill, unlock the unit
                    BalancingSettings.e_Divide.firstKill = true;
                    GameObject.Find("UnlockPopup").transform.localScale = new Vector3(1f, 1f, 1f);
                    print(GameObject.Find("UnlockImage").GetComponent<UnityEngine.UI.Image>().sprite);
                    Time.timeScale = 0;
                }
            }
        }
    }
}