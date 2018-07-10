using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerModel Model;
    public PlayerView View;
    public PlayerController Controller;

    private GameObject ship;
    public int playerID;

    void Start()
    {
        this.ship = GameObject.Instantiate((GameObject)Resources.Load(AppManager.GetPlayerShip(this.playerID)), this.gameObject.transform.position, this.gameObject.transform.rotation);

        // Connecting the Ship's Model.
        this.Model = this.ship.GetComponent<PlayerModel>();
        this.Model.Init(playerID);

        // Connecting the Ship's View.
        this.View = this.ship.GetComponent<PlayerView>();
        this.View.Init(this.gameObject);

        // Connecting the Ship's Controller.
        this.Controller.Init(this.gameObject);
    }
}
