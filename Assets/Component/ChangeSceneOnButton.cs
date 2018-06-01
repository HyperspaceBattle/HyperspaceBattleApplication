using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;

public class ChangeSceneOnButton : MonoBehaviour {
	//REWIRED CONTROLLER SUPPORT
	//================================================================
	public int playerId = 0; // The Rewired player id of this character
	public string buttonPress;
	public string sceneToLoad = "MainMenu";
	public bool HasTimer;
	public int seconds = 60;

	private Player player; // The Rewired Player
	private CharacterController cc;
	private Vector3 moveVector;

	void Awake (){
		player = ReInput.players.GetPlayer(playerId);
		cc = GetComponent<CharacterController>();
	}

	void Update (){
	//======================================================================================================
		if(player.GetButton (buttonPress)) {
			SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
		}

		if (HasTimer) {	StartCoroutine (Timer ());}

	}

	IEnumerator Timer()
	{
		if(player.GetButton (buttonPress)) {
			SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
		}

		yield return new WaitForSeconds(seconds);
		SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
	}
}
