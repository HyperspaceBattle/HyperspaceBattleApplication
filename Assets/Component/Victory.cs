using UnityEngine;	
using System.Collections.Generic;
using Rewired;
using UnityEngine.SceneManagement;
using System;

public class Victory : MonoBehaviour
{
    //REWIRED CONTROLLER SUPPORT
    //================================================================
    [SerializeField] private float Timer;
    public MeshRenderer winner;
    private Rewired.Player[] players; // The Rewired Player
	private Vector3 moveVector;
	private GameObject singleton;
    private float gameTime;

    void Awake ()
    {
        gameTime = 0f;
        int playerCount = AppManager.PlayerCount;
        players = new Player[playerCount];
        //rewire start settings
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        for (int id = 0; id < playerCount; id++)
        {
            players[id] = ReInput.players.GetPlayer(id);
            players[id].AddInputEventDelegate(OnPausePress, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Pause");
        }
        winner.sharedMaterial = (Material)Resources.Load("Prefab/Materials Shared/P" + AppManager.Victor + "Wins"); 
    }

	void FixedUpdate ()
    {
        gameTime += Time.deltaTime;
        if (gameTime < Timer)
        {
            Debug.Log("Changing by Timer.");
            MainMenu();
        }
    }

    void OnPausePress(InputActionEventData data)
    {
        try
        {
            Debug.Log("Changing by Start Press.");
            MainMenu();
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in Victory's OnPausePress: " + ex.Message.ToString());
        }
    }

    void MainMenu()
    {
        foreach (Player player in players)
            player.ClearInputEventDelegates();
        // I was having trouble with the singleton staying when resetting the game so I'm destroying it by name
        //singleton = GameObject.Find("$Initialization");
        //Destroy(singleton);
        SceneManager.LoadScene("Resources/Scenes/ShipSelectMenu", LoadSceneMode.Single);
    }
}