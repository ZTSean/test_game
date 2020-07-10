using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainEnvManager : MonoBehaviour
{
    public Player player;
    public GameObject cafeLevel1;
    public GameObject cafeLevel2;
    public GameObject factoryLevel1;
    public GameObject factoryLevel2;
    public GameObject dormLevel1;
    public GameObject dormLevel2;
    public GameObject spaceshipLevel1;
    public GameObject spaceshipLevel2;
    public GameObject inConstructionPrefeb;

    // Backgound related resources
    public Sprite backgroundMorning;
    public Sprite backgroundAfternoon;
    public Sprite backgroundNight;
    public GameObject backgroundGameobject;
    
    private Dictionary<int, UpgradationTimer> upgradationTimers;
    private Dictionary<int, GameObject> consturctions;

    void Start()
    {
        upgradationTimers = new Dictionary<int, UpgradationTimer>();
        consturctions = new Dictionary<int, GameObject>();
        if (player.playerData.lastUpdateTime != null && 
            player.playerData.lastUpdateTime.AddMinutes(5) > System.DateTime.UtcNow)
        {
            // if off game more than 5 mins move to next day state
            MoveToNextDayState();
        }
        LoadEnvironmentAll();
    }

    void Update()
    {
        foreach(int index in upgradationTimers.Keys)
        {
            if (upgradationTimers[index].IsUpgradationEnd())
            {
                EndUpgradation(index);
            }
        }
    }

    public void LoadEnvironmentAll()
    {
        switch (player.playerData.cafeLevel)
        {
            case 0:
                cafeLevel1.SetActive(true);
                cafeLevel2.SetActive(false);
                break;
            case 1:
                cafeLevel2.SetActive(true);
                cafeLevel1.SetActive(false);
                break;
        }

        switch (player.playerData.factoryLevel)
        {
            case 0:
                factoryLevel1.SetActive(true);
                factoryLevel2.SetActive(false);
                break;
            case 1:
                factoryLevel2.SetActive(true);
                factoryLevel1.SetActive(false);
                break;
        }

        switch (player.playerData.dormLevel)
        {
            case 0:
                dormLevel1.SetActive(true);
                dormLevel2.SetActive(false);
                break;
            case 1:
                dormLevel2.SetActive(true);
                dormLevel1.SetActive(false);
                break;
        }

        switch (player.playerData.level)
        {
            case 0:
                spaceshipLevel1.SetActive(true);
                spaceshipLevel2.SetActive(false);
                break;
            case 1:
                spaceshipLevel2.SetActive(true);
                spaceshipLevel1.SetActive(false);
                break;
        }

        Debug.Log(player.playerData.dayState);
        switch (player.playerData.dayState)
        {
            case Constant.DayState.MORNING:
                backgroundGameobject.GetComponent<SpriteRenderer>().sprite = backgroundMorning;
                break;
            case Constant.DayState.AFTERNOON:
                backgroundGameobject.GetComponent<SpriteRenderer>().sprite = backgroundAfternoon;
                break;
            case Constant.DayState.NIGHT:
                backgroundGameobject.GetComponent<SpriteRenderer>().sprite = backgroundNight;
                break;
        }
    }

    public void StartUpgradation(int upgradationIndex)
    {
        if (upgradationTimers.ContainsKey(upgradationIndex))
        {
            Debug.Log("Already in upgradation.");
            return;
        }
        UpgradationTimer upgradationTimer = new UpgradationTimer();
        upgradationTimer.StartUpgradation();
        upgradationTimers.Add(upgradationIndex, upgradationTimer);

        // Show upgradation animation
        int levelUpgradeTo = upgradationIndex % 10;
        int sceneIndex = (upgradationIndex / 10) % 10;

        Vector3 inConstructionAnimationPos = new Vector3(0,0,0);
        Quaternion inConstructionAnimationRotate = new Quaternion();
        switch (sceneIndex)
        {
            case Constant.SCENE_INDEX_CAFE:
                cafeLevel1.SetActive(false);
                inConstructionAnimationPos = cafeLevel1.transform.position;
                inConstructionAnimationRotate = cafeLevel1.transform.rotation;
                break;
            case Constant.SCENE_INDEX_DORM:
                dormLevel1.SetActive(false);
                inConstructionAnimationPos = dormLevel1.transform.position;
                inConstructionAnimationRotate = dormLevel1.transform.rotation;

                break;
            case Constant.SCENE_INDEX_FACTORY:
                factoryLevel1.SetActive(false);
                inConstructionAnimationPos = factoryLevel1.transform.position;
                inConstructionAnimationRotate = factoryLevel1.transform.rotation;

                break;
            case Constant.SCENE_INDEX_MAIN:
                spaceshipLevel1.SetActive(false);
                inConstructionAnimationPos = spaceshipLevel1.transform.position;
                inConstructionAnimationRotate = spaceshipLevel1.transform.rotation;
                break;
        }
        GameObject inConsturction = Instantiate(inConstructionPrefeb, inConstructionAnimationPos, inConstructionAnimationRotate);
        inConsturction.SetActive(true);
        consturctions.Add(upgradationIndex, inConsturction);
    }

    private void EndUpgradation(int upgradationIndex)
    {
        int levelUpgradeTo = upgradationIndex % 10;
        int sceneIndex = (upgradationIndex / 10) % 10;

        GameObject.DestroyImmediate(consturctions[upgradationIndex]);

        switch (sceneIndex)
        {
            case Constant.SCENE_INDEX_CAFE:
                cafeLevel2.SetActive(true);
                break;
            case Constant.SCENE_INDEX_DORM:
                dormLevel2.SetActive(true);
                break;
            case Constant.SCENE_INDEX_FACTORY:
                factoryLevel2.SetActive(true);
                break;
            case Constant.SCENE_INDEX_MAIN:
                spaceshipLevel2.SetActive(true);
                break;
        }
    }

    public void MoveToNextDayState()
    {
        SpriteRenderer backgroundSpriteRender = backgroundGameobject.GetComponent<SpriteRenderer>();
        if (player.playerData.dayState == Constant.DayState.MORNING)
        {
            player.playerData.dayState = Constant.DayState.AFTERNOON;
            backgroundSpriteRender.sprite = backgroundAfternoon;
        }
        else if (player.playerData.dayState == Constant.DayState.AFTERNOON)
        {
            player.playerData.dayState = Constant.DayState.NIGHT;
            backgroundSpriteRender.sprite = backgroundNight;
        }
        else if (player.playerData.dayState == Constant.DayState.NIGHT)
        {
            player.playerData.dayState = Constant.DayState.MORNING;
            backgroundSpriteRender.sprite = backgroundMorning;
        }

        bool isEndDayStateLoop = false ;
        player.playerData.dayStateLoopCount++;
        if (player.playerData.dayStateLoopCount > 0 && player.playerData.dayStateLoopCount % Constant.DAY_STATE_LOOP_THRESHOLD == 0)
        {
            player.playerData.dayStateLoopCount %= Constant.DAY_STATE_LOOP_THRESHOLD;
            
            isEndDayStateLoop = true;
        }

        for (int i = 0; i < 3; i++)
        {
            player.playerData.UpdateAvatarState(i, Constant.AvatarState.IDLING, isEndDayStateLoop);
        }
    }
}