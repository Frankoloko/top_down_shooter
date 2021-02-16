using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Jump_Shadow : MonoBehaviour
{
    public GameObject jumper;
    public List<GameObject> objectsInside = new List<GameObject>();

    void Update()
    {
        Transform temp = transform;
        E_BASE.MoveTowardsPlayer(ref temp, Settings.e_Jump.shadowMoveSpeed);
        transform.position = temp.position;
    }

    void OnTriggerEnter2D (Collider2D collider) {
        if (collider.gameObject.name != jumper.name) {
            objectsInside.Add(collider.gameObject);
        }
    }

    void OnTriggerExit2D (Collider2D collider) {
        objectsInside.Remove(collider.gameObject);
    }
}