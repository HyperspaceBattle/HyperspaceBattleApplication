using UnityEngine;
using System.Collections;
using Rewired; 

public class PlayerAimRotate : MonoBehaviour {
	
	public int playerId = 0; // The Rewired player id of this character
	private Player player; // The Rewired Player

	void Awake (){
		// Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
		player = ReInput.players.GetPlayer(playerId);
	}
	
		void Update () {
		
		Vector3 worldPosition = new Vector3(-player.GetAxis("RS Fire Horizontal"),0,-player.GetAxis("RS Fire Vertical"))+ transform.position;
		transform.LookAt(worldPosition, Vector3.up);
	}
}
