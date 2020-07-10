using JetBrains.Annotations;
using UnityEngine;

public class AvatarSleeping : Avatar
{
    protected override bool OnMouseUpActionTriggeredValidation(Slot slot)
    {
        base.OnMouseUpActionTriggeredValidation(slot);
        slot.gameObject.SetActive(false);
        return true;
    }

    protected override void UpdateStateIfMouseUpTriggered()
    {
        this.UpdateState(Constant.AvatarState.SLEEPING);
    }
}
