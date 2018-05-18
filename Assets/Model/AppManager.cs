﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;

public class AppManager : MonoBehaviour {
	//APPMANAGER is a singleton
	private static AppManager instance = null; public static AppManager Instance {get { return instance; }}

	//REWIRED CONTROLLER SUPPORT
	//================================================================
	public int playerId = 0; // The Rewired player id of this character
	private Player player; // The Rewired Player
	private CharacterController cc;
	private Vector3 moveVector;
	private bool ShipSelectOneShot = true;
	public GameObject Menu;
	public GameObject P1prefab;
	public Player1Controller p1c;
	public Player1Controller p2c;
	public GameObject P2prefab;
	public GameObject Splitscreen;
	public GameObject Camera;
	public ShipSelect p1s;
	public ShipSelect p2s;

	public string level;

	void Awake (){
		player = ReInput.players.GetPlayer(playerId);
		cc = GetComponent<CharacterController>();
	}
	//======================================================================================================
	//use for respawning later
	//public GameObject player1prefab;
	//public GameObject player2prefab;

	//public Vector3 P1PacmanLvlSpawnPoint; 
	//public Vector3 P2PacmanLvlSpawnPoint; 
	public ShipSelect p1;
	public ShipSelect p2;


	void Start (){
		Scene scene = SceneManager.GetActiveScene();
		//Debug.Log("Active scene is '" + scene.name + "'.");
		if (scene.name == "ShipSelect"){
			SceneManager.LoadScene("ShipSelectMenu", LoadSceneMode.Additive);}
		level = "Lvl-PacMan";
	}



	void Update(){
		if (player.GetButtonDown ("Special")) {
			ChangeLevel ();
		}
			
		if (ShipSelectOneShot && (player.GetButtonDown ("Pause"))){
				LoadLevel ();
		}
	}

	void ChangeLevel(){
		if (level == "Lvl-PacMan") {level = "Lvl-Rift";} 
		else {level = "Lvl-PacMan";}
	}

	void LoadLevel () {
		//hides menu, then loads level
		p1s.colorSelectActive = false;
		p2s.colorSelectActive = false; 

		ShipSelectOneShot = false;
		Menu.SetActive (false);
		SceneManager.UnloadSceneAsync ("ShipSelectMenu");
		if (level == "Lvl-PacMan") {
			SceneManager.LoadScene ("Lvl-PacMan", LoadSceneMode.Additive);
		}
		else if (level == "Lvl-Rift") {
			SceneManager.LoadScene ("Lvl-Rift", LoadSceneMode.Additive);
		}
		//SceneManager.LoadScene("CombatLocal1v1", LoadSceneMode.Additive);

		//tells me what was selected for spawn
		if (p1.p1selected01) {p1c.usingVector = true;}
		if (p1.p1selected02) {p1c.usingStalingrad = true;}
		if (p1.p1selected03) {p1c.usingLaGalaFighter = true;}
		if (p1.p1selected04) {p1c.usingMoonFennec = true;}
		if (p1.p1selected05) {p1c.usingHunter = true;}
		if (p1.p1selected06) {p1c.usingEvolved = true;}

		if (p2.p1selected01) {p2c.usingVector = true;}
		if (p2.p1selected02) {p2c.usingStalingrad = true;}
		if (p2.p1selected03) {p2c.usingLaGalaFighter = true;}
		if (p2.p1selected04) {p2c.usingMoonFennec = true;}
		if (p2.p1selected05) {p2c.usingHunter = true;}
		if (p2.p1selected06) {p2c.usingEvolved = true;}

		Splitscreen.SetActive (true);
		Camera.SetActive (true);
		P1prefab.SetActive (true);
		P2prefab.SetActive (true);

	}

}


