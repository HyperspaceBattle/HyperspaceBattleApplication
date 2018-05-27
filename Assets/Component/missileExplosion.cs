using UnityEngine;
using System.Collections;

public class missileExplosion : MonoBehaviour {

	public GameObject explosion;

	// Use this for initialization
	void OnTriggerEnter(Collider other){
		//if (other.tag == "enemyMinor") {

		//explosion.GetComponent<AudioSource> ().Play ();
		Destroy (gameObject);
		//}
	}

	void OnDestroy(){
		Instantiate(explosion, transform.position,transform.rotation);
	}

}
