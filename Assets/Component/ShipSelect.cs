using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class ShipSelect : MonoBehaviour {
	//REWIRED CONTROLLER SUPPORT
	//================================================================
	public int playerId = 0; private Player player; private CharacterController cc;
	void Awake (){
		player = ReInput.players.GetPlayer(playerId);
		cc = GetComponent<CharacterController>();}
	//======================================================================================================

	public GameObject p1marker;

	public bool 			p1selected01;
	public GameObject 		selection01;
	public GameObject 		Model01;

	public bool 			p1selected02;
	public GameObject 		selection02;
	public GameObject 		Model02;

	public bool 			p1selected03;
	public GameObject 		selection03;
	public GameObject 		Model03L;


	public bool 			p1selected04;
	public GameObject 		selection04;
	public GameObject 		Model04;

	public bool 			p1selected05;
	public GameObject 		selection05;
	public GameObject 		Model05;

	public bool 			p1selected06;
	public GameObject 		selection06;
	public GameObject 		Model06;

	public Material p1m;
	public bool colorSelectActive = true;

	public bool red = false;
	private bool magenta = false;
	private bool yellow = false;

	public bool blue = false;
	private bool cyan = false;	
	private bool green = false;

	// Use this for initialization
	void Start () {
		P1selected01();

		if (red) {p1m.SetColor("_Color", Color.red);}
		if (blue) {p1m.SetColor("_Color", Color.blue);}
	}
	
	// Update is called once per frame
	void Update () {
			//Handles Selection
		if (player.GetButtonDown ("LS Move Horizontal")){
			
			if (p1selected01){P1selected02 ();}
			else if (p1selected02){P1selected03 ();}
			else if (p1selected03){P1selected04 ();}
			else if (p1selected04){P1selected05 ();}
			else if (p1selected05){P1selected06 ();}
			else if (p1selected06){P1selected01 ();}
		}	
		if (player.GetNegativeButtonDown ("LS Move Horizontal")){

			if (p1selected01){P1selected06 ();}
			else if (p1selected02){P1selected01 ();}
			else if (p1selected03){P1selected02 ();}
			else if (p1selected04){P1selected03 ();}
			else if (p1selected05){P1selected04 ();}
			else if (p1selected06){P1selected05 ();}
		}

		if (colorSelectActive && player.GetButtonDown ("HyperSpeed")){ 
			if 		(red) 		{p1m.SetColor("_Color", Color.magenta); magenta = true; red = false;}
			else if (magenta)	{p1m.SetColor("_Color", Color.yellow); yellow = true; magenta = false;}
			else if (yellow) 	{p1m.SetColor("_Color", Color.red); red = true; yellow = false;}

			else if (blue) 		{p1m.SetColor("_Color", Color.cyan); cyan = true; blue = false;}
			else if (cyan)		{p1m.SetColor("_Color", Color.green); green = true; cyan = false;}
			else if (green) 	{p1m.SetColor("_Color", Color.blue); blue = true; green = false;}
		}
			
}
	//PLAYER 1 SELECTION
		void P1selected01() {
			p1marker.transform.position = new Vector3(selection01.transform.position.x, p1marker.transform.position.y, p1marker.transform.position.z );
		p1selected01 = true; p1selected06 = false; p1selected02 = false;
		Model01.SetActive (true);
		Model06.SetActive (false); Model02.SetActive (false);
		}
		
		void P1selected02() {
			p1marker.transform.position = new Vector3(selection02.transform.position.x, p1marker.transform.position.y, p1marker.transform.position.z );
		p1selected02 = true; p1selected01 = false; p1selected03 = false;
				Model02.SetActive (true);
		Model01.SetActive (false); Model03L.SetActive (false); 
		}
		
		void P1selected03() {
			p1marker.transform.position = new Vector3(selection03.transform.position.x, p1marker.transform.position.y, p1marker.transform.position.z );
		p1selected03 = true; p1selected02 = false; p1selected04 = false;
		Model03L.SetActive (true); 
		Model02.SetActive (false); Model04.SetActive (false);
		}
		
		void P1selected04() {
			p1marker.transform.position = new Vector3(selection04.transform.position.x, p1marker.transform.position.y, p1marker.transform.position.z );
		p1selected04 = true; p1selected03 = false; p1selected05 = false;
				Model04.SetActive (true);
		Model03L.SetActive (false);  Model05.SetActive (false);
		}
		
		void P1selected05() {
			p1marker.transform.position = new Vector3(selection05.transform.position.x, p1marker.transform.position.y, p1marker.transform.position.z );
		p1selected05 = true; p1selected04 = false; p1selected06 = false;
				Model05.SetActive (true);
		Model04.SetActive (false); Model06.SetActive (false);
		}
		
		void P1selected06() {
		p1marker.transform.position = new Vector3(selection06.transform.position.x, p1marker.transform.position.y, p1marker.transform.position.z );
		p1selected06 = true; p1selected05 = false; p1selected01 = false;
				Model06.SetActive (true);
		Model05.SetActive (false); Model01.SetActive (false);
		}  

}
