﻿using UnityEngine;
using System.Collections;

public class Initialization : MonoBehaviour {

	//created to initilze vsync and incontrol a singleton
	private static Initialization instance = null;
	public static Initialization Instance
	{
		get { return instance; }
	}

	public bool vsync = true;
	// Use this for initialization
	void Start () {
		Application.targetFrameRate = 60;
		Debug.Log( "Ready!" );
	}
	
	    void Awake() {
      	// rest of singleton code
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
		gameObject.name = "$Initialization";

		//sets game to 60fps
		if (vsync)
			 {QualitySettings.vSyncCount = 1;}
		else {QualitySettings.vSyncCount = 2;}
		}
}
