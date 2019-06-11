using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpIndex = 0;
    private int dodged_cars = 0;
    private bool left = false;
    private bool right = false;
    private bool forward = false;
    private bool spawn_car = false;
    private bool power_up_spawn = false;
    private bool power_down_spawn = false;
    private bool player_pick_up = false;
    private bool player_pick_up_2 = false;

    private GameObject tut_car;
    private GameObject tut_powerUp;
    private GameObject tut_powerDown;
    private float pre_speed_up_score = 0.0f;

    public GameObject[] cars;
    public GameObject gameover_panel;
    public GameObject PauseMenu;
    public float score_multiplier = 1.0f;
    public Text score_text;
    public Text high_score_text;
    public Text powerUpText;
    public Text powerUpType;
    public float[] spawn_pos;
    public float spawn_wait;
    public float z_pos;
    public float y_pos = 0.0f;
    public float x_min, x_max;
    public float powerUpTime = 5.0f;
    public float speedFactor = 1.0f;
    public bool isPaused = false;
    public bool shield = false;
    public bool invert = false;
    public bool tutorial = false;

    private float score = 0.0f;
    private float t;
    private bool gameover = false;
    private bool force = false;

    // power up spawner
    public float xMin = -23.0f;
    public float xMax = 23.0f;
    public GameObject[] powerUps;
    public float waitTime = 10.0f;

    public bool isGameOver()
    {
        return gameover;
    }

    private IEnumerator spawn()
    {

        while (!gameover && force)
        {
            GameObject car;
            int index = (int)Random.Range(0.0f, 4.0f);
            car = cars[Random.Range(0, cars.Length)];
            Vector3 position = new Vector3(spawn_pos[index], y_pos, z_pos);
            Instantiate(car, position, transform.rotation);
            yield return new WaitForSeconds(spawn_wait);
            if (spawn_wait > 0.5)
            {
                spawn_wait -= 0.005f;
                speedFactor += 0.005f;
            }
        }
    }

    public void Pause()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            isPaused = true;
            PauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            isPaused = false;
            PauseMenu.SetActive(false);
        }
    }

    public void Timer(string powerUp)
    {
        powerUpType.text = powerUp;
        powerUpText.text = System.Math.Round(t, 0).ToString("0");
        if (t > 0.0f)
        {
            t -= Time.deltaTime;
            powerUpText.text = System.Math.Round(t, 0).ToString("0");
        }
        else
        {
            t = powerUpTime;
            if (powerUp == "Invert")
            {
                invert = false;
            }
            else if (powerUp == "Shield")
            {
                shield = false;
            }
        }
    }

    void Update()
    {
        if (popUpIndex == 0)
        {
            // Movement tutorial
            popUps[popUpIndex].SetActive(true);
            if (!left && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey("a")))
            {
                left = true;
                popUps[popUpIndex].transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
            }
            if (!right && (Input.GetKey(KeyCode.RightArrow) || Input.GetKey("d")))
            {
                right = true;
                popUps[popUpIndex].transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            }
            if (right && left)
            {
                popUps[popUpIndex].SetActive(false);
                popUpIndex++;
                popUps[popUpIndex].SetActive(true);
                pre_speed_up_score = score;
            }
        }
        if (popUpIndex == 1)
        {
            // Speed up tutorial
            if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey("w")))
            {
                popUps[popUpIndex].transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                if (score > pre_speed_up_score + 20.0f)
                {
                    popUps[popUpIndex].SetActive(false);
                    popUpIndex++;
                    popUps[popUpIndex].SetActive(true);
                }
            }
        }
        if (popUpIndex == 2)
        {
            // Dodge tutorial
            if (!spawn_car)
            {
                GameObject car;
                int index = (int)Random.Range(0.0f, 4.0f);
                car = cars[Random.Range(0, cars.Length)];
                Vector3 position = new Vector3(spawn_pos[index], y_pos, z_pos);
                tut_car = Instantiate(car, position, transform.rotation);
                spawn_car = true;
            } else if (tut_car == null)
            {
                dodged_cars++;
                if (dodged_cars > 7)
                {
                    popUps[popUpIndex].SetActive(false);
                    popUpIndex++;
                    popUps[popUpIndex].SetActive(true);
                }
                else
                {
                    spawn_car = false;
                }
            }
        }
        if (popUpIndex == 3)
        {
            // power up tutorial
            if (!power_up_spawn && !player_pick_up)
            {
                GameObject powerUp = powerUps[0];
                float x = Random.Range(xMin, xMax);
                Vector3 position = new Vector3(x, 0.0f, 1000.0f);
                tut_powerUp = Instantiate(powerUp, position, transform.rotation);
                power_up_spawn = true;
            }
            else if (powerUpText.text != "")
            {
                player_pick_up = true;
            }
            else if (player_pick_up && powerUpText.text == "")
            {
                popUps[popUpIndex].SetActive(false);
                popUpIndex++;
                popUps[popUpIndex].SetActive(true);
            } else if (power_up_spawn && tut_powerUp == null && !player_pick_up && !shield)
            {
                power_up_spawn = false;

            }
        }
        if (popUpIndex == 4)
        {
            // power down tutorial
            if (!power_down_spawn && !player_pick_up_2)
            {
                GameObject powerUp = powerUps[1];
                float x = Random.Range(xMin, xMax);
                Vector3 position = new Vector3(x, 0.0f, 1000.0f);
                tut_powerDown = Instantiate(powerUp, position, transform.rotation);
                power_down_spawn = true;
            }
            else if (powerUpText.text != "")
            {
                player_pick_up_2 = true;
            }
            else if (player_pick_up_2 && powerUpText.text == "")
            {
                popUps[popUpIndex].SetActive(false);
                popUpIndex++;
                popUps[popUpIndex].SetActive(true);
            } else if (power_down_spawn && tut_powerDown == null && !player_pick_up_2 && !invert)
            {
                power_down_spawn = false;

            }
        }

        if (popUpIndex == 5)
        {
            StartCoroutine(WaitToLoad());
        }

            if (!gameover)
        {
            if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey("w")))
                score += Time.deltaTime * score_multiplier;
            else
                score += Time.deltaTime;

            score_text.text = System.Math.Round(score, 2).ToString("0.00");
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pause();
            }
            if (invert)
            {
                Timer("Invert");
            }
            else if (shield)
            {
                Timer("Shield");
            }
            else
            {
                powerUpText.text = "";
                powerUpType.text = "";
            }
        }
        else
        {
            high_score_text.text = score.ToString("0.00");
        }
    }

    void Start()
    {
        // Debug.Log (gameObject.tag + " started");
        t = powerUpTime;
        if (cars.Length != 0)
        {
            StartCoroutine(spawn());
        }
    }

    public void GameOver()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Level");
        if (objs.Length > 0)
        {
            foreach (GameObject go in objs)
            {
                MoveRoad level = go.GetComponent<MoveRoad>();
                level.SetVelocity(new Vector3(0.0f, 0.0f, 0.0f));
            }
        }
        // else
        //  Debug.Log ("cannot find 'MoveRoad' script");

        gameover = true;
        // Debug.Log("Game Over!");
        gameover_panel.SetActive(true);
    }

    IEnumerator WaitToLoad()
    {
        yield return new WaitForSeconds(5);
        Application.LoadLevel(0);
    }
}

