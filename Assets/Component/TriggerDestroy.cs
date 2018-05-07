using UnityEngine;
using System.Collections;

public class TriggerDestroy : MonoBehaviour {
	public float timer = 0.0f;
	
	void OnTriggerEnter(){
		Destroy (gameObject, timer);
	}
}
