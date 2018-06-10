using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class ShipSelect : MonoBehaviour {
	//REWIRED CONTROLLER SUPPORT
	//================================================================
	public int playerId = 0;
    private Player player;
	void Awake ()
    {
		player = ReInput.players.GetPlayer(playerId);
    }
    //======================================================================================================

    private int ShipIndex = -1;
    [SerializeField] private GameObject Ships;
    private List<GameObject> ShipList;

    [SerializeField] private GameObject ShipLogos;
    private List<GameObject> ShipLogoList;

    //[SerializeField] private GameObject Levels;
    //private List<GameObject> LevelList;

    private Material playerColor;
    private Color[] ShipColors = { Color.red , Color.blue, Color.magenta, Color.yellow, Color.cyan, Color.green };
    private int ColorIndex = -1;

	// Use this for initialization
	void Start ()
    {
        this.ShipList = this.FillList(this.Ships);
        this.ShipLogoList = this.FillList(this.ShipLogos);
        //this.LevelList = this.FillList(this.Levels);

        switch (this.playerId)
        {
            case 0:
                this.ShipIndex = 0;
                break;
            case 1:
                this.ShipIndex = this.ShipLogoList.Count - 1;
                break;
        }

        this.SelectShip(this.ShipIndex, 0);
        // Sets the color for the player
        this.ColorIndex = this.playerId;
        //playerColor.SetColor("_Color", this.ShipColors[this.ColorIndex]);
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Handles Selection
        int previousIndex = this.ShipIndex;
        if (player.GetButtonDown ("LS Move Horizontal"))
        {
            this.ShipIndex = (this.ShipIndex + 1) % this.ShipList.Count;
		}	
		if (player.GetNegativeButtonDown ("LS Move Horizontal"))
        {
            this.ShipIndex = (this.ShipIndex - 1) % this.ShipList.Count;
            if(this.ShipIndex < 0)
                this.ShipIndex += this.ShipList.Count;
        }

        if (this.ShipIndex != previousIndex)
            this.SelectShip(this.ShipIndex, previousIndex);

        if ( player.GetButtonDown("hyperspeed"))
        {
            this.ColorIndex = (this.ColorIndex + 1) % this.ShipColors.Length;
            // CHECK IF OTHER PLAYER(S) ARE USING THIS COLOR
            //playerColor.SetColor("_Color", this.ShipColors[this.ColorIndex]);
        }

    }
    //PLAYER 1 SELECTION
    
    private List<GameObject> FillList(GameObject parent)
    {
        List<GameObject> list = new List<GameObject>();
        for (int index = 0; index < parent.transform.childCount; index++)
            list.Add(parent.transform.GetChild(index).gameObject);
        
        return list;
    }

    private void SelectShip(int shipIndex, int prevShipIndex)
    {
        // Deactivates the previously selected ship and activates the newly selected ship
        this.ShipList[prevShipIndex].SetActive(false);
        this.ShipList[shipIndex].SetActive(true);

        // Place the Player Marker over the Ship Logo
        GameObject shipLogo = this.ShipLogoList[shipIndex];
        this.gameObject.transform.position = new Vector3(shipLogo.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
    }
}
