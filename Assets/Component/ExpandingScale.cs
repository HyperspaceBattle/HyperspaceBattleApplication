using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandingScale : MonoBehaviour {

	public bool scaleY = false;
	public float minimumScale = 1f;
	public float maximumScale = 10f;
	public float speed = 2.5f;

	void Start (){
		transform.localScale = new Vector3(minimumScale,0,minimumScale);
	}
	void Update () {
		if (scaleY) {
			transform.localScale += new Vector3 (speed, speed, speed);
		} 

		else {
			transform.localScale += new Vector3 (speed, 0f, speed);
		}

		if ( transform.localScale.x >= maximumScale){
			transform.localScale = new Vector3(minimumScale,1,minimumScale);
		}
	}
}
