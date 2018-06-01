using System.Collections;
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

	public GameObject p1Spawn;
	public GameObject p2Spawn;

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

	public string level;

	public GameObject LevelCard01;
	public GameObject LevelCard02;
	public GameObject LevelCard03;
	public GameObject LevelCard04;
	public GameObject LevelCard05;
	public GameObject LevelCard06;

	public GameObject FightCard;
	void Start (){
		//checks to see if on ship select screen if so load menu and set default level 
		Scene scene = SceneManager.GetActiveScene();
		//Debug.Log("Active scene is '" + scene.name + "'.");
		if (scene.name == "ShipSelect"){
			SceneManager.LoadScene("ShipSelectMenu", LoadSceneMode.Additive);}
		level = "Lvl-Vector Debris Cluster";
		FightCard.SetActive (false);
	}



	void Update(){
		if (player.GetButtonDown ("Special")) {
			ChangeLevel ();
		}
			
		if (ShipSelectOneShot && (player.GetButtonDown ("Pause"))){
			FightCard.SetActive (true);
				LoadLevel ();
		}

		ShowLevelCard ();
			
	}

	void ChangeLevel(){
		if (level == "Lvl-Vector Debris Cluster") {level = "Lvl-LaGalas Simulation";} 
		else if (level == "Lvl-LaGalas Simulation") {level = "Lvl-Evolved Geomis";}
		else if (level == "Lvl-Evolved Geomis") {level = "Lvl-MoonFennec SectorQ";}
		else if (level == "Lvl-MoonFennec SectorQ") {level = "Lvl-Rift";}
		else if (level == "Lvl-Rift") {level = "Lvl-PacMan";}
		else {level = "Lvl-Vector Debris Cluster";}
	}

	void LoadLevel () {
		
		//hides menu
		p1.colorSelectActive = false;
		p2.colorSelectActive = false; 

		ShipSelectOneShot = false;
		Menu.SetActive (false);

		// checks and loads level
		SceneManager.UnloadSceneAsync ("ShipSelectMenu");
		if (level == "Lvl-Vector Debris Cluster") {SceneManager.LoadScene ("Lvl-Vector Debris Cluster", LoadSceneMode.Additive);} 
		else if (level == "Lvl-LaGalas Simulation") {SceneManager.LoadScene ("Lvl-LaGalas Simulation", LoadSceneMode.Additive);}
		else if (level == "Lvl-Evolved Geomis") {SceneManager.LoadScene ("Lvl-Evolved Geomis", LoadSceneMode.Additive);}
		else if (level == "Lvl-MoonFennec SectorQ") {SceneManager.LoadScene ("Lvl-MoonFennec SectorQ", LoadSceneMode.Additive);}			else if (level == "Lvl-Rift") {SceneManager.LoadScene ("Lvl-Rift", LoadSceneMode.Additive);}
		else if (level == "Lvl-PacMan") {SceneManager.LoadScene ("Lvl-PacMan", LoadSceneMode.Additive);}

		//tells me what was selected for spawn
		if (p1.p1selected01) {p1c.usingVector = true;}
		if (p1.p1selected02) {p1c.usingStalingrad = true;}
		if (p1.p1selected03) {p1c.usingLaGalaFighter = true;}
		if (p1.p1selected04) {p1c.usingMoonFennec = true;}
		if (p1.p1selected05) {p1c.usingHunter = true;}
		if (p1.p1selected06) {p1c.usingEvolved = true;}

		//spawns players
		if (p2.p1selected01) {p2c.usingVector = true;}
		if (p2.p1selected02) {p2c.usingStalingrad = true;}
		if (p2.p1selected03) {p2c.usingLaGalaFighter = true;}
		if (p2.p1selected04) {p2c.usingMoonFennec = true;}
		if (p2.p1selected05) {p2c.usingHunter = true;}
		if (p2.p1selected06) {p2c.usingEvolved = true;}

		//sets game up, spawn player, spawn effect 
		Splitscreen.SetActive (true);
		Camera.SetActive (true);
		P1prefab.SetActive (true);
		P2prefab.SetActive (true);
		Instantiate(p1Spawn,P1prefab.transform.position, P1prefab.transform.rotation);
		Instantiate(p2Spawn,P2prefab.transform.position, P2prefab.transform.rotation);

	}
		
	void ShowLevelCard (){
		if (level == "Lvl-Vector Debris Cluster") {
			LevelCard01.SetActive (false);
			LevelCard02.SetActive (false);
			LevelCard03.SetActive (false);
			LevelCard04.SetActive (false);
			LevelCard05.SetActive (false);
			LevelCard06.SetActive (true);
		} 
		
		else if (level == "Lvl-LaGalas Simulation") {
			
			LevelCard01.SetActive (false);
			LevelCard02.SetActive (false);
			LevelCard03.SetActive (false);
			LevelCard04.SetActive (false);
			LevelCard05.SetActive (false);
			LevelCard06.SetActive (false);
			LevelCard01.SetActive (true);
		}
		
		else if (level == "Lvl-Evolved Geomis") {
			LevelCard01.SetActive (false);
			LevelCard03.SetActive (false);
			LevelCard04.SetActive (false);
			LevelCard05.SetActive (false);
			LevelCard06.SetActive (false);
			LevelCard02.SetActive (true);
		}
		
		else if (level == "Lvl-MoonFennec SectorQ") {
			LevelCard01.SetActive (false);
			LevelCard02.SetActive (false);
			LevelCard04.SetActive (false);
			LevelCard05.SetActive (false);
			LevelCard06.SetActive (false);
			LevelCard03.SetActive (true);
		}	
		
		else if (level == "Lvl-Rift") {
			LevelCard01.SetActive (false);
			LevelCard02.SetActive (false);
			LevelCard03.SetActive (false);
			LevelCard04.SetActive (false);
			LevelCard06.SetActive (false);
			LevelCard05.SetActive (true);
		}
		
		else if (level == "Lvl-PacMan") {
			LevelCard01.SetActive (false);
			LevelCard02.SetActive (false);
			LevelCard03.SetActive (false);
			LevelCard05.SetActive (false);
			LevelCard06.SetActive (false);
			LevelCard04.SetActive (true);
		}
	}
}


