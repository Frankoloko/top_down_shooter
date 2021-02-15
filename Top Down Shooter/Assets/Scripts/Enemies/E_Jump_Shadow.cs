using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Jump_Shadow : MonoBehaviour
{
    void Update()
    {
        Transform temp = transform;
        E_BASE.MoveTowardsPlayer(ref temp, Settings.e_Jump.shadowMoveSpeed);
        transform.position = temp.position;
    }
}