using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1.0f;
    public float finalSpeed = 600.0f;
    public float initSpeed = 400.0f;
    public float xMin, xMax;
    public float origin;
    private GameObject player;
    private Quaternion target;
    private float smooth = 3.0f;
    private Vector3 initialVelocity;
    private Vector3 finalVelocity;
    private GameController gc;
    private float wait_time, decreased_wait_time;

    void MovePlayer () {
        Vector3 new_position = transform.position;
        Vector3 new_angle = transform.eulerAngles;
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Level");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey("a")){
            if (new_position.x > xMin)
                new_position += Vector3.left * speed * Time.deltaTime;
            if (new_angle.y < 120 && new_position.x > xMin)
                new_angle.y -= 0.3f;
        }
        
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey("d")){
            if (new_position.x < xMax)
                new_position += Vector3.right * speed * Time.deltaTime;
            if (new_angle.y > 70 && new_position.x < xMax)
                new_angle.y += 0.3f;
        }

        if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey("w")){
            if (objs.Length > 0) {
                foreach(GameObject go in objs)  {
                    MoveRoad level = go.GetComponent<MoveRoad>();
                    level.speed = finalSpeed;
                    level.SetVelocity(finalVelocity);
                }
            }
            else
                Debug.Log ("cannot find 'MoveRoad' script");

            if (enemies.Length > 0) {
                foreach(GameObject go in enemies)  {
                    Mover mover = go.GetComponent<Mover>();
                    mover.SetVelocity(mover.speed*2);
                    gc.spawn_wait = decreased_wait_time;
                }
            }
            else
                Debug.Log ("cannot find 'Mover' script");
        }

        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp ("w")) {
            if (objs.Length > 0) {
                foreach(GameObject go in objs)  {
                    MoveRoad level = go.GetComponent<MoveRoad>();
                    level.speed = initSpeed;
                    level.SetVelocity(initialVelocity);
                }
            }
            else
                Debug.Log ("cannot find 'MoveRoad' script");

            if (enemies.Length > 0) {
                foreach(GameObject go in enemies)  {
                    Mover mover = go.GetComponent<Mover>();
                    mover.SetVelocity(mover.speed);
                    gc.spawn_wait = wait_time;
                }
            }
            else
                Debug.Log ("cannot find 'Mover' script");
        }

        transform.position = new_position;
        transform.eulerAngles = new_angle;
        transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * smooth);
        // if (objs.Length > 0) {
        //     foreach(GameObject go in objs)  {
        //         MoveRoad level = go.GetComponent<MoveRoad>();
        //         level.speed = finalSpeed;
        //         level.SetVelocity(Vector3.Slerp(finalVelocity, initialVelocity, 0.5f));
        //     }
        // }
    }

    void MoveCamera () {
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
        if (gameObject.tag == "MainCamera")
            MoveCamera();
        else
            MovePlayer();
    }

    void Start () {
        initialVelocity = new Vector3 (0.0f, 0.0f, initSpeed);
        finalVelocity = new Vector3 (0.0f, 0.0f, finalSpeed);
        GameObject go = GameObject.FindWithTag("GameController");
        if (go) 
            gc = go.GetComponent<GameController>();
        else
            Debug.Log ("cannot find 'GameController' script");
        wait_time = gc.spawn_wait;
        decreased_wait_time = wait_time - 0.25f;
        if (gameObject.tag == "Player"){
            player = gameObject;
            target = Quaternion.Euler(0.0f, 90.0f, 0.0f);
        }
    }
}

