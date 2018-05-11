using UnityEngine;	
using System.Collections.Generic;
using Rewired;
using UnityEngine.SceneManagement;


public class ChangeScenePressAnyButton : MonoBehaviour {
	//REWIRED CONTROLLER SUPPORT
	//================================================================
	public int playerId = 0; // The Rewired player id of this character
	private Player player; // The Rewired Player
	private CharacterController cc;
	private Vector3 moveVector;

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
			SceneManager.LoadScene("ShipSelect", LoadSceneMode.Single);
		}
	}
}