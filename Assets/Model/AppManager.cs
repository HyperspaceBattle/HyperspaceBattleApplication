using UnityEngine;

public static class AppManager 
{
    #region Private Variables
    [SerializeField] private static float resetTimer = 5.0f;
    [SerializeField] private static int playerCount = 2;
    private static Color[] playerColors = { Color.white, Color.white };
    private static string[] playerShip = { "", "" };
    private static bool[] isPlayersReady = { false, false };
    private static int levelIndex = 0;
    private static int victor = -1;
    private static float gameTimer = 0f;
    private static bool isUnpaused = true;
    private static string strResetScene = "Splash";

    #endregion

    #region Properties          

    public static int Victor
    {
        get
        {
            return victor;
        }
        set
        {
            victor = value;
        }
    }

    public static int PlayerCount
    {
        get
        {
            return playerCount;
        }
    }

    public static bool IsUnpaused
    {
        get
        {
            return isUnpaused;
        }
    }

    public static float ResetTimer
    {
        get
        {
            return resetTimer;
        }
    }

    public static bool PlayersReady
    {
        get
        {
            bool results = true;
            foreach (bool ready in isPlayersReady)
                if (!ready)
                    results = false;
            return results;
        }
    }

    public static int LevelIndex
    {
        get
        {
            return levelIndex;
        }
        set
        {
            levelIndex = value;
        }
    }

    public static float GameTimer
    {
        get
        {
            return gameTimer;
        }
        set
        {
            gameTimer = value;
        }
    }
    
    public static string ResetScene
    {
        get
        {
            return strResetScene;
        }
    }

    #endregion

    #region Functions

    public static void Reset()
    {
        playerColors = new Color[]{ Color.white, Color.white };
        playerShip = new string[] { "", "" };
        isPlayersReady = new bool[]{ false, false };
        victor = -1;
        levelIndex = 0;
        gameTimer = 0f;
        isUnpaused = true;
    }

    public static Color GetPlayerColor(int playerId)
    {
        return playerColors[playerId];
    }

    public static void SetPlayerColor(int playerId, Color color)
    {
        playerColors[playerId] = color;
    }
        
    public static string GetPlayerShip(int playerID)
    {
        return playerShip[playerID];
    }

    public static void SetPlayerShip(int playerID, string ship)
    {
        playerShip[playerID] = "Prefab/Ships/" + ship;
    }
    
    public static void Pause()
    {
        isUnpaused = !isUnpaused;
    }

    public static void SetIsPlayerReady(int playerID, bool result)
    {
        isPlayersReady[playerID] = result;
    }

    public static bool GetIsPlayerReady(int playerID)
    {
        return isPlayersReady[playerID];
    }

    #endregion

}


