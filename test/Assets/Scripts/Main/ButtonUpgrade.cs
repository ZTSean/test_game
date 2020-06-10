using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUpgrade : MonoBehaviour
{
    public Player player;
    public int sceneIndex;
    public int levelUpgradeTo;
    public MainEnvManager mainEnvManager;

    private bool isValid;

    private void Start()
    {
        // validate sceneIndex & levelUpgradeTo input from the scene
        foreach(int index in Constant.SCENE_INDEXES)
        {
            if (sceneIndex == index && (levelUpgradeTo == 1 || levelUpgradeTo == 2))
            {
                isValid = true;
                break;
            }
        }
        this.gameObject.GetComponent<Button>().onClick.AddListener(Upgrade);
    }

    private void Upgrade()
    {
        if (isValid)
        {
            // calculate upgradationRequirementIndex based on sceneIndex & levelUpgradeTo
            int upgradationRequirementIndex = sceneIndex * 10 + levelUpgradeTo;
            if (isSatisfyUpgradationRequirements(upgradationRequirementIndex))
            {
                switch (sceneIndex)
                {
                    case Constant.SCENE_INDEX_CAFE:
                        player.playerData.cafeLevel = levelUpgradeTo;
                        break;
                    case Constant.SCENE_INDEX_DORM:
                        player.playerData.dormLevel = levelUpgradeTo;
                        break;
                    case Constant.SCENE_INDEX_FACTORY:
                        player.playerData.factoryLevel = levelUpgradeTo;
                        break;
                    case Constant.SCENE_INDEX_MAIN:
                        player.playerData.level = levelUpgradeTo;
                        break;
                }

                // Reload env after upgradtion
                mainEnvManager.LoadEnvironmentAll();
            }
        }
        else
        {
            Debug.Log("[ERROR] !!! invalid sceneIndex or upgrade level value set for button:" + this.gameObject.name);
        }
    }

    private bool isSatisfyUpgradationRequirements(int upgradationRequirementIndex)
    {
        // Add requirements check
        return true;
    }
}
