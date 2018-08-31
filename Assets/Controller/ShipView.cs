using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipView : MonoBehaviour
{
    private Ship ship;
    private GameObject chargingFX;
    //private Material playerColor;
    private bool isDamaged;
    private float gameTime;

    //[Range(0.0F, 100.0F)] public float RecoverRate = 5f;

    public GameObject ChargingFX { get { return chargingFX; } }

    void Awake()
    {
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
        foreach (Material material in this.GetComponent<MeshRenderer>().materials)
        {
            if (material.name.Contains("PlayerColor"))
                material.color = this.ship.Model.ShipColor;
        }
        
        // Sets the Reticule color if it exists.
        if (this.ship.Model.Reticule != null)
            this.ship.Model.Reticule.GetComponent<MeshRenderer>().material.SetColor("_TintColor", this.ship.Model.ShipColor);
            
        // Sets the trail color depending on the player
        TrailRenderer children = (TrailRenderer)this.ship.Model.GetComponentInChildren(typeof(TrailRenderer));
        if (children != null)
            children.startColor = this.ship.Model.ShipColor;
    }

    void FixedUpdate()
    {
        try
        {
            this.gameTime += Time.deltaTime;
            if (this.isDamaged && this.gameTime >= this.ship.Model.InvincibleTimer)
            {
                this.isDamaged = false;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in ShipView's FixedUpdate: " + ex.Message.ToString());
        }
    }

    void OnCollisionEnter(Collision other)
    {
        try
        {
            // Handles damaging a ship.
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                this.Damage(bullet.parentNum);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in ShipView's OnCollisionEnter: " + ex.Message.ToString());
        }
    }

    private void Damage(int opponentNum)
    {
        try
        {
            if (this.ship.Model.PlayerNumber != opponentNum && !this.isDamaged)
            {
                this.isDamaged = true;
                this.gameTime = 0f;
                bool isDead = this.ship.Model.Health.Damage();
                if (isDead)
                    Explode(opponentNum);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in ShipView's Damage: " + ex.Message.ToString());
        }
    }


    private void Explode(int opponentID)
    {
        try
        {
            AppManager.Victor = opponentID;
            GameObject explosion = Instantiate((GameObject)Resources.Load("Prefab/FXexplosion"), this.ship.transform.position, this.ship.transform.rotation);
            this.ship.gameObject.SetActive(false);
            Invoke("NextScene", explosion.GetComponent<TimedSelfDestruct>().timeTilDestruct + 0.5f);
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in ShipView's Explode: " + ex.Message.ToString());
        }
    }

    private void NextScene()
    {
        try
        {
            this.ship.Model.Player.ClearInputEventDelegates();
            this.ship.Model.Player = null;
            SceneManager.LoadScene("Resources/Scenes/Victory", LoadSceneMode.Single);
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in ShipView's NextScene: " + ex.Message.ToString());
        }
    }
}
