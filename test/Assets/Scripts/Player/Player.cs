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
        playerData = SaveSystem.LoadPlayer();
    }

    private void OnDestroy()
    {
        this.playerData.lastUpdateTime = System.DateTime.UtcNow;
        SaveSystem.SavePlayer(this.playerData);
    }
}