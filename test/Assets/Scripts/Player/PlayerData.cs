using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int materialAmount;
    public int energyAmount;
    public int cafeLevel;
    public int factoryLevel;
    public int dormLevel;
    public int level;
    public int numberOfAvatars;

    public PlayerData()
    {
        materialAmount = 0;
        energyAmount = 0;
        cafeLevel = 0;
        dormLevel = 0;
        factoryLevel = 0;
        level = 0;
        numberOfAvatars = 3;
    }
}
