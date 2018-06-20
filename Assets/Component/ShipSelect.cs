using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;

public class ShipSelect : MonoBehaviour
{
	public int playerId = 0;
    private Player player;

    private int ShipIndex = -1;
    [SerializeField] private GameObject Ships;
    private List<GameObject> ShipList;

    [SerializeField] private GameObject ShipLogos;
    private List<GameObject> ShipLogoList;

    [SerializeField] private GameObject Levels;
    private List<GameObject> LevelList;

    public Material playerColor;
    public Color[] ShipColors = { Color.red , Color.blue, Color.magenta, Color.yellow, Color.cyan, Color.green };
    private int ColorIndex = -1;

    private Color P1SELECTCOLOR = new Color(0.3f, 0, 0, 1f);
    private Color P1UNSELECTCOLOR = new Color(1f, 0, 0, 1f);
    private Color P2SELECTCOLOR = new Color(0, 0.08965492f, 0.3f, 1f);
    private Color P2UNSELECTCOLOR = new Color(0, 0.08965492f, 1f, 1f);

    // Use this for initialization
    void Start ()
    {
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
                this.ShipIndex = this.ShipLogoList.Count - 1;
                break;
        }

        this.SelectShip(this.ShipIndex, 0);
        // Sets the color for the player
        this.ColorIndex = this.playerId;
        AppManager.SetPlayerColor(this.playerId, this.ShipColors[this.ColorIndex]);
        this.playerColor.SetColor("_Color", AppManager.GetPlayerColor(this.playerId));

        this.LevelList[AppManager.LevelIndex].SetActive(true);
        AppManager.SetIsPlayerReady(this.playerId, false);
    }

    void MoveCursor(InputActionEventData data)
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

    private List<GameObject> FillList(GameObject parent)
    {
        List<GameObject> list = new List<GameObject>();
        for (int index = 0; index < parent.transform.childCount; index++)
            list.Add(parent.transform.GetChild(index).gameObject);        
        return list;
    }

    private void SelectShip(int shipIndex, int prevShipIndex)
    {
        // Deactivates the previously selected ship and activates the newly selected ship
        this.ShipList[prevShipIndex].SetActive(false);
        this.ShipList[shipIndex].SetActive(true);

        // Place the Player Marker over the Ship Logo
        GameObject shipLogo = this.ShipLogoList[shipIndex];
        this.gameObject.transform.position = new Vector3(shipLogo.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
    }

    void ColorChange(InputActionEventData data)
    {
        if (!AppManager.GetIsPlayerReady(this.playerId))
        {
            Color currentColor = this.ShipColors[this.ColorIndex];
            while (currentColor == AppManager.GetPlayerColor(this.playerId))
            {
                this.ColorIndex = (this.ColorIndex + 1) % this.ShipColors.Length;
                Color newColor = this.ShipColors[this.ColorIndex];
                if (AppManager.AvaliableColor(this.playerId, newColor))
                {
                    AppManager.SetPlayerColor(this.playerId, newColor);
                    playerColor.SetColor("_Color", newColor);
                }
            }
        }        
    }

    void LevelChange(InputActionEventData data)
    {
        int prevLevel = AppManager.LevelIndex;
        AppManager.LevelIndex = (1 + AppManager.LevelIndex) % this.LevelList.Count;
        this.LevelList[prevLevel].SetActive(false);
        this.LevelList[AppManager.LevelIndex].SetActive(true);
    }

    void SelectedShip(InputActionEventData data)
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
        AppManager.SetIsPlayerReady(this.playerId, !AppManager.GetIsPlayerReady(this.playerId));
        AppManager.SetPlayerShip(this.playerId, this.ShipList[this.ShipIndex].name);
        if (AppManager.PlayersReady)
            this.LoadLevel();
    }

    void LoadLevel()
    {
        this.player.RemoveInputEventDelegate(MoveCursor);
        this.player.RemoveInputEventDelegate(ColorChange);
        this.player.RemoveInputEventDelegate(LevelChange);
        this.player.RemoveInputEventDelegate(SelectedShip);        

        // Loads the selected scene
        SceneManager.LoadScene(this.LevelList[AppManager.LevelIndex].name);
    }
}
