using UnityEngine;
using System.Collections;
using Rewired;

public class PlayerShipRotate : MonoBehaviour {
	
	public int playerId = 0; // The Rewired player id of this character
	private Player player; // The Rewired Player
	private CharacterController cc;

	void Awake (){
		// Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
		player = ReInput.players.GetPlayer(playerId);
		// Get the character controller
		cc = GetComponent<CharacterController>();
	}
	void Update () {
		//rotate the ship to face the direction your moving in
		Vector3 rotation = new Vector3(-player.GetAxis("LS Move Horizontal"),0,-player.GetAxis("LS Move Vertical"))+ transform.position;
		transform.LookAt(rotation, Vector3.up);
		
		//transform.RotateAround(transform.position, Vector3.up, Time.deltaTime * 90f);
	}
}
