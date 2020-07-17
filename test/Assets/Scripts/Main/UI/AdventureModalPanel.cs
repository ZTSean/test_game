using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureModalPanel : MonoBehaviour
{
    public MainEnvManager envManager;
    public int selectedIndexAdventure = -1;
    public int selectedIndexAvatar = -1;

    void Start()
    {
        selectedIndexAdventure = -1;
        selectedIndexAvatar = -1;
    }

    void Update()
    {
        
    }

    public void SetSelectedIndexAdventure(int index)
    {
        this.selectedIndexAdventure = index;
    }

    public void SetSelectedIndexAvatar(int index)
    {
        this.selectedIndexAvatar = index;
    }

    public void StartAdventure()
    {
        //validate whether the avatar has enough sanity & hungry
    }

    private bool isEnoughSanityAndHungry(int adventureIndex, int avatarIndex)
    {
        // Determine hungry & sanity cost for the adventure
        Adventure adventure = DetermineAdventure(adventureIndex);

        AvatarData avatarData = null;
        switch(avatarIndex)
        {
            case 0:
                avatarData = envManager.player.playerData.avatarData1;
                break;
            case 1:
                avatarData = envManager.player.playerData.avatarData2;
                break;
            case 2:
                avatarData = envManager.player.playerData.avatarData3;
                break;
        }

        return avatarData != null && 
            avatarData.state == Constant.AvatarState.IDLING && 
            avatarData.hungry >= adventure.hungryCost && 
            avatarData.sanity >= adventure.sanityCost;
    }

    public Adventure DetermineAdventure(int adventureIndex)
    {
        switch(adventureIndex)
        {
            case 0:
                return Constant.ADVENTURE1;
            default:
                return null;
        };
    }
}
