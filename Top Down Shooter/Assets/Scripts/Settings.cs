using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    // Inspector arrangement

    [Header("General")]
    public Player player = new Player();
    [Header("Enemies")]
    public E_Divide e_Divide;
    public E_Brown e_Brown;
    public E_Green e_Green;
    [Header("Abilities")]
    public Clone clone;
    public Flash flash;

    // General classes

    [System.Serializable]
    public class Player
    {
        public float bulletSpeed = 7f;
        public float movementSpeed = 7f;
        public float shootCooldown = 7f;
    }

    // ENEMIES

    [System.Serializable]
    public class E_Divide
    {
        public float L_MovementSpeed = 7f;
        public float L_Health = 7f;
        public float M_MovementSpeed = 7f;
        public float M_Health = 7f;
        public float S_MovementSpeed = 7f;
        public float S_Health = 7f;
    }

    [System.Serializable]
    public class E_Brown
    {
        public float movementSpeed = 7f;
        public float health = 7f;
    }

    [System.Serializable]
    public class E_Green
    {
        public float movementSpeed = 7f;
        public float health = 7f;
    }

    // ABILITIES

    [System.Serializable]
    public class Clone
    {
        public float cooldown = 7f;
        public float duration = 10f;
    }

    [System.Serializable]
    public class Flash
    {
        public float cooldown = 7f;
        public float distance = 15f;
    }
}