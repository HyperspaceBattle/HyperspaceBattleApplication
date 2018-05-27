using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRandomForce : MonoBehaviour {

    public float thrust;
    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
		rb.AddForce(Random.Range(-thrust, thrust), 0, Random.Range(-thrust, thrust), ForceMode.Impulse);
    }
		
}
