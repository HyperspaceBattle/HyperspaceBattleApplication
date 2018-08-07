using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipView : MonoBehaviour
{
    private Ship ship;
    private GameObject chargingFX;
    private GameObject explosion;
    private Material playerColor;
    private bool isDamaged;
    private float gameTime;

    [Range(0.0F, 100.0F)] public float RecoverRate = 5f;

    public GameObject ChargingFX { get { return chargingFX; } }

    void Awake()
    {
        // Creates the explosion GameObject.
        this.explosion = (GameObject)Resources.Load("Prefab/FXexplosion");

        // ChargingFx
        this.chargingFX = Instantiate((GameObject)Resources.Load("Prefab/ChargingFx"), this.gameObject.transform.position, this.gameObject.transform.rotation);
        this.chargingFX.transform.parent = this.gameObject.transform;
        this.chargingFX.SetActive(false);

        this.isDamaged = false;
        this.gameTime = 0f;
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
        if (children != null)
            children.startColor = this.ship.Model.ShipColor;
    }

    void FixedUpdate()
    {
        try
        {
            this.gameTime += Time.deltaTime * this.RecoverRate;
            if (this.isDamaged && this.gameTime >= this.ship.Model.InvincibleTimer)
                this.isDamaged = false;
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in ShipView's FixedUpdate: " + ex.Message.ToString());
        }
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
            Debug.LogError("Error in ShipView's SetBulletColor: " + ex.Message.ToString());
        }
    }

    public void Damage(int opponentNum)
    {
        try
        {
            if (this.ship.Model.PlayerNumber != opponentNum && !this.isDamaged)
            {
                bool isDead = this.ship.Model.Health.Damage();
                if (isDead)
                    Explode(opponentNum);
                this.isDamaged = true;
                this.gameTime = 0f;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in ShipView's Damage: " + ex.Message.ToString());
        }
    }


    public void Explode(int opponentID)
    {
        try
        {
            Time.timeScale = .1f;
            AppManager.Victor = opponentID;
            //spawn explosion
            Instantiate(this.explosion, this.ship.transform.position, this.ship.transform.rotation);
            this.ship.gameObject.SetActive(false);
            Time.timeScale = 1f;

            this.ship.Model.Player.ClearInputEventDelegates();
            this.ship.Model.Player = null;
            //Destroy(this.ship);
            SceneManager.LoadScene("Resources/Scenes/Victory", LoadSceneMode.Additive);
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in ShipView's Explode: " + ex.Message.ToString());
        }
    }

}
