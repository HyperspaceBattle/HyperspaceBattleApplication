using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDeleteOnLoad : MonoBehaviour
{
    private bool isCreated = false;

    void Awake()
    {        
        if (!isCreated)
        {
            DontDestroyOnLoad(this.gameObject);
            this.isCreated = true;
        }        
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Equals("Splash"))
        {
            Destroy(this.gameObject);
        }
    }
}
