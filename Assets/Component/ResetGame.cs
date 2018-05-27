using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour {
//REWIRED CONTROLLER SUPPORT
	//================================================================
	public int playerId = 0; // The Rewired player id of this character
	public string sceneToLoad = "MainMenu";
	private Player player; // The Rewired Player
	private CharacterController cc;
	private Vector3 moveVector;
	
	public int secondsToWait = 400;
	
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
			Reset();
		}
	}
	
	void Reset(){
		Application.OpenURL("http://hsb.itch.io/");
		System.Diagnostics.Process.Start(Application.dataPath.Replace("_Data", ".exe")); 
		Debug.Log ("quit app");
		Application.Quit(); //kill current process
	}

	void Start (){
		StartCoroutine (Timer ());
	}

	IEnumerator Timer()
	{
		yield return new WaitForSeconds(secondsToWait);
		SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
	}

}

