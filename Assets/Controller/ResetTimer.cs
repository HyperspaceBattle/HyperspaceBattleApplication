using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetTimer : MonoBehaviour
{
    public float resetTime = 60f;
    void FixedUpdate()
    {
        try
        {
            AppManager.GameTimer += Time.deltaTime;
            if(AppManager.GameTimer >= resetTime)
                SceneManager.LoadScene(AppManager.ResetScene, LoadSceneMode.Single);
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in ResetTimer's FixedUpdate: " + ex.Message.ToString());
        }        
    }
}
