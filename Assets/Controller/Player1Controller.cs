using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class Player1Controller : MonoBehaviour {
	
	//All objects report to the game manager which handles scenes, data, and menus  
	public GameObject 							AppManager;
	
	//REWIRED CONTROLLER SUPPORT
	//================================================================
	public int playerId = 0; // The Rewired player id of this character
	private Player player; // The Rewired Player
	private CharacterController cc;
	private Vector3 moveVector;

	void Awake (){
		//rewire start settings
		// Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
		player = ReInput.players.GetPlayer(playerId);
		// Get the character controller
		cc = GetComponent<CharacterController>();
		isControllable = true;
	}
	//======================================================================================================
	
	//OBJECT VARIABLES
	// disables for menus, pause screens, death, paralysis, etc.
	public bool isControllable					= true; 				
	public GameObject 							RotationController;

	//movement variables 
	private float speed 						= 0.0f;
	private float speedNormal 					= .42f;
	private float speedMax 						= 2f;
	private Vector3 direction; 
	
	private bool canHyperspeed 					= true;
	public float hyperEnergy					= 0f;
	private float hyperEnergyMax				= 10f;
	private float hyperExhaustRate				= 2f; 
	private float hyperRestoreRate				= 1f;
	private float collisionRadius				= .5f;

	
	//player ship objects to show/hide active/inactive and variable ship stats
	public bool usingVector; 
	public GameObject 							vectorShip; 
	public int vectorHp							= 5;
	public float vectorSpeedNormal				= .42f; 
	public float vectorSpeedMax					= 2f;
	public float vectorEnergyMax				= 10f;
	public float vectorExhaustRate				= 2f;
	public float vectorRestoreRate				= 1f;
	public float vectorcollisionRadius			= .5f;
	
	public bool usingStalingrad; 
	public GameObject 							stalingradShip;
	public int stalingradHp						= 5;
	public float stalingradSpeedNormal			= .42f; 
	public float stalingradSpeedMax				= 2f;
	public float stalingradEnergyMax			= 10f;
	public float stalingradExhaustRate			= 2f;
	public float stalingradRestoreRate			= 1f;
	public float stalingradCollisionRadius		= .5f;

	public bool usingMoonFennec; 
	public GameObject 							moonFennecShip;
	public int moonFennecHp						= 5;
	public float moonFennecSpeedNormal			= .42f; 
	public float moonFennecSpeedMax				= 2f;
	public float moonFennecEnergyMax			= 10f;
	public float moonFennecExhaustRate			= 2f;
	public float moonFennecRestoreRate			= 1f;
	public float moonFennecCollisionRadius		= .5f;

	public bool usingLaGalaFighter; 
	public GameObject 							laGalaFighterShipL;
	public GameObject 							laGalaFighterShipR;
	public int laGalaFighterHp					= 5;
	public float laGalaFighterSpeedNormal		= .42f; 
	public float laGalaFighterSpeedMax			= 2f;
	public float laGalaFighterEnergyMax			= 10f;
	public float laGalaFighterExhaustRate		= 2f;
	public float laGalaFighterRestoreRate		= 1f;
	public float laGalaFighterCollisionRadius	= .5f;

	public bool usingHunter; 
	public GameObject hunterShip;
	public float hunterSpeedNormal				= .42f; 
	public int HunterHp							= 5;
	public float hunterSpeedMax					= 2f;
	public float hunterEnergyMax				= 10f;
	public float hunterExhaustRate				= 2f;
	public float hunterRestoreRate				= 1f;
	public float hunterCollisionRadius			= .5f;

	public bool usingEvolved; 
	public GameObject 							evolvedShip;
	public int evolvedHp						= 5;
	public float evolvedSpeedNormal				= .42f; 
	public float evolvedSpeedMax				= 2f;
	public float evolvedEnergyMax				= 10f;
	public float evolvedExhaustRate				= 2f;
	public float evolvedRestoreRate				= 1f;
	public float evolvedCollisionRadius			= .5f;

	public GameObject 							explosionFX; 	//sound effect in explosion			
	public GameObject							chargingFX;

	public bool paused 							= false;		//pause state
	private bool canPause 						= true;

	public int hitPoints						= 5;
	public GameObject 							hpmarker1;
	public GameObject 							hpmarker2;
	public GameObject 							hpmarker3;
	public GameObject 							hpmarker4;
	public GameObject 							hpmarker5;
	// Use this for initialization
	void Start () {
		//pausePanel.SetActive(false);
		ShipActivator ();		//can also be put in update if needed
	}
	
	// Update is called once per frame
	void Update () {
		updateHP ();
		//simplest pause i could find
		if (player.GetButtonDown ("Pause"))
		{
			if (!paused) {PauseGame();}
			else  {ContinueGame();}
		} 
		
		// if player controllable, then check to see if using hyperspeed then and move character
		if ( isControllable )											
		{
			Hyperspeed (); 
			HyperspeedEnergyLimiter ();
			UpdateMove ();
		}
			 
	
	}

	private void PauseGame()
	{
		Debug.Log ("Game Pause");
		if(canPause){Time.timeScale = 0;}
		//pausePanel.SetActive(true);
		isControllable = false; 
		//Disable scripts that still work while timescale is set to 0
		RotationController.SetActive (false);
		paused = true;
	} 
	private void ContinueGame()
	{
		Debug.Log ("Game Continued");
		if(canPause){Time.timeScale = 1;}
		//pausePanel.SetActive(false);
		isControllable = true;
		RotationController.SetActive (true);
		//enable the scripts again
		paused = false;
	}

	void UpdateMove (){													
	
		//Axis Movement, uses controller.move for precise collision using character controller
		moveVector.x = player.GetAxis("LS Move Horizontal"); 			// get input by name or action id
		moveVector.z = player.GetAxis("LS Move Vertical");
		
		if(moveVector.x != 0.0f || moveVector.z != 0.0f) {
			direction = new Vector3 (moveVector.x * Time.deltaTime, 0, moveVector.z * Time.deltaTime).normalized * speed;
			cc.Move (direction);		
		}

	}
	void Hyperspeed (){

		//charging fx code
		if (hyperEnergy <= hyperEnergyMax *.9f) {
			chargingFX.SetActive (true);
		} 
		else {
			chargingFX.SetActive (false);
		}

		//press the button
		if (player.GetButton /*LongPress*/ ("Hyperspeed")){
			//what to do if you have no energy
			if (hyperEnergy <= 0){
				canHyperspeed = false;
				hyperEnergy += hyperRestoreRate * Time.deltaTime;
			}
			
			//what to do if you have energy
			else {
				
				//but you have less than enough energy to start
				if (hyperEnergy <= 0){
					canHyperspeed = false;
				}
				// you have enough to start
				if (hyperEnergy >= hyperEnergyMax * .25f){
				canHyperspeed = true;

				}
			}
		

			//the actual line to speed you up and restore passively
			if (canHyperspeed){
				speed = speedMax;
				hyperEnergy -= hyperExhaustRate * Time.deltaTime;
				}
			if (!canHyperspeed) {
				speed = speedNormal;
				//hyperEnergy += hyperRestoreRate * Time.deltaTime;

			}
			chargingFX.SetActive (false);
			
		}
		 else {

			hyperEnergy += hyperRestoreRate * Time.deltaTime;
			speed = speedNormal;
//			if (hyperEnergy >= hyperEnergyMax) {
//				hyperEnergyMax -= .05f;
//			}
		}

	}
	
	void HyperspeedEnergyLimiter (){
			if (hyperEnergy >= hyperEnergyMax){
				hyperEnergy = hyperEnergyMax *.999F;}
			
			if (hyperEnergy <= 0){
				hyperEnergy = 0;}	
	}
	
	//For ship selection it sets the object active at match start
	void ShipActivator (){
		if (usingVector){
			vectorShip.SetActive (true);
			speed = vectorSpeedNormal; 
			speedMax = vectorSpeedMax;
			hyperEnergyMax = vectorEnergyMax;
			hyperExhaustRate = vectorExhaustRate; 
			hyperRestoreRate = vectorRestoreRate;	
			collisionRadius = vectorcollisionRadius;
			hitPoints = vectorHp;
			}
			else vectorShip.SetActive (false); 
			
		if (usingStalingrad){
			stalingradShip.SetActive (true);
			speed = stalingradSpeedNormal; 
			speedMax = stalingradSpeedMax; 
			hyperEnergyMax = stalingradEnergyMax;
			hyperExhaustRate = stalingradExhaustRate; 
			hyperRestoreRate = stalingradRestoreRate;	
			collisionRadius = stalingradCollisionRadius;
			hitPoints = stalingradHp;
			}
			else stalingradShip.SetActive (false); 
			
		if (usingMoonFennec){
			moonFennecShip.SetActive (true);
			speed = moonFennecSpeedNormal; 
			speedMax = moonFennecSpeedMax; 
			hyperEnergyMax = moonFennecEnergyMax;
			hyperExhaustRate = moonFennecExhaustRate; 
			hyperRestoreRate = moonFennecRestoreRate;	
			collisionRadius = moonFennecCollisionRadius;
			hitPoints = moonFennecHp;
			}
			else moonFennecShip.SetActive (false); 
			
		if (usingLaGalaFighter){
			laGalaFighterShipL.SetActive (true);
			laGalaFighterShipR.SetActive (true);
			speed = laGalaFighterSpeedNormal; 
			speedMax = laGalaFighterSpeedMax; 
			hyperEnergyMax = laGalaFighterEnergyMax;
			hyperExhaustRate = laGalaFighterExhaustRate; 
			hyperRestoreRate = laGalaFighterRestoreRate;
			collisionRadius = laGalaFighterCollisionRadius;
			hitPoints = laGalaFighterHp;
			}
		if (!usingLaGalaFighter){laGalaFighterShipL.SetActive (false); 
			laGalaFighterShipR.SetActive (false);}
			
		if (usingHunter){
			hunterShip.SetActive (true);
			speed = hunterSpeedNormal; 
			speedMax = hunterSpeedMax; 
			hyperEnergyMax = hunterEnergyMax;
			hyperExhaustRate = hunterExhaustRate; 
			hyperRestoreRate = hunterRestoreRate;	
			collisionRadius = hunterCollisionRadius;
			hitPoints = HunterHp;
			}
			else hunterShip.SetActive (false); 
			
		if (usingEvolved){
			evolvedShip.SetActive (true);
			speed = evolvedSpeedNormal; 
			speedMax = evolvedSpeedMax; 
			hyperEnergyMax = evolvedEnergyMax;
			hyperExhaustRate = evolvedExhaustRate; 
			hyperRestoreRate = evolvedRestoreRate;	
			collisionRadius = evolvedCollisionRadius;
			hitPoints = evolvedHp;
			}
			else evolvedShip.SetActive (false); 
			
		cc.radius = collisionRadius;
	}
	
	//COLLISION SECTION ======================
	
	void OnTriggerEnter( Collider other) {			

		if (playerId == 0){ 
			if (other.tag == "Player2" || other.tag == "Player3" ||other.tag == "Player4" ){
				Debug.Log ("Player1 Collision");
				hitPoints -= 1;
					if (hitPoints <= 0){
						Explode	();	
						   SceneManager.LoadScene("P2Victory", LoadSceneMode.Single);

					}
			}
		}
			
			
		if (playerId == 1){
			if (other.tag == "Player3" || other.tag == "Player4" ||other.tag == "Player1" ){
				Debug.Log ("Player2 Collision");		
				hitPoints -= 1;
					if (hitPoints <= 0){
						Explode	();	
						   SceneManager.LoadScene("P1Victory", LoadSceneMode.Single);
					}
			}
		}
		if (playerId == 2){
			if (other.tag == "Player4" || other.tag == "Player1" ||other.tag == "Player2" ){
				Debug.Log ("Player3 Collision");
				hitPoints -= 1;
					if (hitPoints <= 0){
						Explode	();	
						SceneManager.LoadScene("P3Victory", LoadSceneMode.Single);
					}
			}
		}
		
		if (playerId == 3){
			if (other.tag == "Player1" || other.tag == "Player2" ||other.tag == "Player3" ){
				Debug.Log ("Player4 Collision");
					hitPoints -= 1;
					if (hitPoints <= 0){
						Explode	();	
						SceneManager.LoadScene("P4Victory", LoadSceneMode.Single);
					}
			}
		}
											

	}
	
	void Explode (){
		canPause = false;
		Time.timeScale = .1f;
		isControllable = false;
		//spawn explosion
		 Instantiate(explosionFX, transform.position, transform.rotation);
		RotationController.SetActive (false);
		
		//turn off ship meshes
		vectorShip.SetActive (false);
		stalingradShip.SetActive (false);
		moonFennecShip.SetActive (false);
		laGalaFighterShipL.SetActive (false);
		laGalaFighterShipR.SetActive (false);
		hunterShip.SetActive (false);
		evolvedShip.SetActive (false);
		gameObject.SetActive (false);

		Time.timeScale = 1f;

	}

	void updateHP (){
		if (hitPoints <= 4) {
			hpmarker5.SetActive (false);
		}

		if (hitPoints <= 3) {
			hpmarker4.SetActive (false);
		}

		if (hitPoints <= 2) {
			hpmarker3.SetActive (false);
		}

		if (hitPoints <= 1) {
			hpmarker2.SetActive (false);
		}
			
	}
		
}
	