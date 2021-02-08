using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class E_Movement : MonoBehaviour, E_BaseInterface
{
    float health;

    void Start()
    {
        health = Settings.e_Movement.health;

        Transform temp = transform;
        E_BASE.SpawnOutsideCamera(ref temp);
        transform.position = temp.position;
    }

    void Update()
    {
        Transform temp = transform;
        E_BASE.MoveTowardsPlayer(ref temp, Settings.e_Movement.movementSpeed);
        transform.position = temp.position;
    }

    public void GotHit()
    {
        GameObject temp = gameObject;
        bool dead = E_BASE.BasicDamage(ref health, ref temp);

        if (dead) {
            if (!Settings.progress.e_Movement_FirstKill) {
                // First kill, unlock the unit
                Settings.progress.e_Movement_FirstKill = true;
                GameObject.Find("UnlockPopup").transform.localScale = new Vector3(1f, 1f, 1f);
                Sprite sprite = Resources.Load<Sprite>("Enemies/E_Movement");
                GameObject.Find("UnlockImage").GetComponent<Image>().sprite = sprite;
                Time.timeScale = 0;
            }
        }
    }
}