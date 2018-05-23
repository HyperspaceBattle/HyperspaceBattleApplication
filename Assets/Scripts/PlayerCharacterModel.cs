using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterModel : MonoBehaviour
{
    //movement variables 
    [SerializeField] private float speedNormal = .42f;
    [SerializeField] private float speedMax = 2f;
    [SerializeField] private float hyperSpeedMax = 10f;
    [SerializeField] private float hyperExhaustRate = 2f;
    [SerializeField] private float hyperRestoreRate = 1f;
    [SerializeField] private float bulletDelayTime = 10f;
    [SerializeField] private float bulletVelocity = 80f;
    [SerializeField] private int healthBars = 5;
    [SerializeField] private bool leftStickRotation = true;
    [SerializeField] private bool isLongGun = false;
    [SerializeField] private bool isShortGun = false;
    private string strHorizontalRot;
    private string strVerticalRot;
    private string strBulletType;
    private string strPlayer;
    private Color trailColor;
    private int playerID;
    private List<GameObject> guns;


    public float SpeedNormal { set { this.speedNormal = value; } get { return this.speedNormal; } }
    public float SpeedMax { set { this.speedMax = value; } get { return this.speedMax; } }
    public float HyperSpeedMax { set { this.HyperSpeedMax = value; } get { return this.hyperSpeedMax; } }
    public float HyperExhaustRate { set { this.HyperExhaustRate = value; } get { return this.hyperExhaustRate; } }
    public float HyperRestoreRate { set { this.hyperRestoreRate = value; } get { return this.hyperRestoreRate; } }
    public int HealthBars { set { this.healthBars = value; } get { return this.healthBars; } }
    public string HorizontalRotation { get { return this.strHorizontalRot; } }
    public string VerticalRotation { get { return this.strVerticalRot; } }
    public string BulletType { get { return this.strBulletType; } }
    public float BulletDelayTime { get { return this.bulletDelayTime; } }
    public float BulletVelocity { get { return this.bulletVelocity; } }
    public int PlayerID { get { return this.playerID; } }
    public string Player { get { return this.strPlayer; } }
    public Color TrailColor { get { return this.trailColor; } }
    public List<GameObject> Guns { get { return guns; } }

    void Awake()
    {
        // Determines whether to use the left or right stick to rotate the player.
        if (this.leftStickRotation)
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
            {
                this.guns.Add(child);
            }
        }
    }

    public void Init(int playerId)
    {
        this.playerID = playerId;

        // Determines the values for the Player
        switch (this.playerID)
        {
            case 0:
                this.strPlayer = "Player1";
                this.trailColor = Color.red;
                break;
            case 1:
                this.strPlayer = "Player2";
                this.trailColor = Color.blue;
                break;
                // Can add more players later.
        }

        // Determines the type of bullets the Player will fire.
        this.strBulletType = "View/Prefabs/!PlayerBulletMachineGunP" + (playerId + 1);
        if (this.isLongGun)
            this.strBulletType += "LongRange";
        else if (this.isShortGun)
            this.strBulletType += "ShortRange";
    }
}
