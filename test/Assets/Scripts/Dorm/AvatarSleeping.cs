using JetBrains.Annotations;
using UnityEngine;

public class AvatarSleeping : Avatar
{
    protected override void OnMouseUpActionTriggered(Slot slot)
    {
        base.OnMouseUpActionTriggered(slot);
        slot.gameObject.SetActive(false);
    }

    protected override void UpdateStateIfMouseUpTriggered()
    {
        this.UpdateState(Constant.AvatarState.SLEEPING);
    }
}
