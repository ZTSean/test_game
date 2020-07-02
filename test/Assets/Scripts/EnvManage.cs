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

    public void MoveToNextDayState()
    {
        if (player.playerData.dayState == Constant.DayState.MORNING)
        {
            player.playerData.dayState = Constant.DayState.AFTERNOON;
        }
        else if (player.playerData.dayState == Constant.DayState.AFTERNOON)
        {
            player.playerData.dayState = Constant.DayState.NIGHT;
        }
        else if (player.playerData.dayState == Constant.DayState.NIGHT)
        {
            player.playerData.dayState = Constant.DayState.MORNING;
        }

        for (int i = 0; i < 3; i++)
        {
            player.playerData.UpdateAvatarState(i, Constant.AvatarState.IDLING);
        }
    }
}
