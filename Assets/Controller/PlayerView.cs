using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private Player character;
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
        this.character = parent.GetComponent<Player>();
        this.gameObject.transform.parent = this.character.transform;
                
        // Sets the meshes that display the ship
        Material[] shipMaterials = this.character.Model.GetComponent<MeshRenderer>().sharedMaterials;
        // Connects the Ship Color Material to the Player
        this.playerColor = (Material)Resources.Load("Prefab/Materials Shared/PlayerColor" + this.character.Model.PlayerNumber);
        // Sets the ship's color depending on the color the player selected.
        this.playerColor.SetColor("_Color", this.character.Model.ShipColor);
        shipMaterials[shipMaterials.Length - 1] = this.playerColor;
        this.character.Model.GetComponent<MeshRenderer>().sharedMaterials = shipMaterials;

        // Sets the trail color depending on the player
        TrailRenderer children = (TrailRenderer)this.character.Model.GetComponentInChildren(typeof(TrailRenderer));
        if(children != null)
            children.startColor = this.character.Model.ShipColor;
    }   
    
    public void SetBulletColor(GameObject bullet)
    {
        try
        {
            SetColor bulletColor = bullet.GetComponent<SetColor>();
            if (bulletColor != null)
                bulletColor.ColorSet(this.character.Model.ShipColor);
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
}
