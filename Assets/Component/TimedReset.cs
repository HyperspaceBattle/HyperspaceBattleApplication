using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;

public class TimedReset : MonoBehaviour {
	
	public int secondsToWait = 5;

	void Start (){
		StartCoroutine (Example ());
	}

	IEnumerator Example()
	{
		yield return new WaitForSeconds(secondsToWait);
		SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
	}

}
