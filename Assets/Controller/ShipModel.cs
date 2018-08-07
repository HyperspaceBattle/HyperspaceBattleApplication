﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class ShipModel : MonoBehaviour
{
    //movement variables 
    [SerializeField] private float speedNormal = .42f;
    [SerializeField] private float speedMax = 2f;
    [SerializeField] private float hyperSpeedMax = 10f;
    [SerializeField] private float hyperExhaustRate = 2f;
    [SerializeField] private float hyperRestoreRate = 1f;
    [SerializeField] private float bulletDelayTime = 10f;
    [SerializeField] private float bulletVelocity = 80f;
    [SerializeField] private float invincibleTimer = 5f;
    [SerializeField] private int healthBars = 5;
    [SerializeField] private bool leftStickRotation = true;
    private string strHorizontalRot;
    private string strVerticalRot;
    private Color shipColor;
    private int playerID;
    private List<GameObject> guns;
    private HpMarker health;
    private Player player;

    public float SpeedNormal { set { this.speedNormal = value; } get { return this.speedNormal; } }
    public float SpeedMax { set { this.speedMax = value; } get { return this.speedMax; } }
    public float HyperSpeedMax { set { this.HyperSpeedMax = value; } get { return this.hyperSpeedMax; } }
    public float HyperExhaustRate { set { this.HyperExhaustRate = value; } get { return this.hyperExhaustRate; } }
    public float HyperRestoreRate { set { this.hyperRestoreRate = value; } get { return this.hyperRestoreRate; } }
    public int HealthBars { set { this.healthBars = value; } get { return this.healthBars; } }
    public Player Player { set { this.player = value; } get { return this.player; } }

    public string HorizontalRotation { get { return this.strHorizontalRot; } }
    public string VerticalRotation { get { return this.strVerticalRot; } }
    public float BulletDelayTime { get { return this.bulletDelayTime; } }
    public float BulletVelocity { get { return this.bulletVelocity; } }
    public float InvincibleTimer { get { return this.invincibleTimer; } }
    public int PlayerID { get { return this.playerID; } }
    public int PlayerNumber { get { return this.playerID + 1; } }
    public Color ShipColor { get { return this.shipColor; } }
    public List<GameObject> Guns { get { return this.guns; } }
    public HpMarker Health { get { return this.health; } }


    [System.Flags]
    public enum BulletType { Standard, Standard_Long_Range, Standard_Short_Range, Machine_Gun, Machine_Dual, Spreader, Pellet_Sm, Galaga_Sniper, Lasar }
    [SerializeField] private BulletType bullet;
    public string Bullet { get { return "Prefab/Bullets/" + this.bullet.ToString(); } }


    void Awake()
    {
        // Determines whether to use the left or right stick to rotate the player.
        if (leftStickRotation)
        {
            this.strHorizontalRot = "LS Move Horizontal";
            this.strVerticalRot = "LS Move Vertical";
        }
        else
        {
            this.strHorizontalRot = "RS Fire Horizontal";
            this.strVerticalRot = "RS Fire Vertical";
        }

        this.guns = new List<GameObject>();
        int childCount = this.gameObject.transform.childCount;
        for (int index = 0; index < childCount; index++)
        {
            GameObject child = this.gameObject.transform.GetChild(index).gameObject;
            if (child.name.Contains("StdGun"))
                this.guns.Add(child);
            else if (child.name.Contains("HpMarkers"))
                this.health = child.GetComponent<HpMarker>();
        }
    }

    public void Init(int playerId)
    {
        this.playerID = playerId;
        this.shipColor = AppManager.GetPlayerColor(playerID);
        this.health.Init(this.PlayerNumber, this.healthBars, this.shipColor);
    }
}
