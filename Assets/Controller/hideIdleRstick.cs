using UnityEngine;
using System.Collections;
using Rewired;

public class hideIdleRstick : MonoBehaviour {

	public int playerId = 0; // The Rewired player id of this character
	private Rewired.Player player; // The Rewired Player
	private bool fh;
	private bool fv;
void Awake (){
		// Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
		player = ReInput.players.GetPlayer(playerId);

	}
	public GameObject hideObject; 

	// Update is called once per frame
	void Update () {
		if (player.GetAxis("RS Fire Horizontal") >= .1f ) { fh = true;}
		if (player.GetAxis("RS Fire Horizontal") < .1f ) { fh = false;}
		if (player.GetAxis("RS Fire Vertical") >= .1f ) { fv = true;}
		if (player.GetAxis("RS Fire Vertical") < .1f ) { fv = false;}



		if (!fh && !fv){ Hide ();}
		else {Show();}
	}
	
	void Show			() {												// turn player rendering mesh 'on'
	hideObject.SetActive(true);
	}
	
	void Hide			() {												// turn player rendering mesh 'off'
	hideObject.SetActive(false);
	}
}
