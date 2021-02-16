using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Jump : MonoBehaviour, E_BaseInterface
{
    float health;
    bool bendDown = false;
    bool jumpUp = false;
    bool jumpDown = false;
    float jumpSpeed = 1.5f;
    public GameObject shadowPrefab;
    GameObject shadow;
    Vector3 originalScale;

    void Start()
    {
        health = Settings.e_Jump.health;

        Transform temp = transform;
        E_BASE.SpawnOutsideCamera(ref temp);
        transform.position = temp.position;

        InvokeRepeating("RepeatingPattern", 0f, 10f);
    }

    void RepeatingPattern()
    {
        // To do all this waiting, we need to start it in a Coroutine
        StartCoroutine(WaitCode());
    }

    IEnumerator WaitCode()
    {
        // Bend down
        bendDown = true;
        originalScale = gameObject.transform.localScale;
        yield return new WaitForSeconds(Settings.e_Jump.bendDownTime);
        bendDown = false;

        // Spawn shadow
        shadow = Instantiate(shadowPrefab, transform.position, Quaternion.identity);
        shadow.GetComponent<E_Jump_Shadow>().jumper = gameObject;

        // Jump Up
        jumpUp = true;
        transform.gameObject.tag = "Untagged";
        yield return new WaitForSeconds(1f);
        jumpUp = false;

        // Move to player (the shadow hold its own code to move towards they player)
        yield return new WaitForSeconds(Settings.e_Jump.shadowMoveTime);

        // Slam down
        jumpDown = true;
    }

    void Update()
    {
        // Scale the unit down
        if (bendDown) {
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y * 0.9975f, gameObject.transform.localScale.z);
        }

        // Move the y position upwards
        if (jumpUp) {
            transform.position = new Vector2(transform.position.x, transform.position.y + jumpSpeed);
        }

        // Move the y position downwards until it
        if (jumpDown) {
            if (transform.position.y - (jumpSpeed / 2) - (originalScale.y / 10) > shadow.transform.position.y) {
                transform.position = new Vector2(shadow.transform.position.x, transform.position.y - (jumpSpeed / 2));
            } else {
                jumpDown = false;
                gameObject.transform.localScale = originalScale;
                transform.gameObject.tag = "Enemy";

                // Loop through all the objects inside of the shadow and destory each of them
                List<GameObject> objectsInside = shadow.GetComponent<E_Jump_Shadow>().objectsInside;
                for (var i = 0; i < objectsInside.Count; i++) {
                    // Console.WriteLine("Amount is {0} and type is {1}", myMoney[i].amount, myMoney[i].type);
                    if (objectsInside[i].name != "Player") {
                        Destroy(objectsInside[i]);
                    }
                }

                // Make sure to do this last (after looping through the objects inside)
                Destroy(shadow);
            }
        }
    }

    public void GotHit()
    {
        GameObject temp = gameObject;
        E_BASE.BasicDamage(ref health, ref temp);
    }
}