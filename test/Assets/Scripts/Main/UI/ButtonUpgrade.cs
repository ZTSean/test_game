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
                mainEnvManager.StartUpgradation(upgradationRequirementIndex);
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
