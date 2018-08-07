using UnityEngine;

public static class AppManager
{
    #region Private Function

    [SerializeField] private static int playerCount = 2;
    private static Color[] playerColors = { Color.white, Color.white };
    private static bool[] isPlayersReady = { false, false };
    private static string[] playerShip = { "", "" };
    private static bool isUnpaused = true;
    private static int levelIndex = 0;
    private static int victor = -1;

    #endregion

    #region Properties

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

    public static bool IsUnpaused
    {
        get
        {
            return isUnpaused;
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

    #endregion

    #region Functions

    public static Color GetPlayerColor(int playerId)
    {
        return playerColors[playerId];
    }

    public static void SetPlayerColor(int playerId, Color color)
    {
        playerColors[playerId] = color;
    }

    public static void SetIsPlayerReady(int playerID, bool result)
    {
        isPlayersReady[playerID] = result;
    }

    public static bool GetIsPlayerReady(int playerID)
    {
        return isPlayersReady[playerID];
    }

    public static void Pause()
    {
        isUnpaused = !isUnpaused;
    }

    public static string GetPlayerShip(int playerID)
    {
        return playerShip[playerID];
    }

    public static void SetPlayerShip(int playerID, string ship)
    {
        playerShip[playerID] = "Prefab/Ships/" + ship;
    }


    public static bool AvaliableColor(int playerId, Color color)
    {
        bool isAvaliable = true;
        for (int index = 0; index < playerColors.Length; index++)
        {
            if (index != playerId && playerColors[index].Equals(color))
            {
                isAvaliable = false;
                break;
            }
        }
        return isAvaliable;
    }

    #endregion

}


