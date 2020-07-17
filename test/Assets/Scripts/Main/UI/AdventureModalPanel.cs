using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdventureModalPanel : MonoBehaviour
{
    public MainEnvManager envManager;
    public Button continueButton;
    public Button goButton;
    public int selectedIndexAdventure = -1;
    public int selectedIndexAvatar = -1;

    void Start()
    {
        goButton.enabled = false;
        continueButton.enabled = false;
        selectedIndexAdventure = -1;
        selectedIndexAvatar = -1;
    }

    public void SetSelectedIndexAdventure(int index)
    {
        this.selectedIndexAdventure = index;
        continueButton.enabled = true;
    }

    public void SetSelectedIndexAvatar(int index)
    {
        this.selectedIndexAvatar = index;
    }

    public void StartAdventure()
    {
        //validate whether the avatar has enough sanity & hungry
    }

    public bool isEnoughSanityAndHungry(int adventureIndex, int avatarIndex)
    {
        // Determine hungry & sanity cost for the adventure
        Adventure adventure = DetermineAdventure(adventureIndex);

        AvatarData avatarData = DetermineAvatar(avatarIndex);

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

    public AvatarData DetermineAvatar(int avatarIndex)
    {
        switch (avatarIndex)
        {
            case 0:
                return envManager.player.playerData.avatarData1;
            case 1:
                return envManager.player.playerData.avatarData2;
            case 2:
                return envManager.player.playerData.avatarData3;
            default:
                return null;
        }
    }

    public void UpdateGoButton(bool isAvailable)
    {
        goButton.enabled = isAvailable || goButton.enabled;
    }

    public void UpdateContinueButton(bool isAvailable)
    {
        continueButton.enabled = isAvailable || goButton.enabled;
    }
}
