﻿using JetBrains.Annotations;
using UnityEngine;

public class AvatarSleeping : Avatar
{
    protected override bool OnMouseUpActionTriggeredValidation(Slot slot)
    {
        base.OnMouseUpActionTriggeredValidation(slot);
        
        return true;
    }

    public override void LoadData(AvatarData avatarData, Slot slot)
    {
        this.state = avatarData.state;
        this.currentDropSlot = slot;
        if (slot != null)
        {
            ChangePosition(currentDropSlot.transformToBeSet.position);
            ChangeAnimation(ANIMATOR_IS_DROPPING_PROPERTY_NAME + currentDropSlot.level + currentDropSlot.slotIndex, true);
            ChangeAnimation(ANIMATOR_IS_DRAGGING_PROPERTY_NAME, false);
            if (state == Constant.AvatarState.SLEEPING)
            {
                currentDropSlot.gameObject.SetActive(false);
            }
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
        currentDropSlot.gameObject.SetActive(false);
        ChangePosition(currentDropSlot.transformToBeSet.position);
        ChangeAnimation(ANIMATOR_IS_DROPPING_PROPERTY_NAME + currentDropSlot.level + currentDropSlot.slotIndex, true);
        ChangeAnimation(ANIMATOR_IS_DRAGGING_PROPERTY_NAME, false);
        UpdateStateIfMouseUpTriggered();
    }

    protected override void UpdateStateIfMouseUpTriggered()
    {
        this.UpdateState(Constant.AvatarState.SLEEPING);
    }
}
