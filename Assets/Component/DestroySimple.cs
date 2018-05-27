using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySimple : MonoBehaviour {

	public GameObject explosion;
	
	// Update is called once per frame
	void OnTriggerEnter(Collider other) 
	{
		Instantiate(explosion, transform.position, transform.rotation);
		Destroy (gameObject);
	}
}
