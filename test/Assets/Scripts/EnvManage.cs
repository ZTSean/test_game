using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

public class EnvManage : MonoBehaviour
{
    public string sceneName;
    public Player player;
    public List<Slot> slots;
    public Avatar avatar1;
    public Avatar avatar2;
    public Avatar avatar3;
    public GameObject level1Scene;
    public GameObject level2Scene;
    public GameObject level3Scene;

    private int currentSceneIndex;
    private int currentSceneLevel;

    void Start()
    {
        slots = new List<Slot>();
        LoadEnvironmentAll();
    }

    void Update()
    {
        switch (currentSceneIndex)
        {
            case Constant.SCENE_INDEX_CAFE:
                checkSceneLevelUpdate(currentSceneLevel, player.playerData.cafeLevel);
                break;
            case Constant.SCENE_INDEX_FACTORY:
                checkSceneLevelUpdate(currentSceneLevel, player.playerData.factoryLevel);
                break;
            case Constant.SCENE_INDEX_DORM:
                checkSceneLevelUpdate(currentSceneLevel, player.playerData.dormLevel);
                break;
        }
    }

    void checkSceneLevelUpdate(int oldLevel, int newLevel)
    {
        if (oldLevel != newLevel)
        {
            currentSceneLevel = newLevel;
            LoadEnvironmentAll();
        }
    }

    public void LoadEnvironmentAll()
    {
        switch (sceneName)
        {
            case Constant.SCENE_NAME_CAFE:
                currentSceneIndex = Constant.SCENE_INDEX_CAFE;
                currentSceneLevel = player.playerData.cafeLevel;
                break;
            case Constant.SCENE_NAME_DROM:
                currentSceneIndex = Constant.SCENE_INDEX_DORM;
                currentSceneLevel = player.playerData.dormLevel;
                break;
            case Constant.SCENE_NAME_FACTORY:
                currentSceneIndex = Constant.SCENE_INDEX_FACTORY;
                currentSceneLevel = player.playerData.factoryLevel;
                break;
            default:
                Debug.LogError("No correct scene found");
                break;
        }
        LoadEnvironmentObjects(currentSceneLevel);
    }

    private void LoadEnvironmentObjects(int level)
    {
        level1Scene.SetActive(level == 0);
        level2Scene.SetActive(level == 1);
        level3Scene.SetActive(level == 2);
        if (level != 0 && level != 1 && level != 2)
        {
            Debug.LogError("Error level detected, check saved player data");
        }

        LoadSlotsIfPossible(level);
    }

    public void LoadAvatarDataFromPlayerData(int avatarIndex)
    {
        switch(avatarIndex)
        {
            case 0:
                avatar1.LoadData(player.playerData.avatarData1, LocateSlotByIndex(player.playerData.avatarData1.slotIndex));
                break;
            case 1:
                avatar2.LoadData(player.playerData.avatarData2, LocateSlotByIndex(player.playerData.avatarData2.slotIndex));
                break;
            case 2:
                avatar3.LoadData(player.playerData.avatarData3, LocateSlotByIndex(player.playerData.avatarData3.slotIndex));
                break;
        }
    }

    private Slot LocateSlotByIndex(int slotIndex)
    {
        Slot result = null;
        foreach (Slot slot in slots)
        {
            if (slot.slotIndex == slotIndex)
            {
                result = slot;
                break;
            }
        }

        return result;
    }

    private void LoadSlotsIfPossible(int level)
    {
        Slot[] allSlots = FindObjectsOfType<Slot>();
        foreach(Slot slot in allSlots)
        {
            if (slot.level == level)
            {
                slots.Add(slot);
            }
        }
    }
}
