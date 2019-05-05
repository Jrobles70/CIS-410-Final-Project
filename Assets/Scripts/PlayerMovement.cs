using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int speed = 1;
    public float xMin, xMax;

    void move () {
        Vector3 new_position = transform.position;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey("a"))
            new_position += Vector3.left * speed * Time.deltaTime;
        
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey("d"))
            new_position += Vector3.right * speed * Time.deltaTime;

        transform.position = new Vector3
        (
            Mathf.Clamp(new_position.x, xMin, xMax),
            transform.position.y,
            transform.position.z
        );
    }

    void Update()
    {
        move();
    }
}

