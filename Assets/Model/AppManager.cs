using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;
using System;

public static class AppManager
{
    private static Color[] playerColors = { Color.white, Color.white };

    private static bool[] isPlayersReady = { false, false };
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

    private static bool isPaused = false;
    public static bool IsPaused
    {
        get
        {
            return isPaused;
        }
    }

    public static void Pause()
    {
        isPaused = !isPaused;
    }
    
    private static string[] playerShip = {"", ""};
    public static string GetPlayerShip(int playerID)
    {
        return playerShip[playerID];
    }

    public static void SetPlayerShip(int playerID, string ship)
    {
        playerShip[playerID] = "Prefab/Ships/" + ship;
    }

    private static int levelIndex = 0;
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
}


