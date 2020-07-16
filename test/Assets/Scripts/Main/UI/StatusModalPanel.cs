using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusModalPanel : MonoBehaviour
{
    public MainEnvManager envManage;

    public Text avatar1Name;
    public Text avatar1Status;
    public Text avatar1Level;
    public Text avatar1Expr;
    public Text avatar1Hungry;
    public Text avatar1Sanity;

    public Text avatar2Name;
    public Text avatar2Status;
    public Text avatar2Level;
    public Text avatar2Expr;
    public Text avatar2Hungry;
    public Text avatar2Sanity;

    public Text avatar3Name;
    public Text avatar3Status;
    public Text avatar3Level;
    public Text avatar3Expr;
    public Text avatar3Hungry;
    public Text avatar3Sanity;

    void Start()
    {
        UpdateStatus(envManage.player.playerData.avatarData1, Constant.AVATAR1_NAME, avatar1Name, avatar1Status, avatar1Level, avatar1Expr, avatar1Hungry, avatar1Sanity);
        UpdateStatus(envManage.player.playerData.avatarData2, Constant.AVATAR2_NAME, avatar2Name, avatar2Status, avatar2Level, avatar2Expr, avatar2Hungry, avatar2Sanity);
        UpdateStatus(envManage.player.playerData.avatarData3, Constant.AVATAR3_NAME, avatar3Name, avatar3Status, avatar3Level, avatar3Expr, avatar3Hungry, avatar3Sanity);
    }

    private void UpdateStatus(AvatarData avatarData, string nameToSet, Text name, Text status, Text level, Text expr, Text hungry, Text sanity)
    {
        name.text = nameToSet;
        status.text = avatarData.state.ToString();
        level.text = avatarData.level.ToString();
        expr.text = avatarData.experience.ToString() + "/" + (avatarData.level == 0 ? Constant.AVATAR_EXPR_LEVEL_1_TO_2.ToString() : Constant.AVATAR_EXPR_LEVEL_2_TO_3.ToString());
        hungry.text = avatarData.hungry.ToString() + "/" + (avatarData.level == 0 ? Constant.AVATAR_MAX_HUNGRY_LEVEL_1.ToString() : Constant.AVATAR_MAX_HUNGRY_LEVEL_2.ToString()); ;
        sanity.text = avatarData.sanity.ToString() + "/" + (avatarData.level == 0 ? Constant.AVATAR_MAX_SANITY_LEVEL_1.ToString() : Constant.AVATAR_MAX_SANITY_LEVEL_2.ToString()); ;
    }
}
