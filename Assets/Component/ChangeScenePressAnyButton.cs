﻿using UnityEngine;	
using System.Collections.Generic;
using Rewired;
using UnityEngine.SceneManagement;


public class ChangeScenePressAnyButton : MonoBehaviour {
	//REWIRED CONTROLLER SUPPORT
	//================================================================
	public int playerId = 0; // The Rewired player id of this character
	public string sceneToLoad = "MainMenu";
	private Rewired.Player player; // The Rewired Player
	private CharacterController cc;
	private Vector3 moveVector;
	private GameObject singleton;

	void Awake (){
		//rewire start settings
		// Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
		player = ReInput.players.GetPlayer(playerId);
		// Get the character controller
		cc = GetComponent<CharacterController>();
	}

	void Update (){
	//======================================================================================================

		if(player.GetButton ("Pause")) {
			Debug.Log ("start pressed");
				// I was having trouble with the singleton staying when resetting the game so I'm destroying it by name
				singleton = GameObject.Find("$Initialization"); 
				Destroy(singleton);

			SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
		}
	}
}