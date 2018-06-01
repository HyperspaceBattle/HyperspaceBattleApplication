using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killzone : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter(Collider other){
		//if (other.tag == "enemyMinor") {

		//explosion.GetComponent<AudioSource> ().Play ();
		Destroy (gameObject);
		//}
	}
}
