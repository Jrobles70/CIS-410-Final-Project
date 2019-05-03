using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int speed = 1;
    public float xMin, xMax, zMin, zMax;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 new_position = transform.position;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey("a"))
        {
            new_position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey("d"))
        {
            new_position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey("w"))
        {
            new_position += Vector3.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey("s"))
        {
            new_position += Vector3.back * speed * Time.deltaTime;
        }

        transform.position = new Vector3
        (
            Mathf.Clamp(new_position.x, xMin, xMax),
            transform.position.y,
            Mathf.Clamp(new_position.z, zMin, zMax)
        );
    }
}

