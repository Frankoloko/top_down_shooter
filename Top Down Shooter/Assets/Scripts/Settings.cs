using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    // Inspector arrangement

    [Header("General")]
    public Player player = new Player();
    public Enemies enemies;
    [Header("Abilities")]
    public Flash flash;

    // General classes

    [System.Serializable]
    public class Player
    {
        public float bulletSpeed = 7f;
        public float movementSpeed = 7f;
        public float shootCooldown = 7f;
    }

    [System.Serializable]
    public class Enemies
    {
        public float movementSpeed = 7f;
    }

    // Ability classes, arrange the classes alphabetically

    [System.Serializable]
    public class Flash
    {
        public float cooldown = 7f;
        public float distance = 7f;
    }
}

