using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Movement : MonoBehaviour, E_BaseInterface
{
    float health;

    void Start()
    {
        health = BalancingSettings.e_Movement.health;

        Transform temp = transform;
        E_BASE.SpawnOutsideCamera(ref temp);
        transform.position = temp.position;
    }

    void Update()
    {
        Transform temp = transform;
        E_BASE.MoveTowardsPlayer(ref temp, BalancingSettings.e_Movement.movementSpeed);
        transform.position = temp.position;
    }

    public void GotHit()
    {
        GameObject temp = gameObject;
        bool dead = E_BASE.BasicDamage(ref health, ref temp);

        if (dead) {
            if (!BalancingSettings.e_Movement.firstKill) {
                // First kill, unlock the unit
                print("First Kill!");
                BalancingSettings.e_Movement.firstKill = true;
                GameObject.Find("UnlockPopup").transform.localScale = new Vector3(1f, 1f, 1f);
                // Sprite sprite = Resources.Load<Sprite>("Graphics/Enemies/E_Movement");
                // Sprite sprite = Resources.Load<Sprite>("Assets/Graphics/Enemies/E_Movement");
                Sprite sprite = Resources.Load<Sprite>("E_Movement");
                GameObject.Find("UnlockImage").GetComponent<UnityEngine.UI.Image>().sprite = sprite;
                Time.timeScale = 0;
            }
        }
    }
}