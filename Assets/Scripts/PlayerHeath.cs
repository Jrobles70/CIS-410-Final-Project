using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeath : MonoBehaviour
{
    public bool isDead = false;
    public int health = 3;
    public Text gameOverText;

    private bool invincible = false;

    void Update()
    {
        if (isDead)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                isDead = false;
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

    private void Awake()
    {
        HealthManager.health = health;
    }

    IEnumerator OnTriggerEnter(Collider collision)
    {
        if (!invincible & !isDead)
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
