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

    public const string ANIMATOR_IS_IN_LOW_HUNGRY_PROPERTY_NAME = "isLowHungry";
    public const string ANIMATOR_IS_IN_LOW_SANITY_PROPERTY_NAME = "isLowSanity";
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
        this.state = avatarData.state;
        switch(state)
        {
            case Constant.AvatarState.LOW_HUNGRY:
                ChangeAnimation(ANIMATOR_IS_IN_LOW_HUNGRY_PROPERTY_NAME, true);
                break;
            case Constant.AvatarState.LOW_SANITY:
                ChangeAnimation(ANIMATOR_IS_IN_LOW_SANITY_PROPERTY_NAME, true);
                break;
            case Constant.AvatarState.DEAD:
                ChangeAnimation(ANIMATOR_IS_IN_DEAD_PROPERTY_NAME, true);
                break;
        }
    }

    protected void ChangeAnimation(string flagName, bool value)
    {
        animator.SetBool(flagName, value);
    }
}
