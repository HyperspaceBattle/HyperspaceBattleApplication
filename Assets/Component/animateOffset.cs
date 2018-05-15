using UnityEngine;
using System.Collections;

public class animateOffset : MonoBehaviour {
 
     public bool on = true;
 
     public float scrollSpeed;
	 public float maxTime = 99999;
	 private float offset;
	 private float rate;
     
     private Material _material;
 
     void Awake () {
     
         _material = GetComponent<Renderer>().material;

		rate = scrollSpeed * Time.deltaTime;
 
     }
 
     void Update () {
 		
         offset = Mathf.Min(maxTime, offset + rate);
		_material.mainTextureOffset = new Vector2 (0, offset);
 
     }
 }