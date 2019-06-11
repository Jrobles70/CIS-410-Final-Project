using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    private string sceneName;
    static bool AudioBegin = false;

    private void Start()
    {
        if (!AudioBegin)
        {
            GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
            AudioBegin = true;
            sceneName = SceneManager.GetActiveScene().name;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != sceneName)
        {
            Destroy(gameObject);
            AudioBegin = false;
        }
    }
}
