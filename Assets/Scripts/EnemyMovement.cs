﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int speed = 1;
    public string type;
    public int test;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Vector3.forward * Time.deltaTime);
    }
}
