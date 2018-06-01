using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class SpecialWeaponVector : MonoBehaviour {

	public Rigidbody bulletStage1;
	public Rigidbody bulletStage2;
	public float velocityStage1 = 100.0f;
	public float velocityStage2 = 100.0f;
	public GameObject basicGun;

	//REWIRED CONTROLLER SUPPORT
	//================================================================
	public int playerId = 0; // The Rewired player id of this character
	private Player player; // The Rewired Player

	void Awake (){
		// Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
		player = ReInput.players.GetPlayer(playerId);
	}
	//======================================================================================================

	private bool canSpecial 					= true;

	public float specialChargeTime 				= 0;
	public float specialChargeRate				= 1f;
	public float specialStage1Time 				= 5f;
	public float specialStage2Time 				= 20f;
	public GameObject							chargingFX01;
	public GameObject							chargingFX02;

	//charging
	void Update () {

		//activating
		if (player.GetButtonUp ("Special") && specialChargeTime >= specialStage1Time && specialChargeTime < specialStage2Time) {
			Rigidbody newBullet = Instantiate (bulletStage1, transform.position, transform.rotation) as Rigidbody;
			newBullet.AddForce (-transform.forward * velocityStage1, ForceMode.VelocityChange);
			GetComponent<AudioSource> ().Play ();
			Reset ();

		} 
		if (player.GetButtonUp ("Special") && specialChargeTime >= specialStage2Time) {
			Reset ();
			Rigidbody newBullets2 = Instantiate (bulletStage2, transform.position, transform.rotation) as Rigidbody;
			newBullets2.AddForce (-transform.forward * velocityStage2, ForceMode.VelocityChange);
			GetComponent<AudioSource> ().Play ();
		} 

		if (player.GetButton ("Special") && canSpecial) {			
			basicGun.SetActive (false);
			specialChargeTime += Time.deltaTime * specialChargeRate; 
			if (playerId == 0){
				chargingFX01.SetActive (true);
			}
			if (playerId == 1){
				chargingFX02.SetActive (true);
			}

		}
		else {Reset (); basicGun.SetActive (true); chargingFX01.SetActive (false); chargingFX02.SetActive (false);}




	}
	void Reset(){specialChargeTime = 0;}
}
