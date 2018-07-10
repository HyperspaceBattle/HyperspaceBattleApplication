using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour
{
	public Player1Controller kObject;

    void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.tag.Equals("Ship"))
            Destroy(other.gameObject);
    }
}
