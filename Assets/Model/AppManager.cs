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

	void Awake (){
		player = ReInput.players.GetPlayer(playerId);
		cc = GetComponent<CharacterController>();
	}
	//======================================================================================================

	public GameObject player1prefab;
	public GameObject player2prefab;

	public Vector3 P1PacmanLvlSpawnPoint; 
	public Vector3 P2PacmanLvlSpawnPoint; 
//	private GameObject shipSelected;
	public GameObject CombatObjects;
//	public GameObject staligrad;
//	public GameObject moonFennec;
//	public GameObject laGalaFighter;
//	public GameObject hunter;
//	public GameObject evolved; 
	
	void Start (){
		Scene scene = SceneManager.GetActiveScene();
		//Debug.Log("Active scene is '" + scene.name + "'.");
		if (scene.name == "ShipSelect"){
			SceneManager.LoadScene("ShipSelectMenu", LoadSceneMode.Additive);}
	}


	void Update(){
		if (player.GetButtonDown ("Pause"))
		{
			GetScene ();
		}
	}
		
	void GetScene()
	{
		Scene scene = SceneManager.GetActiveScene();
		Debug.Log("Active scene is '" + scene.name + "'.");
		if (scene.name == "ShipSelect"){
			LoadLevel ();
		}
	}


	void LoadLevel () {
		//change scenes to load level function later
		SceneManager.UnloadSceneAsync ("ShipSelectMenu");
		SceneManager.LoadScene("Lvl-PacMan", LoadSceneMode.Additive);
		SceneManager.LoadScene("CombatLocal1v1", LoadSceneMode.Additive);
	}

}


