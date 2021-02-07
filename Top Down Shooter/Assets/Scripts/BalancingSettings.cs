using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalancingSettings
{
    // Inspector arrangement

    // NOTE: Whatever you add here, you also need to add in the restart of the scene (Menu.cs)
    [Header("General")]
    static public Player player = new Player();
    [Header("Enemies")]
    static public E_Divide e_Divide = new E_Divide();
    static public E_Brown e_Brown = new E_Brown();
    static public E_Green e_Green = new E_Green();
    static public E_Shoot e_Shoot = new E_Shoot();
    [Header("Abilities")]
    static public Clone clone = new Clone();
    static public Flash flash = new Flash();

    static public void ResetStatics()
    {
        player = new Player();
        e_Divide = new E_Divide();
        e_Brown = new E_Brown();
        e_Green = new E_Green();
        e_Shoot = new E_Shoot();
        clone = new Clone();
        flash = new Flash();
    }

    // General classes

    [System.Serializable]
    public class Player
    {
        public float bulletSpeed = 15f;
        public float movementSpeed = 8f;
        public float shootCooldown = 1f;
        public bool unlockedMovement = false;
        public bool unlockedShooting = false;
    }

    // ENEMIES

    [System.Serializable]
    public class E_Divide
    {
        public float L_MovementSpeed = 3f;
        public float L_Health = 4f;
        public float M_MovementSpeed = 5f;
        public float M_Health = 2f;
        public float S_MovementSpeed = 7f;
        public float S_Health = 1f;
    }

    [System.Serializable]
    public class E_Brown
    {
        public float movementSpeed = 5f;
        public float health = 3f;
    }

    [System.Serializable]
    public class E_Green
    {
        public float movementSpeed = 2f;
        public float health = 15f;
    }

    [System.Serializable]
    public class E_Shoot
    {
        public float movementSpeed = 2f;
        public float health = 2f;
        public float bulletSpeed = 15f;
        public float shotDelay = 3f;
    }

    // ABILITIES

    [System.Serializable]
    public class Clone
    {
        public bool unlocked = false;
        public float cooldown = 7f;
        public float duration = 10f;
    }

    [System.Serializable]
    public class Flash
    {
        public bool unlocked = false;
        public float cooldown = 7f;
        public float distance = 10f;
    }
}