using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public ShipModel Model;
    public ShipView View;
    public ShipController Controller;

    private GameObject ship;
    public int playerID;

    void Start()
    {
        GameObject goShip = (GameObject)Resources.Load(AppManager.GetPlayerShip(this.playerID));
        this.ship = Instantiate(goShip, this.gameObject.transform.position, goShip.transform.rotation);

        // Connecting the Ship's Model.
        this.Model = this.ship.GetComponent<ShipModel>();
        this.Model.Init(playerID);

        // Connecting the Ship's View.
        this.View = this.ship.GetComponent<ShipView>();
        this.View.Init(this.gameObject);

        // Connecting the Ship's Controller.
        this.Controller.Init(this.gameObject);
    }
}
