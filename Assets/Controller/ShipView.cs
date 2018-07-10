using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipView : MonoBehaviour
{
    private Ship ship;
    private GameObject chargingFX;
    private GameObject explosion;
    private Material playerColor;
    public GameObject ChargingFX { get { return chargingFX; } }

    void Awake()
    {
        // Creates the explosion GameObject.
        this.explosion = (GameObject)Resources.Load("Prefab/FXexplosion");
        
        // ChargingFx
        this.chargingFX = Instantiate((GameObject)Resources.Load("Prefab/ChargingFx"), this.gameObject.transform.position, this.gameObject.transform.rotation);
        this.chargingFX.transform.parent = this.gameObject.transform;
        this.chargingFX.SetActive(false);
    }

    public void Init(GameObject parent)
    {
        this.ship = parent.GetComponent<Ship>();
        this.gameObject.transform.parent = this.ship.transform;
                
        // Sets the meshes that display the ship
        Material[] shipMaterials = this.ship.Model.GetComponent<MeshRenderer>().sharedMaterials;
        // Connects the Ship Color Material to the Player
        this.playerColor = (Material)Resources.Load("Prefab/Materials Shared/PlayerColor" + this.ship.Model.PlayerNumber);
        // Sets the ship's color depending on the color the player selected.
        this.playerColor.SetColor("_Color", this.ship.Model.ShipColor);
        shipMaterials[shipMaterials.Length - 1] = this.playerColor;
        this.ship.Model.GetComponent<MeshRenderer>().sharedMaterials = shipMaterials;

        // Sets the trail color depending on the player
        TrailRenderer children = (TrailRenderer)this.ship.Model.GetComponentInChildren(typeof(TrailRenderer));
        if(children != null)
            children.startColor = this.ship.Model.ShipColor;
    }   
    
    public void SetBulletColor(GameObject bullet)
    {
        try
        {
            SetColor bulletColor = bullet.GetComponent<SetColor>();
            if (bulletColor != null)
                bulletColor.ColorSet(this.ship.Model.ShipColor);
            else
                Debug.LogWarning("Warning in SetBulletColor: Bullet is Null");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in SetBulletColor: " + ex.Message.ToString());
        }
    }

    void OnCollisionEnter(Collision other)
    {
        //Debug.Log("Collider: " + other.gameObject.name);
        //Player otherPlayer = other.gameObject.GetComponent<Player>();
        //if(otherPlayer != null)
        //{
        //    bool isDead = this.hpMarker.Damage();
        //    if (isDead)
        //        this.character.Controller.Explode(this.explosion);
        //}
    }


    public void Explode(GameObject explosion)
    {
        try
        {
            Time.timeScale = .1f;
            //spawn explosion
            Instantiate(explosion, this.ship.View.transform.position, this.ship.View.transform.rotation);
            this.ship.gameObject.SetActive(false);
            Time.timeScale = 1f;

            this.ship.Model.Player.ClearInputEventDelegates();
            this.ship.Model.Player = null;
            Destroy(this.ship);
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in PlayerCharacterController's Explode: " + ex.Message.ToString());
        }
    }

}
