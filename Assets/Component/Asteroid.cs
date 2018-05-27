using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

	public GameObject explosion;
	public GameObject miniAsteroi01;
	public GameObject miniAsteroi02;
	public GameObject miniAsteroi03;
	public float numMini;
	public float thrust;
	public Rigidbody rb;

	void Awake(){
		numMini = Random.Range (0f, 3f);
		rb = GetComponent<Rigidbody>();
		rb.AddForce(Random.Range(-thrust, thrust), 0, Random.Range(-thrust, thrust), ForceMode.Impulse);

	}
	// Update is called once per frame
	void OnTriggerEnter(Collider other) 
	{
		if (numMini < 1) { 
			Instantiate (miniAsteroi01, transform.position, transform.rotation);
		}
		else if ( numMini > 1 && numMini < 2) { 
			Instantiate (miniAsteroi01, transform.position, transform.rotation);
			Instantiate (miniAsteroi02, transform.position, transform.rotation);
		}
		else if (numMini >= 2) { 
			Instantiate (miniAsteroi01, transform.position, transform.rotation);
			Instantiate (miniAsteroi02, transform.position, transform.rotation);
			Instantiate (miniAsteroi03, transform.position, transform.rotation);
		}

		Instantiate(explosion, transform.position, transform.rotation);
		Destroy (gameObject);
	}
}
