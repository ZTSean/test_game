using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Resources;
using UnityEngine;

[System.Serializable]
public class AvatarData
{
    public int sanity;
    public int hungry;
    public int level;
    public int experience;
    public Constant.AvatarState state;

    public AvatarData()
    {
        hungry = Constant.AVATAR_MAX_HUNGRY_LEVEL_1;
        sanity = Constant.AVATAR_MAX_SANITY_LEVEL_1;
        state = Constant.AvatarState.IDLING;
    }

    public Reward SetState(Constant.AvatarState state, int cafeLevel, int dormLevel, int factoryLevel, int adventureIndex, bool isEndDayStateLoop)
    {
        Reward reward = new Reward();
        if (state == Constant.AvatarState.IDLING)
        {
            switch (this.state)
            {
                // Update avatar status data based on current level
                case Constant.AvatarState.EATING:
                    UpdateStatusCafeComplete(cafeLevel);
                    break;
                case Constant.AvatarState.SLEEPING:
                    UpdateStatusDormComplete(dormLevel);
                    break;
                case Constant.AvatarState.ADVENTURE:
                    reward = UpdateAdventureComplete(adventureIndex);
                    break;
                case Constant.AvatarState.WORKING:
                    reward = UpdateStatusFactoryComplete(factoryLevel);
                    break;
            }
        }
        if (isEndDayStateLoop)
        {
            UpdateStatusDayStateComplete();
        }
        this.state = state;
        return reward;
    }

    private void UpdateStatusCafeComplete(int cafeLevel)
    {
        int delta = 0;
        if (cafeLevel == 0)
        {
            delta = Constant.CAFE_HUNGRY_RECOVER_LEVEL_1;
        }

        if (cafeLevel == 1)
        {
            delta = Constant.CAFE_HUNGRY_RECOVER_LEVEL_2;
        }
        this.hungry += delta;
        Debug.Log("Cafe Complete: hungry get " + delta);
    }

    private void UpdateStatusDormComplete(int dormLevel)
    {
        int delta = 0;
        if (dormLevel == 0)
        {
            delta = Constant.DORM_SANITY_RECOVER_LEVEL_1;
        }

        if (dormLevel == 1)
        {
            delta = Constant.DORM_SANITY_RECOVER_LEVEL_2;
        }
        this.sanity += delta;
        Debug.Log("Dorm Complete: sanity get " + delta);
    }

    private Reward UpdateStatusFactoryComplete(int factoryLevel)
    {
        int delta = 0, delta2 = 0;
        Reward reward = new Reward();
        if (factoryLevel == 0)
        {
            delta = Constant.FACTORY_HUNGRY_COST_LEVEL_1;
            delta2 = Constant.FACTORY_SANITY_COST_LEVEL_1;
            reward.energy = Constant.FACTORY_ENERGY_PRODUCE_LEVEL_1;
        }

        if (factoryLevel == 1)
        {
            delta = Constant.FACTORY_HUNGRY_COST_LEVEL_2;
            delta2 = Constant.FACTORY_SANITY_COST_LEVEL_2;
            reward.energy = Constant.FACTORY_ENERGY_PRODUCE_LEVEL_2;
        }
        this.hungry += delta;
        this.sanity += delta2;
        return reward;
    }

    private Reward UpdateAdventureComplete(int adventureIndex)
    {
        Reward reward = new Reward();
        return reward;
    }

    private void UpdateStatusDayStateComplete()
    {
        this.hungry += Constant.DAY_STATE_LOOP_HUNGRY_COST;
        this.sanity += Constant.DAY_STATE_LOOP_SANITY_COST;
        Debug.Log("Day State Loop Complete: sanity get " + Constant.DAY_STATE_LOOP_SANITY_COST + " hungry get " + Constant.DAY_STATE_LOOP_HUNGRY_COST);
    }
}
