using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;

public class TimedReset : MonoBehaviour {
	
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
	//======================================================================================================

	public int secondsToWait = 5;

	void Start (){
		StartCoroutine (Example ());
	}
	void Update()
	{
		if (player.GetButton ("Start")) {
			Debug.Log ("start pressed");
			SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);}

	}

	IEnumerator Example()
	{
		yield return new WaitForSeconds(secondsToWait);
		SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
	}

}
