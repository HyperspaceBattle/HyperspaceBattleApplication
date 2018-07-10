using UnityEngine;
using System.Collections;
using Rewired;

public class PlayerGunMG : MonoBehaviour {

	public int playerId = 0; // The Rewired player id of this character
	private Rewired.Player player; // The Rewired Player

	public Rigidbody bullet;
	public float delayTime = .05f;	
	public float velocity = 100.0f;
	public float spread = 10.0f;
	
	private float currentCharge = 0.0f;
	
	void Awake (){
		// Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
		player = ReInput.players.GetPlayer(playerId);
		// Get the character controller
	}
	void Update () 
	{
		currentCharge += Time.deltaTime;
		//transform.rotation = Quaternion.AngleAxis(-1, Vector3.up) * transform.rotation;
		transform.localRotation = Quaternion.AngleAxis(Random.Range (-spread,spread), Vector3.up);

		if (player.GetAxis("RS Fire Horizontal") != 0 | player.GetAxis("RS Fire Vertical") != 0)
		{
			if (currentCharge >= delayTime)
			{
				Rigidbody newBullet = Instantiate(bullet,transform.position,transform.rotation) as Rigidbody;
				newBullet.AddForce (transform.forward*velocity,ForceMode.VelocityChange);
				GetComponent<AudioSource>().Play();
				Reset();
			}
		}
	}
	
	void Reset(){currentCharge = 0;}
}
