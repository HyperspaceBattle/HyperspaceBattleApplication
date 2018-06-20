using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public PlayerCharacterModel Model;
    public PlayerCharacterView View;
    public PlayerCharacterController Controller;

    private GameObject ship;
    public int playerID;

    void Start()
    {
        this.ship = GameObject.Instantiate((GameObject)Resources.Load(AppManager.GetPlayerShip(this.playerID)), this.gameObject.transform.position, this.gameObject.transform.rotation);

        this.Model = this.ship.GetComponent<PlayerCharacterModel>();
        this.Model.Init(playerID);
        this.View.Init(this.gameObject);
        this.Controller.Init(this.gameObject);
    }
}
