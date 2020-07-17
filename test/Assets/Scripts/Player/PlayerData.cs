using System;
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
    public AvatarData avatarData1;
    public AvatarData avatarData2;
    public AvatarData avatarData3;
    public Constant.DayState dayState;
    public DateTime lastUpdateTime;
    public int dayStateLoopCount;
    public Dictionary<Item, int> items;
    public PlayerData()
    {
        materialAmount = 0;
        energyAmount = 0;
        cafeLevel = 0;
        dormLevel = 0;
        factoryLevel = 0;
        level = 0;
        numberOfAvatars = 3;
        dayState = Constant.DayState.MORNING;
        avatarData1 = new AvatarData(0);
        avatarData2 = new AvatarData(1);
        avatarData3 = new AvatarData(2);
        items = new Dictionary<Item, int>();
    }

    public void UpdateAvatarState(int avatarIndex, Constant.AvatarState state, bool isEndDayStateLoop)
    {
        Reward reward = new Reward();
        switch (avatarIndex)
        {
            case 0:
                reward = avatarData1.SetState(state, cafeLevel, dormLevel, factoryLevel, null, isEndDayStateLoop);
                break;
            case 1:
                reward = avatarData2.SetState(state, cafeLevel, dormLevel, factoryLevel, null, isEndDayStateLoop);
                break;
            case 2:
                reward = avatarData3.SetState(state, cafeLevel, dormLevel, factoryLevel, null, isEndDayStateLoop);
                break;
        }

        if (reward.energyGain > 0)
        {
            // Update material cost if from factory working to idling
            if (factoryLevel == 1)
            {
                materialAmount += Constant.FACTORY_MATERIAL_COST_LEVEL_1;
            }
            else if (factoryLevel == 2)
            {
                materialAmount += Constant.FACTORY_MATERIAL_COST_LEVEL_2;
            }

            // Update enercy collected if from factory working to idling
            energyAmount += reward.energyGain;
            Debug.Log("Reward: energy get " + reward.energyGain);
        }

        // Update collected item from adventure
        foreach (var item in reward.items)
        {
            int alreayHaveCount = 0;
            bool hasValue = this.items.TryGetValue(item.Key, out alreayHaveCount);
            if (hasValue)
            {
                this.items[item.Key] = alreayHaveCount + item.Value;
            }
            else
            {
                this.items[item.Key] = item.Value;
            }
        }
    }
}
