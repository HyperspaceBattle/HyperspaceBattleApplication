using UnityEngine;
using System.Collections;
using Rewired; 

public class PlayerAimRotate : MonoBehaviour {
	
	public int playerId = 0; // The Rewired player id of this character
	private Rewired.Player player; // The Rewired Player

	public bool invert = false;
	private float invertValue = -1f;

	void Awake (){
		// Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
		player = ReInput.players.GetPlayer(playerId);
	}
	
		void Update () {

		if (invert) {
			invertValue = -1f;
		} else invertValue = 1;

		Vector3 worldPosition = new Vector3(-player.GetAxis("RS Fire Horizontal") * invertValue, 0 ,-player.GetAxis("RS Fire Vertical") * invertValue)+ transform.position;
		transform.LookAt(worldPosition, Vector3.up);
	}
}
