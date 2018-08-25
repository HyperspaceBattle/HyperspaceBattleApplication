using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;
using System;

public class ShipSelect : MonoBehaviour
{
	public int playerId = 0;
    private Rewired.Player player;

    private int ShipIndex = -1;
    [SerializeField] private GameObject Ships;
    private List<GameObject> ShipList;

    [SerializeField] private GameObject ShipLogos;
    private List<GameObject> ShipLogoList;

    [SerializeField] private GameObject Levels;
    private List<GameObject> LevelList;

    public Material playerColor;
    public Color[,] ShipColors = new Color[,] { 
        {
            new Color(255,0,0,255), 
            new Color(255,65,29,255), 
            new Color(255,136,25,255),
            new Color(255,183,0,255),
            new Color(255,255,0,255),
            new Color(255,220,220,255),
            new Color(255,204,102,255),
            new Color(236,151,135,255),
            new Color(144,76,33,255),
            new Color(168,52,46,255),
            new Color(131,0,128,255),
            new Color(72,0,0,255),
            new Color(51,28,7,255),
            new Color(22,0,140,255)
        }, 
        {
            new Color(0,0,255,255),
            new Color(0,131,213,255),
            new Color(0,141,144,255),
            new Color(0,185,0,255),
            new Color(133,255,12,255),
            new Color(202,220,255,255),
            new Color(211,255,165,255),
            new Color(108,160,220,255),
            new Color(55,65,44,255),
            new Color(37,12,42,255),
            new Color(66,0,193,255),
            new Color(0,0,72,255),
            new Color(0,30,30,255),
            new Color(0,255,255,255)
        }
    };
    private int ColorIndex = -1;

    private Color P1SELECTCOLOR = new Color(0.3f, 0, 0, 1f);
    private Color P1UNSELECTCOLOR = new Color(1f, 0, 0, 1f);
    private Color P2SELECTCOLOR = new Color(0, 0.08965492f, 0.3f, 1f);
    private Color P2UNSELECTCOLOR = new Color(0, 0.08965492f, 1f, 1f);
    private Color shipColor;
    
    // Use this for initialization
    void Start ()
    {
        try
        {
            AppManager.Reset();
            this.player = ReInput.players.GetPlayer(playerId);
            this.player.AddInputEventDelegate(MoveCursor, UpdateLoopType.Update, InputActionEventType.NegativeButtonJustPressed, "LS Move Horizontal");
            this.player.AddInputEventDelegate(MoveCursor, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "LS Move Horizontal");
            this.player.AddInputEventDelegate(ColorChange, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "HyperSpeed");
            this.player.AddInputEventDelegate(LevelChange, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Special");
            this.player.AddInputEventDelegate(SelectedShip, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Pause");


            this.ShipList = this.FillList(this.Ships);
            this.ShipLogoList = this.FillList(this.ShipLogos);
            this.LevelList = this.FillList(this.Levels);

            switch (this.playerId)
            {
                case 0:
                    this.ShipIndex = 0;
                    break;
                case 1:
                    this.ShipIndex = this.ShipLogoList.Count - 2;
                    break;
            }

            this.SelectShip(this.ShipIndex, 0);
            // Sets the color for the player
            this.ColorIndex = 0;
            this.shipColor = this.ShipColors[this.playerId, this.ColorIndex];
            this.playerColor.SetColor("_Color", this.shipColor);

            this.LevelList[AppManager.LevelIndex].SetActive(true);
            AppManager.SetIsPlayerReady(this.playerId, false);
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in ShipSelect's Start, Player " + playerId + ": " + ex.Message.ToString());
        }
    }

    void FixedUpdate()
    {
        if (player.GetAnyButtonDown())
            AppManager.GameTimer = 0f;
    }

    void MoveCursor(InputActionEventData data)
    {
        try
        {
            if (!AppManager.GetIsPlayerReady(this.playerId))
            {
                int previousIndex = this.ShipIndex;
                if (data.GetAxis() >= 0)
                {
                    this.ShipIndex = (this.ShipIndex + 1) % this.ShipList.Count;
                }
                else
                {
                    this.ShipIndex = (this.ShipIndex - 1) % this.ShipList.Count;
                    if (this.ShipIndex < 0)
                        this.ShipIndex += this.ShipList.Count;
                }
                this.SelectShip(this.ShipIndex, previousIndex);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in ShipSelect's MoveCursor Start, Player " + playerId + ": " + ex.Message.ToString());
        }
    }

    private List<GameObject> FillList(GameObject parent)
    {
        List<GameObject> list = new List<GameObject>();
        try
        {
            for (int index = 0; index < parent.transform.childCount; index++)
                list.Add(parent.transform.GetChild(index).gameObject);
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in ShipSelect's FillList, Player " + playerId + ": " + ex.Message.ToString());
        }
        return list;
    }

    private void SelectShip(int shipIndex, int prevShipIndex)
    {
        try
        {
            // Deactivates the previously selected ship and activates the newly selected ship
            this.ShipList[prevShipIndex].SetActive(false);
            this.ShipList[shipIndex].SetActive(true);

            // Place the Player Marker over the Ship Logo
            GameObject shipLogo = this.ShipLogoList[shipIndex];
            this.gameObject.transform.position = new Vector3(shipLogo.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in SelectShip, Player " + playerId + ": " + ex.Message.ToString());
        }
    }

    void ColorChange(InputActionEventData data)
    {
        try
        {
            if (!AppManager.GetIsPlayerReady(this.playerId))
            {
                this.ColorIndex = (this.ColorIndex + 1) % this.ShipColors.GetLength(1);
                this.shipColor = this.ShipColors[this.playerId, this.ColorIndex];
                this.playerColor.SetColor("_Color", this.ShipColors[this.playerId, this.ColorIndex]);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in ShipSelect's ColorChange, Player " + playerId + ": " + ex.Message.ToString());
        }    
    }

    void LevelChange(InputActionEventData data)
    {
        try
        {
            int prevLevel = AppManager.LevelIndex;
            AppManager.LevelIndex = (1 + AppManager.LevelIndex) % this.LevelList.Count;
            this.LevelList[prevLevel].SetActive(false);
            this.LevelList[AppManager.LevelIndex].SetActive(true);
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in ShipSelect's LevelChange, Player " + playerId + ": " + ex.Message.ToString());
        }
    }

    void SelectedShip(InputActionEventData data)
    {
        try
        {
            Color selectColor = Color.white;
            Color unselectColor = Color.black;
            switch (this.playerId)
            {
                case 0:
                    selectColor = P1SELECTCOLOR;
                    unselectColor = P1UNSELECTCOLOR;
                    break;
                case 1:
                    selectColor = P2SELECTCOLOR;
                    unselectColor = P2UNSELECTCOLOR;
                    break;
            }
            Color newColor = (AppManager.GetIsPlayerReady(this.playerId)) ? unselectColor : selectColor;
            this.gameObject.GetComponent<Renderer>().materials[0].SetColor("_TintColor", newColor);
            
            if (this.ShipList[this.ShipIndex].name.Equals("Random"))
            {
                System.Random rand = new System.Random((int)DateTime.Now.Ticks);
                int randomIndex = Mathf.Abs(rand.Next() % this.ShipList.Count - 1);
                AppManager.SetPlayerShip(this.playerId, this.ShipList[randomIndex].name);
            }
            else
                AppManager.SetPlayerShip(this.playerId, this.ShipList[this.ShipIndex].name);

            AppManager.SetPlayerColor(this.playerId, this.shipColor);
            AppManager.SetIsPlayerReady(this.playerId, !AppManager.GetIsPlayerReady(this.playerId));
            if (AppManager.PlayersReady)
                this.LoadLevel();
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in ShipSelect's SelectedShip, Player " + playerId + ": " + ex.Message.ToString());
        }
    }

    void LoadLevel()
    {
        try
        {
            this.player.ClearInputEventDelegates();

            // Loads the selected scene
            SceneManager.LoadScene(this.LevelList[AppManager.LevelIndex].name);
            Destroy(this.gameObject);
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in ShipSelect's LoadLevel, Player " + playerId + ": " + ex.Message.ToString());
        }
    }    
}
