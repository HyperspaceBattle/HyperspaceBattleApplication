using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterView : MonoBehaviour
{
    private PlayerCharacter character;
    private HpMarker hpMarker;
    private GameObject chargingFX;
    private GameObject healthBar;
    private GameObject explosion;

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
        shipMaterials[shipMaterials.Length - 1] = (Material)Resources.Load(("View/Materials Shared/"+ this.character.Model.Player +"Color"));
        this.character.Model.GetComponent<MeshRenderer>().sharedMaterials = shipMaterials;

        MeshCollider prefabCol = this.character.Model.GetComponent<MeshCollider>();
        this.gameObject.GetComponent<MeshCollider>().sharedMesh = prefabCol.sharedMesh;

        // Creates the Health Bar for the player.
        Vector3 healthBarPos = parent.transform.position - new Vector3(0f, 0.677f, 7f);
        this.healthBar = Instantiate((GameObject)Resources.Load("Prefab/HpMarkers"), healthBarPos, parent.transform.rotation);
        this.hpMarker = this.healthBar.GetComponent<HpMarker>();
        this.healthBar.transform.parent = this.gameObject.transform;
        this.hpMarker.Init(this.character.Model.HealthBars, this.character.Model.Player);
        
        // Sets the trail color depending on the player
        TrailRenderer children = (TrailRenderer)this.character.Model.GetComponentInChildren(typeof(TrailRenderer));
        children.startColor = this.character.Model.TrailColor;
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
