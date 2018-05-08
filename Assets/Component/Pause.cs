using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class Pause : MonoBehaviour 
{
	[SerializeField] private GameObject pausePanel;
	//REWIRED CONTROLLER SETUP
	public int playerId = 0; // The Rewired player id of this character
	private Player player; // The Rewired Player
	void Awake (){player = ReInput.players.GetPlayer(playerId);}

	void Start()
	{
		pausePanel.SetActive(false);
	}
	void Update()
	{
		if (player.GetButton ("Pause"))
		{
			Debug.Log ("Pause Pressed");
			if (!pausePanel.activeInHierarchy) 
			{
				PauseGame();
			}
			if (pausePanel.activeInHierarchy) 
			{
				ContinueGame();   
			}
		} 
	}
	private void PauseGame()
	{
		Time.timeScale = 0;
		pausePanel.SetActive(true);
		//Disable scripts that still work while timescale is set to 0
	} 
	private void ContinueGame()
	{
		Time.timeScale = 1;
		pausePanel.SetActive(false);
		//enable the scripts again
	}
}