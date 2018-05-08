using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;

[RequireComponent(typeof(CharacterController))]
public class Player1Controller : MonoBehaviour {

	public bool isControllable					= true; 				// disables for menus or pause screens

	//required rewired =====================================================================================
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
	}
	//======================================================================================================
	
	//movement variables 
	private float speed 						= 0.0f;
	private float speedNormal 					= .42f;
	private float speedMax 						= 2f;
	private Vector3 direction; 
	
	private bool canHyperspeed 					= true;
	private float hyperEnergy					= 0f;
	private float hyperEnergyMax				= 10f;
	private float hyperExhaustRate				= 2f; 
	private float hyperRestoreRate				= 1f;

	
	//player ship objects and bool to be set active/ inactive
	public bool usingVector; 
	public GameObject vectorShip; 
	public float vectorSpeedNormal				= .42f; 
	public float vectorSpeedMax					= 2f;
	public float vectorEnergyMax				= 10f;
	public float vectorExhaustRate				= 2f;
	public float vectorRestoreRate				= 1f;
	
	public bool usingStalingrad; 
	public GameObject stalingradShip;
	public float stalingradSpeedNormal				= .42f; 
	public float stalingradSpeedMax					= 2f;
	public float stalingradEnergyMax				= 10f;
	public float stalingradExhaustRate				= 2f;
	public float stalingradRestoreRate				= 1f;
	
	public bool usingMoonFennec; 
	public GameObject moonFennecShip;
	public float moonFennecSpeedNormal				= .42f; 
	public float moonFennecSpeedMax					= 2f;
	public float moonFennecEnergyMax				= 10f;
	public float moonFennecExhaustRate				= 2f;
	public float moonFennecRestoreRate				= 1f;
	
	public bool usingLaGalaFighter; 
	public GameObject laGalaFighterShipL;
	public GameObject laGalaFighterShipR;
	public float laGalaFighterSpeedNormal				= .42f; 
	public float laGalaFighterSpeedMax					= 2f;
	public float laGalaFighterEnergyMax				= 10f;
	public float laGalaFighterExhaustRate				= 2f;
	public float laGalaFighterRestoreRate				= 1f;
	
	public bool usingHunter; 
	public GameObject hunterShip;
	public float hunterSpeedNormal				= .42f; 
	public float hunterSpeedMax					= 2f;
	public float hunterEnergyMax				= 10f;
	public float hunterExhaustRate				= 2f;
	public float hunterRestoreRate				= 1f;
	
	public bool usingEvolved; 
	public GameObject evolvedShip;
	public float evolvedSpeedNormal				= .42f; 
	public float evolvedSpeedMax					= 2f;
	public float evolvedEnergyMax				= 10f;
	public float evolvedExhaustRate				= 2f;
	public float evolvedRestoreRate				= 1f;


	[SerializeField] private GameObject pausePanel; 
	public bool paused = false;

	// Use this for initialization
	void Start () {
		pausePanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (player.GetButtonDown ("Pause"))
		{
			if (!paused) 
			{
				PauseGame();

			}
			else  
			{
				ContinueGame();   
			}
		} 

		ShipActivator ();
		if ( isControllable )											// if player controllable, then move character
		{
			Hyperspeed (); 
			HyperspeedEnergyLimiter ();
			UpdateMove ();
		}
	

	}

	private void PauseGame()
	{
		Debug.Log ("Game Pause");
		Time.timeScale = 0;
		pausePanel.SetActive(true);
		isControllable = false; 
		//Disable scripts that still work while timescale is set to 0
		paused = true;
	} 
	private void ContinueGame()
	{
		Debug.Log ("Game Continued");
		Time.timeScale = 1;
		pausePanel.SetActive(false);
		isControllable = true;
		//enable the scripts again
		paused = false;
	}

	void UpdateMove (){													//Axis Movement, uses controller.move for proper collsion using character controller

		moveVector.x = player.GetAxis("LS Move Horizontal"); 			// get input by name or action id
		moveVector.z = player.GetAxis("LS Move Vertical");
		
		if(moveVector.x != 0.0f || moveVector.z != 0.0f) {
			direction = new Vector3 (moveVector.x * Time.deltaTime, 0, moveVector.z * Time.deltaTime).normalized * speed;
			cc.Move (direction);		
		}

	}
	void Hyperspeed (){
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
			
		}
		else hyperEnergy += hyperRestoreRate * Time.deltaTime;

	}
	
	void HyperspeedEnergyLimiter (){
			if (hyperEnergy > hyperEnergyMax){
				hyperEnergy = hyperEnergyMax;}
			
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
			}
			else vectorShip.SetActive (false); 
			
		if (usingStalingrad){
			stalingradShip.SetActive (true);
			speed = stalingradSpeedNormal; 
			speedMax = stalingradSpeedMax; 
			hyperEnergyMax = stalingradEnergyMax;
			hyperExhaustRate = stalingradExhaustRate; 
			hyperRestoreRate = stalingradRestoreRate;	
			}
			else stalingradShip.SetActive (false); 
			
		if (usingMoonFennec){
			moonFennecShip.SetActive (true);
			speed = moonFennecSpeedNormal; 
			speedMax = moonFennecSpeedMax; 
			hyperEnergyMax = moonFennecEnergyMax;
			hyperExhaustRate = moonFennecExhaustRate; 
			hyperRestoreRate = moonFennecRestoreRate;	
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
			}
			else hunterShip.SetActive (false); 
			
		if (usingEvolved){
			evolvedShip.SetActive (true);
			speed = evolvedSpeedNormal; 
			speedMax = evolvedSpeedMax; 
			hyperEnergyMax = evolvedEnergyMax;
			hyperExhaustRate = evolvedExhaustRate; 
			hyperRestoreRate = evolvedRestoreRate;	
			}
			else evolvedShip.SetActive (false); 
			
	}
}
