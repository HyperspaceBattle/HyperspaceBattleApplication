using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {
	public Player1Controller kObject;

    void OnTriggerExit(Collider other)
    {
		if (other.tag == "player01" || other.tag == "player02") {
			
			kObject = other.gameObject.GetComponent<Player1Controller>();
			kObject.hitPoints -=1;
		} 
		
        else{Destroy(other.gameObject);}
    }
}
