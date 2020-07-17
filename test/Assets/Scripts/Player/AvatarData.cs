using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Resources;
using UnityEngine;

[System.Serializable]
public class AvatarData
{
    public int index;
    public string name;
    public int sanity;
    public int hungry;
    public int level;
    public int experience;
    public Constant.AvatarState state;
    public int deadDaysCount;
    public int slotIndex;
    public bool isDead;

    private Adventure adventure;

    public AvatarData(int index)
    {
        this.index = index;
        switch(index)
        {
            case 0:
                this.name = Constant.AVATAR1_NAME;
                break;
            case 1:
                this.name = Constant.AVATAR2_NAME;
                break;
            case 2:
                this.name = Constant.AVATAR3_NAME;
                break;
        }
        level = 1;
        hungry = Constant.AVATAR_MAX_HUNGRY_LEVEL_1;
        sanity = Constant.AVATAR_MAX_SANITY_LEVEL_1;
        state = Constant.AvatarState.IDLING;
        slotIndex = -1;
    }

    public Reward SetState(Constant.AvatarState state, int cafeLevel, int dormLevel, int factoryLevel, Adventure adventure, bool isEndDayStateLoop)
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
                    reward = UpdateAdventureComplete(adventure);
                    
                    break;
                case Constant.AvatarState.WORKING:
                    reward = UpdateStatusFactoryComplete(factoryLevel);
                    break;
                default:
                    // Do nothing
                    break;
            }
            slotIndex = -1;
        }
        else if (state == Constant.AvatarState.ADVENTURE && adventure != null)
        {
            this.adventure = adventure;
        }

        // Update avatar dead days if it has < 0 sanity/hungry
        if (this.hungry <= 0 || this.sanity <= 0)
        {
            if (deadDaysCount >= 3)
            {
                Debug.Log("3 days hungry/sanity below 0, dead");
                isDead = true;
            }
            else
            {
                Debug.Log("Dead days ++");
                deadDaysCount++;
            }
        }
        else
        {
            deadDaysCount = 0;
            isDead = false;
        }

        // Update if 3 day state change reached
        if (isEndDayStateLoop && !isDead)
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
            delta = Constant.CAFE_HUNGRY_RECOVER_LEVEL_1 + this.hungry > Constant.AVATAR_MAX_HUNGRY_LEVEL_1 ?
                Constant.AVATAR_MAX_HUNGRY_LEVEL_1 - this.hungry
                :
                Constant.DORM_SANITY_RECOVER_LEVEL_1;
        }

        if (cafeLevel == 1)
        {
            delta = Constant.CAFE_HUNGRY_RECOVER_LEVEL_2 + this.hungry > Constant.AVATAR_MAX_HUNGRY_LEVEL_2 ?
                Constant.AVATAR_MAX_HUNGRY_LEVEL_2 - this.hungry
                :
                Constant.DORM_SANITY_RECOVER_LEVEL_2;
        }
        this.hungry += delta;
        Debug.Log("Cafe Complete: hungry get " + delta);
    }

    private void UpdateStatusDormComplete(int dormLevel)
    {
        int delta = 0;
        if (dormLevel == 0)
        {
            delta = Constant.DORM_SANITY_RECOVER_LEVEL_1 + this.sanity > Constant.AVATAR_MAX_SANITY_LEVEL_1 ?
                Constant.AVATAR_MAX_SANITY_LEVEL_1 - this.sanity
                :
                Constant.DORM_SANITY_RECOVER_LEVEL_1;
        }

        if (dormLevel == 1)
        {
            delta = Constant.DORM_SANITY_RECOVER_LEVEL_2 + this.sanity > Constant.AVATAR_MAX_SANITY_LEVEL_2 ?
                Constant.AVATAR_MAX_SANITY_LEVEL_2 - this.sanity
                :
                Constant.DORM_SANITY_RECOVER_LEVEL_2
                ;
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
            reward.energyGain = Constant.FACTORY_ENERGY_PRODUCE_LEVEL_1;
        }

        if (factoryLevel == 1)
        {
            delta = Constant.FACTORY_HUNGRY_COST_LEVEL_2;
            delta2 = Constant.FACTORY_SANITY_COST_LEVEL_2;
            reward.energyGain = Constant.FACTORY_ENERGY_PRODUCE_LEVEL_2;
        }
        this.hungry += delta;
        this.sanity += delta2;
        return reward;
    }

    private Reward UpdateAdventureComplete(Adventure adventure)
    {
        Dictionary<Item, int> rewardItems = new Dictionary<Item, int>();
        foreach (KeyValuePair<Item, int> pair in adventure.items)
        {
            int alreadyHave = 0;
            rewardItems.TryGetValue(pair.Key, out alreadyHave);
            rewardItems[pair.Key] = alreadyHave + pair.Value;
        }
        
        return new Reward(adventure.materialGain, adventure.energyGain, rewardItems);
    }

    private void UpdateStatusDayStateComplete()
    {
        this.hungry += Constant.DAY_STATE_LOOP_HUNGRY_COST;
        this.sanity += Constant.DAY_STATE_LOOP_SANITY_COST;
        Debug.Log("Day State Loop Complete: sanity get " + Constant.DAY_STATE_LOOP_SANITY_COST + " hungry get " + Constant.DAY_STATE_LOOP_HUNGRY_COST);
    }
}
