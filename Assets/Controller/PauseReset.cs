using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseReset : MonoBehaviour
{
    public float resetTime = 300f;
    void FixedUpdate()
    {
        try
        {
            if (!AppManager.IsUnpaused)
            {
                AppManager.GameTimer += Time.deltaTime;
                if (AppManager.GameTimer >= resetTime)
                    SceneManager.LoadScene(AppManager.ResetScene, LoadSceneMode.Single);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in PauseReset's FixedUpdate: " + ex.Message.ToString());
        }
    }
}
