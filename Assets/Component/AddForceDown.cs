using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceDown : MonoBehaviour
{
	// Use this for initialization
	public float thrust;
    
    void FixedUpdate()
    {
        if(AppManager.IsUnpaused)
            transform.position += gameObject.transform.forward * Time.deltaTime * -(thrust);
    }
}
