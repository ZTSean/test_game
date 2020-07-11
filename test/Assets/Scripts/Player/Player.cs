using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerData playerData;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Load player data");
        playerData = SaveSystem.LoadPlayer();
    }

    private void OnDestroy()
    {
        Debug.Log("Save player data");
        this.playerData.lastUpdateTime = System.DateTime.UtcNow;
        SaveSystem.SavePlayer(this.playerData);
    }
}