using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1.0f;
    public float finalSpeed = 600.0f;
    public float initSpeed = 400.0f;
    public float xMin, xMax;
    public float yMin = 70.0f, yMax = 120.0f;
    public float origin;
    private Quaternion target;
    private float smooth = 3.0f;
    private Vector3 initialVelocity;
    private Vector3 finalVelocity;
    private GameController gc;
    private bool fast = false;
    public float DecreaseWaitFactor = 0.05f;

    void MoveLeft (ref Vector3 new_position, ref Vector3 new_angle) {
        new_position += Vector3.left * speed * Time.deltaTime;
        if (new_angle.y < yMax && new_position.x > xMin)
            new_angle.y -= 0.3f;
    }

    void MoveRight (ref Vector3 new_position, ref Vector3 new_angle) {
        new_position += Vector3.right * speed * Time.deltaTime;
        if (new_angle.y > yMin && new_position.x < xMax)
            new_angle.y += 0.3f;
    }

    void MovePlayer () {
        Vector3 new_position = transform.position;
        Vector3 new_angle = transform.eulerAngles;
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Level");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] barrels = GameObject.FindGameObjectsWithTag("Invert");
        GameObject[] shields = GameObject.FindGameObjectsWithTag("Shield");
        if (!gc.isPaused) {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey("a")){
                if (gc.invert) {
                    MoveRight (ref new_position, ref new_angle);
                }
                else {
                    MoveLeft (ref new_position, ref new_angle);
                }
            }
            
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey("d")){
                if (gc.invert) {
                    MoveLeft (ref new_position, ref new_angle);
                }
                else {
                    MoveRight (ref new_position, ref new_angle);
                }
            }

            if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey("w")){
                if (objs.Length > 0) {
                    foreach(GameObject go in objs)  {
                        MoveRoad level = go.GetComponent<MoveRoad>();
                        level.speed = finalSpeed;
                        level.SetVelocity(finalVelocity);
                    }
                }
                //barrels
                if (barrels.Length > 0) {
                    foreach(GameObject go in barrels)  {
                        Mover mover = go.GetComponent<Mover>();
                        mover.SetVelocity(mover.speed * gc.speedFactor * 2.0f);
                    }
                }
                //shields
                if (shields.Length > 0) {
                    foreach(GameObject go in shields)  {
                        Mover mover = go.GetComponent<Mover>();
                        mover.SetVelocity(mover.speed * gc.speedFactor * 2.0f);
                    }
                }
                //cars
                if (enemies.Length > 0) {
                    foreach(GameObject go in enemies)  {
                        Mover mover = go.GetComponent<Mover>();
                        mover.SetVelocity(mover.speed * gc.speedFactor * 2.0f);
                        if (!fast) {
                            gc.spawn_wait -= DecreaseWaitFactor;
                            fast = true;
                        }
                    }
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp ("w")) {
            if (objs.Length > 0) {
                foreach(GameObject go in objs)  {
                    MoveRoad level = go.GetComponent<MoveRoad>();
                    level.speed = initSpeed;
                    level.SetVelocity(initialVelocity);
                }
            }
            //barrels
            if (barrels.Length > 0) {
                foreach(GameObject go in barrels)  {
                    Mover mover = go.GetComponent<Mover>();
                    mover.SetVelocity(mover.speed * gc.speedFactor);
                }
            }
            //shields
            if (shields.Length > 0) {
                foreach(GameObject go in shields)  {
                    Mover mover = go.GetComponent<Mover>();
                    mover.SetVelocity(mover.speed * gc.speedFactor);
                }
            }
            //cars
            if (enemies.Length > 0) {
                foreach(GameObject go in enemies)  {
                    Mover mover = go.GetComponent<Mover>();
                    mover.SetVelocity(mover.speed * gc.speedFactor);
                    if (fast) {
                        gc.spawn_wait += DecreaseWaitFactor;
                        fast = false;
                    }
                }
            }
        }

        transform.position = new Vector3 (
        	Mathf.Clamp (new_position.x, xMin, xMax),
        	transform.position.y, 
        	transform.position.z
        );
        transform.eulerAngles = new_angle;
        transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * smooth);
    }

    void MoveCamera () {
        Vector3 new_position = transform.position;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey("a")){
            if (gc.invert) {
                new_position += Vector3.right * speed * Time.deltaTime;
            }
            else {
                new_position += Vector3.left * speed * Time.deltaTime;
            }
        }
        
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey("d")){
            if (gc.invert) {
                new_position += Vector3.left * speed * Time.deltaTime;
            }
            else {
                new_position += Vector3.right * speed * Time.deltaTime;
            }
        }

        transform.position = new Vector3
        (
            Mathf.Clamp(new_position.x, xMin, xMax),
            transform.position.y,
            transform.position.z
        );
    }


    void Update()
    {
        if (!gc.isGameOver()) {
            if (!gc.isPaused && gameObject.tag == "MainCamera")
                MoveCamera();
            else
                MovePlayer();
        }
    }

    void Start () {
        initialVelocity = new Vector3 (0.0f, 0.0f, initSpeed);
        finalVelocity = new Vector3 (0.0f, 0.0f, finalSpeed);
        GameObject go = GameObject.FindWithTag("GameController");
        if (go) 
            gc = go.GetComponent<GameController>();
        // else
        //     Debug.Log ("cannot find 'GameController' script");

        if (gameObject.tag == "Player"){
            target = Quaternion.Euler(0.0f, 90.0f, 0.0f);
        }
    }
}

