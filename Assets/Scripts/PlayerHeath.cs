using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerHeath : MonoBehaviour
{
    public bool isDead = false;
    public int health = 3;
    public Text gameOverText;
    public Text scoreText;

    private bool invincible = false;
    private float timer;

    void Update()
    {
        if (isDead)
        {
            GameObject.Find("Scoreboard").SetActive(true);
            if (Input.GetKeyDown(KeyCode.R))
            {
                GameObject.Find("Scoreboard").SetActive(false);
                isDead = false;
                Application.LoadLevel(Application.loadedLevel);
            }
        }
        else
        {
            timer += Time.deltaTime;
            scoreText.text = "" + Math.Round(timer, 2);
        }
    }

    private void Awake()
    {
        HealthManager.health = health;
        timer = 0.0f;
    }

    IEnumerator OnTriggerEnter(Collider collision)
    {
        bool isEnemy =
            !collision.gameObject.name.ToLower().Contains("road")
            &&
            !collision.gameObject.name.ToLower().Contains("boundary");
        if (!invincible & !isDead & isEnemy)
        {
            health -= 1;
            HealthManager.health = health;
            if (health > 0)
            {
                invincible = true;
                yield return new WaitForSeconds(2);
                invincible = false;
            }
        }
        HealthManager.health = health;
        if (health == 0)
        {
            isDead = true;
            gameOverText.text = "GAME OVER! \n Press R to restart.";
        }
    }
}
