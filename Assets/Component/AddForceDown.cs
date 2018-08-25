using System;
using UnityEngine;

public class AddForceDown : MonoBehaviour
{
	// Use this for initialization
	public float thrust;
    
    void FixedUpdate()
    {
        try
        { 
            if(AppManager.IsUnpaused)
                transform.position += gameObject.transform.forward * Time.deltaTime * -(thrust);
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in AddForceDown's FixedUpdate: " + ex.Message.ToString());
        }
    }
}
