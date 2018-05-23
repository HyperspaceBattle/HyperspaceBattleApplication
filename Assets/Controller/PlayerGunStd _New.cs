using UnityEngine;
using System.Collections;
using Rewired;

public class PlayerGunStd_New : MonoBehaviour {
    
	private Player player; // The Rewired Player
    private PlayerCharacter character;
    private float gameTime;
    private float lastPressed;
	
    public void Init(GameObject parent)
    {
        this.character = parent.GetComponent<PlayerCharacter>();        
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        this.player = ReInput.players.GetPlayer(this.character.Model.PlayerID);
        this.gameTime = 0f;
        this.lastPressed = 0f;
    }
    void Update () 
	{
		gameTime += Time.deltaTime * 1000;

		if (player.GetAxis("RS Fire Horizontal") != 0 | player.GetAxis("RS Fire Vertical") != 0)
		{
			if ((gameTime - lastPressed) > (1000/ this.character.Model.BulletDelayTime))
			{
				if (!player.GetButton /*LongPress*/ ("Hyperspeed")) {

					Rigidbody newBullet = Instantiate ((Rigidbody)Resources.Load(this.character.Model.BulletType), transform.position, transform.rotation) as Rigidbody;
					//newBullet.AddForce (-transform.forward*velocity,ForceMode.VelocityChange);
					newBullet.AddForce (-transform.forward * this.character.Model.BulletVelocity, ForceMode.VelocityChange);
					GetComponent<AudioSource> ().Play ();
                    lastPressed = gameTime;
				}
			}
		}
	}
}
