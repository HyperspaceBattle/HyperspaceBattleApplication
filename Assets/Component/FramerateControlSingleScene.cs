using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramerateControlSingleScene : MonoBehaviour {

	public bool vsync = true;
	// Use this for initialization
	void Start () {
		Application.targetFrameRate = 60;
		Debug.Log( "Ready!" );
	}

	void Awake() {
		//sets game to 60fps
		if (vsync)
		{QualitySettings.vSyncCount = 1;}
		else {QualitySettings.vSyncCount = 2;}
	}
}
