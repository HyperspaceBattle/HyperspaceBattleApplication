using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class HideONButton : MonoBehaviour {
	//REWIRED CONTROLLER SUPPORT
	//================================================================
	public int playerId = 0; // The Rewired player id of this character
	public string buttonPress = "Pause";
	public GameObject ObjectToSetActive;
	public GameObject ObjectToSetInactive;
	public bool HasTimer;
	public int seconds = 60;

	private Rewired.Player player; // The Rewired Player
	private CharacterController cc;
	private Vector3 moveVector;

	void Awake (){
		player = ReInput.players.GetPlayer(playerId);
		cc = GetComponent<CharacterController>();
	}

	void Update (){
		//======================================================================================================
		if(player.GetButton (buttonPress)) {
			ObjectToSetInactive.SetActive (false);
			ObjectToSetActive.SetActive (true);
		}

		if (HasTimer) {	StartCoroutine (Timer ());}

	}

	IEnumerator Timer()
	{
		if(player.GetButton (buttonPress)) {
			ObjectToSetInactive.SetActive (false);
			ObjectToSetActive.SetActive (true);
		}

		yield return new WaitForSeconds(seconds);
		ObjectToSetInactive.SetActive (false);
		ObjectToSetActive.SetActive (true);
	}
}
