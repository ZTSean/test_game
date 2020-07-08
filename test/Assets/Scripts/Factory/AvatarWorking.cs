using UnityEngine;

public class AvatarWorking : Avatar
{
    protected override bool OnMouseUpActionTriggered(Slot slot)
    {
        AvatarData avatarData = null;
        switch(avatarIndex)
        {
            case 0:
                avatarData = envManage.player.playerData.avatarData1;
                break;
            case 1:
                avatarData = envManage.player.playerData.avatarData2;
                break;
            case 2:
                avatarData = envManage.player.playerData.avatarData3;
                break;
        }

        int hungryCost = 0, sanityCost = 0, materialCost = 0;
        switch(envManage.player.playerData.factoryLevel)
        {
            case 0:
                hungryCost = Constant.FACTORY_HUNGRY_COST_LEVEL_1;
                sanityCost = Constant.FACTORY_SANITY_COST_LEVEL_1;
                materialCost = Constant.FACTORY_MATERIAL_COST_LEVEL_1;
                break;
            case 1:
                hungryCost = Constant.FACTORY_HUNGRY_COST_LEVEL_2;
                sanityCost = Constant.FACTORY_SANITY_COST_LEVEL_2;
                materialCost = Constant.FACTORY_MATERIAL_COST_LEVEL_2;
                break;

        }
        if (avatarData == null)
        {
            Debug.LogError("Could not locate avatarData");
            return false;
        }
        else if (avatarData.hungry >= hungryCost && 
            avatarData.sanity > sanityCost && 
            envManage.player.playerData.materialAmount > materialCost)
        {
            Debug.Log("Detected slot & enough sanity and hungry");
            ChangePosition(slot.transformToBeSet.position);
            ChangeAnimation(ANIMATOR_IS_DROPPING_PROPERTY_NAME + slot.level, true);
            ChangeAnimation(ANIMATOR_IS_DRAGGING_PROPERTY_NAME, false);
            UpdateStateIfMouseUpTriggered();
            return true;
        }
        else
        {
            Debug.Log("Not enough material/sanity/hungry for avatar: " + avatarIndex);
            return false;
        }
    }

    protected override void UpdateStateIfMouseUpTriggered()
    {
        this.UpdateState(Constant.AvatarState.WORKING);
    }
}