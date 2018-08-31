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
    private string timerScene;

    void Awake ()
    {
        this.gameTime = 0f;
        this.players = new Player[AppManager.PlayerCount];
        //rewire start settings
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        for (int id = 0; id < AppManager.PlayerCount; id++)
        {
            this.players[id] = ReInput.players.GetPlayer(id);
            this.players[id].AddInputEventDelegate(OnInputUpdate, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed);
        }
        timerScene = (goToSplashScreen) ? AppManager.ResetScene : sceneToLoad;
    }

    void OnInputUpdate(InputActionEventData data)
    {
        try
        {
            this.NextScene(false);
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
                this.NextScene(true);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in ChangeScenePressAnyButton's FixedUpdate: " + ex.Message.ToString());
        }        
    }

    void NextScene(bool bolTimer)
    {
        try
        {
            foreach (Player player in this.players)
                player.ClearInputEventDelegates();

            if (bolTimer)
                SceneManager.LoadScene(timerScene, LoadSceneMode.Single);
            else
                SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in ChangeScenePressAnyButton's NextScene: " + ex.Message.ToString());
        }
    }
}