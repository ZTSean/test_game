using JetBrains.Annotations;
using UnityEngine;

public class AvatarSleeping : Avatar
{
    protected override bool OnMouseUpActionTriggered(Slot slot)
    {
        base.OnMouseUpActionTriggered(slot);
        slot.gameObject.SetActive(false);
        return true;
    }

    protected override void UpdateStateIfMouseUpTriggered()
    {
        this.UpdateState(Constant.AvatarState.SLEEPING);
    }
}
