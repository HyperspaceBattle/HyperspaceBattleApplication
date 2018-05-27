using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceDown : MonoBehaviour {

	// Use this for initialization
	public float thrust;
	public Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		rb.AddForce(0, 0, Random.Range(0, thrust), ForceMode.Impulse);
	}
}
