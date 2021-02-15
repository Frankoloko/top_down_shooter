using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings
{
    // General
    static public Progress progress = new Progress();
    static public Player player = new Player();
    // Enemies
    static public E_Divide e_Divide = new E_Divide();
    static public E_Movement e_Movement = new E_Movement();
    static public E_Jump e_Jump = new E_Jump();
    static public E_Shoot e_Shoot = new E_Shoot();
    static public E_Teleport e_Teleport = new E_Teleport();
    // Abilities
    static public Clone clone = new Clone();
    static public Teleport teleport = new Teleport();

    static public void ResetStatics()
    {
        // progress = new Progress(); // NEVER USE THIS: Progress is meant to stay saved over each new game start
        player = new Player();
        e_Divide = new E_Divide();
        e_Movement = new E_Movement();
        e_Shoot = new E_Shoot();
        e_Teleport = new E_Teleport();
        e_Jump = new E_Jump();
        clone = new Clone();
        teleport = new Teleport();
    }

    // GENERAL

    [System.Serializable]
    public class Progress
    {
        // This class is for values that need to be kept over multiple levels/waves
        public bool e_Divide_FirstKill = false;
        public bool e_Movement_FirstKill = false;
        public bool e_Shoot_FirstKill = false;
        public bool e_Teleport_FirstKill = false;
        public bool e_Jump_FirstKill = false;

        public string q_Ability = null;
        public string e_Ability = null;
        public Sprite q_Sprite = null;
        public Sprite e_Sprite = null;

        public int nextWave = 1;
    }

    [System.Serializable]
    public class Player
    {
        public float bulletSpeed = 15f;
        public float movementSpeed = 8f;
        public float shootCooldown = 1f;
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
        public int killCounter = 8;
    }

    [System.Serializable]
    public class E_Movement
    {
        public float movementSpeed = 4f;
        public float health = 3f;
    }

    [System.Serializable]
    public class E_Jump
    {
        public float health = 10f;
        public float shadowMoveSpeed = 6f;
        public float shadowMoveTime = 2f;
        public float bendDownTime = 2f;
    }

    [System.Serializable]
    public class E_Shoot
    {
        public float movementSpeed = 3f;
        public float health = 2f;
        public float bulletSpeed = 15f;
        public float moveTime = 5f;
        public float pauseBeforeShoot = 0.5f;
        public float pauseAfterShoot = 1f;
    }

    [System.Serializable]
    public class E_Teleport
    {
        public float movementSpeed = 3f;
        public float health = 2f;
        public float bulletSpeed = 15f;
        public float pauseBeforeShoot = 3f;
        public float pauseAfterShoot = 1f;
        public float bulletSize = 25f;
    }

    // ABILITIES

    [System.Serializable]
    public class Clone
    {
        public float cooldown = 7f;
        public float duration = 10f;
    }

    [System.Serializable]
    public class Teleport
    {
        public float cooldown = 7f;
        public float distance = 10f;
    }
}