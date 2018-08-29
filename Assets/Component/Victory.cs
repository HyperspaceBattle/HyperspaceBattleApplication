using UnityEngine;	
using System.Collections.Generic;
using Rewired;
using UnityEngine.SceneManagement;
using System;

public class Victory : MonoBehaviour
{
    //REWIRED CONTROLLER SUPPORT
    //================================================================
    [SerializeField] private float Timer = 10f;
    public MeshRenderer winner;
    private Player[] players; // The Rewired Player
    private float gameTime;

    void Awake ()
    {
        gameTime = 0f;
        this.players = new Player[AppManager.PlayerCount];
        //rewire start settings
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        for (int id = 0; id < AppManager.PlayerCount; id++)
        {
            players[id] = ReInput.players.GetPlayer(id);
            players[id].AddInputEventDelegate(OnPausePress, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Pause");
        }
        winner.material = (Material)Resources.Load("Materials/P" + AppManager.Victor + "Wins"); 
    }

	void FixedUpdate ()
    {
        try
        {
            gameTime += Time.deltaTime;
            if (gameTime > Timer)
            {
                MainMenu(true);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in Victory's FixedUpdate: " + ex.Message.ToString());
        }
    }

    void OnPausePress(InputActionEventData data)
    {
        try
        {
            Debug.Log("PRESSED");
            MainMenu(false);
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in Victory's OnPausePress: " + ex.Message.ToString());
        }
    }

    void MainMenu(bool bolTimer)
    {
        foreach (Player player in players)
            player.ClearInputEventDelegates();
        if(bolTimer)
            SceneManager.LoadScene("Splash", LoadSceneMode.Single);
        else
            SceneManager.LoadScene("ShipSelectMenu", LoadSceneMode.Single);
    }
}