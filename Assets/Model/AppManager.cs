using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppManager : MonoBehaviour {
	//APPMANAGER is a singleton
	private static AppManager instance = null; public static AppManager Instance {get { return instance; }}
	// Update is called once per frame
	void Start () {
		SceneManager.LoadScene("Lvl-PacMan", LoadSceneMode.Additive);}
	
}
