using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarEating : Avatar
{
    public override void LoadData(AvatarData avatarData, Slot slot)
    {
        this.state = avatarData.state;
        this.currentDropSlot = slot;
        if (slot != null && this.state == Constant.AvatarState.EATING)
        {
            ChangePosition(currentDropSlot.transformToBeSet.position);
            ChangeAnimation(ANIMATOR_IS_DROPPING_PROPERTY_NAME + currentDropSlot.level + currentDropSlot.slotIndex, true);
            ChangeAnimation(ANIMATOR_IS_DRAGGING_PROPERTY_NAME, false);
        }
        else
        {
            ChangeAnimation(ANIMATOR_IS_DRAGGING_PROPERTY_NAME, false);
            if (currentDropSlot != null)
            {
                Debug.Log(ANIMATOR_IS_DROPPING_PROPERTY_NAME + currentDropSlot.level + currentDropSlot.slotIndex);
                ChangeAnimation(ANIMATOR_IS_DROPPING_PROPERTY_NAME + currentDropSlot.level + currentDropSlot.slotIndex, false);
                currentDropSlot = null;
            }
            ChangePosition(initialPosition);
        }
    }

    protected override void OnMouseUpActionTriggered()
    {
        ChangePosition(currentDropSlot.transformToBeSet.position);
        ChangeAnimation(ANIMATOR_IS_DROPPING_PROPERTY_NAME + currentDropSlot.level + currentDropSlot.slotIndex, true);
        ChangeAnimation(ANIMATOR_IS_DRAGGING_PROPERTY_NAME, false);
        UpdateStateIfMouseUpTriggered();
    }

    protected override void UpdateStateIfMouseUpTriggered()
    {
        this.UpdateState(Constant.AvatarState.EATING);
    }
}
