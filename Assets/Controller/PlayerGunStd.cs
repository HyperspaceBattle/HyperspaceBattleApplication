using UnityEngine;
using System.Collections;
using Rewired;

public class PlayerGunStd : MonoBehaviour {

	public int playerId = 0; // The Rewired player id of this character
	private Rewired.Player player; // The Rewired Player
	public Rigidbody bullet;
	public float delayTime = .05f;	
	public float velocity = 100.0f;


	private float currentCharge = 0.0f;
	
	void Awake (){
		// Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
		player = ReInput.players.GetPlayer(playerId);
	}
	void Update () 
	{
		currentCharge += Time.deltaTime;

		if (player.GetAxis("RS Fire Horizontal") != 0 | player.GetAxis("RS Fire Vertical") != 0)
		{
			if (currentCharge >= delayTime)
			{
				if (!player.GetButton /*LongPress*/ ("Hyperspeed")) {

					Rigidbody newBullet = Instantiate (bullet, transform.position, transform.rotation) as Rigidbody;
					//newBullet.AddForce (-transform.forward*velocity,ForceMode.VelocityChange);
					newBullet.AddForce (-transform.forward * velocity, ForceMode.VelocityChange);
					GetComponent<AudioSource> ().Play ();
					Reset ();
				}
			}
		}
	}
	
	void Reset(){currentCharge = 0;}
}
