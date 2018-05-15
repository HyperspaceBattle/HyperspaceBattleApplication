using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandScale : MonoBehaviour {
	public float speed = 1f;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		this.transform.localScale += new Vector3 (speed, speed, speed); 
	}
}
