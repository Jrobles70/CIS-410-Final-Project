using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static int health;

    Text text;

    void Awake()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Health: " + health;
    }
}
