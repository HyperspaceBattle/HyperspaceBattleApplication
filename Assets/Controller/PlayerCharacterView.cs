using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterView : MonoBehaviour
{
    private PlayerCharacter character;
    private HpMarker hpMarker;
    private GameObject chargingFX;
    private GameObject explosion;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private Material playerColor;
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
        this.character = parent.GetComponent<PlayerCharacter>();
        this.character.Model.transform.parent = this.gameObject.transform;

        // Sets the meshes that display the ship
        Material[] shipMaterials = this.character.Model.GetComponent<MeshRenderer>().sharedMaterials;

        // Sets the ship's color depending on the color the player selected.
        this.playerColor.SetColor("_Color", this.character.Model.ShipColor);
        shipMaterials[shipMaterials.Length - 1] = this.playerColor;
        this.character.Model.GetComponent<MeshRenderer>().sharedMaterials = shipMaterials;

        MeshCollider prefabCol = this.character.Model.GetComponent<MeshCollider>();
        this.gameObject.GetComponent<MeshCollider>().sharedMesh = prefabCol.sharedMesh;

        // Creates the Health Bar for the player.
        Vector3 healthBarPos = parent.transform.position - new Vector3(0f, 0.677f, 7f);
        this.hpMarker = this.healthBar.GetComponent<HpMarker>();
        this.healthBar.transform.parent = this.gameObject.transform;
        this.hpMarker.Init(this.character.Model.HealthBars, this.character.Model.PlayerID);
        
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
        //PlayerCharacter otherPlayer = other.gameObject.GetComponent<PlayerCharacter>();
        //if(otherPlayer != null)
        //{
        //    bool isDead = this.hpMarker.Damage();
        //    if (isDead)
        //        this.character.Controller.Explode(this.explosion);
        //}
    }
}
