using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public Player1Controller kObject;

	void OnTriggerEnter(Collider other) 
	{
		if (other.tag == "Boundary")
		{
			return;
		}
			

		if (other.tag == "PlayerBullet")
		{
			Instantiate(explosion, transform.position, transform.rotation);
			//Destroy (other.gameObject);
			Destroy (gameObject);
		}

		if (other.tag == "Ship") {
			//kObject = other.gameObject.GetComponent<Player1Controller>();
			//kObject.hitPoints -= 10;
			//Instantiate(explosion, transform.position, transform.rotation);
		} 

	}
}
