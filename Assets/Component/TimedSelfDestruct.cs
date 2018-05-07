using UnityEngine;
using System.Collections;

public class TimedSelfDestruct : MonoBehaviour {
	
	public float timeTilDestruct = 5.0f; 

	void OnEnable ()
	{
		Destroy( this.gameObject, timeTilDestruct );
	}
}
