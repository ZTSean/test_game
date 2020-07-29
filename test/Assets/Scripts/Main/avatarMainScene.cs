using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class avatarMainScene : MonoBehaviour
{
    public int avatarIndex;
    public Animator animator;
    public MainEnvManager envManage;
    public Constant.AvatarState state;

    Vector2 initialPosition;

    public const string ANIMATOR_IS_IN_IDLING_PROPERTY_NAME = "isIdling";
    public const string ANIMATOR_IS_IN_LOW_HUNGRY_PROPERTY_NAME = "isLowHungry";
    public const string ANIMATOR_IS_IN_LOW_SANITY_PROPERTY_NAME = "isLowSanity";
    public const string ANIMATOR_IS_IN_LOW_HUNGRY_SANITY_PROPERTY_NAME = "isLowHungrySanity";
    public const string ANIMATOR_IS_IN_DEAD_PROPERTY_NAME = "isDead";

    void Start()
    {
        initialPosition = this.gameObject.transform.position;
    }

    void Update()
    {
        envManage.LoadAvatarDataFromPlayerData(avatarIndex);
    }

    public void LoadData(AvatarData avatarData)
    {
        bool isLowHungry = avatarData.hungry <= 2;
        bool isLowSanity = avatarData.sanity <= 2;
        if (avatarData.isDead)
        {
            ChangeAnimation(ANIMATOR_IS_IN_DEAD_PROPERTY_NAME, true);
        }
        else if (isLowHungry && isLowSanity)
        {
            ChangeAnimation(ANIMATOR_IS_IN_LOW_HUNGRY_SANITY_PROPERTY_NAME, true);
        }
        else if (isLowHungry)
        {
            ChangeAnimation(ANIMATOR_IS_IN_LOW_HUNGRY_PROPERTY_NAME, true);
        }
        else if (isLowSanity)
        {
            ChangeAnimation(ANIMATOR_IS_IN_LOW_SANITY_PROPERTY_NAME, true);
        }
        else
        {
            ChangeAnimation(ANIMATOR_IS_IN_IDLING_PROPERTY_NAME, true);
        }
    }

    protected void ChangeAnimation(string flagName, bool value)
    {
        animator.SetBool(flagName, value);
    }
}
