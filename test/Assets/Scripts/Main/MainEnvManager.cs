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

    private Dictionary<int, UpgradationTimer> upgradationTimers;
    private Dictionary<int, GameObject> consturctions;

    void Start()
    {
        upgradationTimers = new Dictionary<int, UpgradationTimer>();
        consturctions = new Dictionary<int, GameObject>();
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
        Debug.Log("Check end upgradation");
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
}
