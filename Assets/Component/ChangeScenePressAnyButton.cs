using UnityEngine;	
using Rewired;
using UnityEngine.SceneManagement;
using System;
public class ChangeScenePressAnyButton : MonoBehaviour
{
	//REWIRED CONTROLLER SUPPORT
	//================================================================
	public string sceneToLoad = "MainMenu";
	public bool goToSplashScreen = false;
	public int seconds = 60;
    private Player[] players; // The Rewired Player
    private float gameTime;

    void Awake ()
    {
        this.gameTime = 0f;
        int playerCount = AppManager.PlayerCount;
        this.players = new Player[playerCount];
        //rewire start settings
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        for (int id = 0; id < playerCount; id++)
        {
            this.players[id] = ReInput.players.GetPlayer(id);
            this.players[id].AddInputEventDelegate(OnInputUpdate, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed);
        }
    }

    void OnInputUpdate(InputActionEventData data)
    {
        try
        {
            foreach (Player player in this.players)
                player.ClearInputEventDelegates();
            this.NextScene();
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in ChangeScenePressAnyButton's OnInputUpdate: " + ex.Message.ToString());
        }
    }

    void FixedUpdate()
    {
        try
        {
            this.gameTime += Time.deltaTime;
            if (this.gameTime >= this.seconds)
            {
                if (goToSplashScreen)
                    SceneManager.LoadScene(AppManager.ResetScene, LoadSceneMode.Single);
                else
                    this.NextScene();
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in ChangeScenePressAnyButton's FixedUpdate: " + ex.Message.ToString());
        }        
    }

    void NextScene()
    {
        try
        {
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in ChangeScenePressAnyButton's NextScene: " + ex.Message.ToString());
        }
    }
}