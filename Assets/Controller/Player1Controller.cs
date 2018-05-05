using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;

[RequireComponent(typeof(CharacterController))]
public class Player1Controller : MonoBehaviour {

	//required rewired variables 
	public int playerId = 0; // The Rewired player id of this character
	private Player player; // The Rewired Player
	private CharacterController cc;
	private Vector3 moveVector;

	//MASTER CONTROLLER IS A SINGLETON RUNS ON ALL SCENES BUT MAIN MENU =====================================
	private static Player1Controller instance = null;	
	public static Player1Controller Instance
	{
		get { return instance; }
	}
	void Awake (){
		if (Instance != null && instance != this)
		{
			Destroy(this.gameObject);
			return;
		}
		else
		{
			instance = this;	
		}
		DontDestroyOnLoad(this.gameObject);
		gameObject.name = "$Player1Controller";

		//rewire start settings
		// Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
		player = ReInput.players.GetPlayer(playerId);
		// Get the character controller
		cc = GetComponent<CharacterController>();
	}
	//======================================================================================================

	//movement variables 
	public float speed 							= 0.0f;
	public float speedNormal 					= .42f;
	public float speedMax 						= 2f;
	private Vector3 direction; 

	public bool isControllable					= true; 				// enables you to use player control

	
	// Use this for initialization
	void Start () {
		speed = speedNormal;
	}
	
	// Update is called once per frame
	void Update () {
		if ( isControllable )											// if player controllable, then move character
		{
			UpdateMove ();
		}
		
		
	}

	void UpdateMove (){													//Axis Movement, uses controller.move for proper collsion using character controller

		moveVector.x = player.GetAxis("LS Move Horizontal"); 			// get input by name or action id
		moveVector.z = player.GetAxis("LS Move Vertical");
		
		if(moveVector.x != 0.0f || moveVector.z != 0.0f) {
			direction = new Vector3 (moveVector.x * Time.deltaTime, 0, moveVector.z * Time.deltaTime).normalized * speed;
			cc.Move (direction);		
		}

	}
}
