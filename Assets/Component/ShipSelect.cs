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
	// public bool 			p2selected01;
	public GameObject 		selection01;
	public GameObject 		Model01;

	public bool 			p1selected02;
	// public bool 			p2selected02;
	public GameObject 		selection02;
	public GameObject 		Model02;

	public bool 			p1selected03;
	// public bool 			p2selected03;
	public GameObject 		selection03;
	public GameObject 		Model03L;


	public bool 			p1selected04;
	// public bool 			p2selected04;
	public GameObject 		selection04;
	public GameObject 		Model04;

	public bool 			p1selected05;
	// public bool 			p2selected05;
	public GameObject 		selection05;
	public GameObject 		Model05;

	public bool 			p1selected06;
	// public bool 			p2selected06;
	public GameObject 		selection06;
	public GameObject 		Model06;

	// Use this for initialization
	void Start () {
		P1selected01();
		//P2selected06 ();
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
	// PLAYER 2 SELCTION

		// void P2selected01() {
			// p2marker.transform.position = new Vector3(selection01.transform.position.x, p2marker.transform.position.y, p2marker.transform.position.z );
			// p2selected01 = true; p2selected06 = false; p2selected02 = false;
		// }

		// void P2selected02() {
			// p2marker.transform.position = new Vector3(selection02.transform.position.x, p2marker.transform.position.y, p2marker.transform.position.z );
			// p2selected02 = true; p2selected01 = false; p2selected03 = false;
		// }

		// void P2selected03() {
			// p2marker.transform.position = new Vector3(selection03.transform.position.x, p2marker.transform.position.y, p2marker.transform.position.z );
			// p2selected03 = true; p2selected02 = false; p2selected04 = false;
		// }

		// void P2selected04() {
			// p2marker.transform.position = new Vector3(selection04.transform.position.x, p2marker.transform.position.y, p2marker.transform.position.z );
			// p2selected04 = true; p2selected03 = false; p2selected05 = false;
		// }

		// void P2selected05() {
			// p2marker.transform.position = new Vector3(selection05.transform.position.x, p2marker.transform.position.y, p2marker.transform.position.z );
			// p2selected05 = true; p2selected04 = false; p2selected06 = false;
		// }

		// void P2selected06() {
			// p2marker.transform.position = new Vector3(selection06.transform.position.x, p2marker.transform.position.y, p2marker.transform.position.z );
			// p2selected06 = true; p2selected05 = false; p2selected01 = false;
		// }  

		void BroadcastSelection(){
			
		}
}
