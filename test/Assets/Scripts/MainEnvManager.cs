using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        LoadEnvironmentAll();
    }

    void Update()
    {

    }

    public void LoadEnvironmentAll()
    {
        switch (player.playerData.cafeLevel)
        {
            case 0:
                cafeLevel1.SetActive(true);
                cafeLevel1.SetActive(false);
                break;
            case 1:
                cafeLevel2.SetActive(true);
                cafeLevel2.SetActive(false);
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
}
