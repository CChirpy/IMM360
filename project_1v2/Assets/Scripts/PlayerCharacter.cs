using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Tracks points and current player health.

public class PlayerCharacter : MonoBehaviour
{
    public int health;
    public static int points;

    void Start()
    {
        // Initialize health value. 
        health = 3;
        points = 0;
    }

    public void Hurt(int damage)
    {
        // Decrement the player's health.
        health -= damage;               	
        Debug.Log("Health: " + health);
    }

    void Update()
    {
        // Restart scene on death.
        if (health <= 0)
        {
			Debug.Log("GAME OVER! \n" + "TOTAL POINTS: " + points);
            SceneManager.LoadScene("Scene");
        }
    }
}