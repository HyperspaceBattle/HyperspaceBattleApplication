using UnityEngine;
using System.Collections;
using Rewired;

public class PlayerShipRotate : MonoBehaviour
{	
	public int playerId = 0; // The Rewired player id of this character
    public float rotationSpeed = 5f;
	private Rewired.Player player; // The Rewired Player

	void Awake ()
    {
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        this.player = ReInput.players.GetPlayer(playerId);
	}
	void FixedUpdate ()
    {
        this.transform.localEulerAngles += new Vector3(this.player.GetAxis("RS Fire Vertical") * this.rotationSpeed, -this.player.GetAxis("RS Fire Horizontal") * this.rotationSpeed);
    }
}
