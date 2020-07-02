using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarEating : Avatar
{
    protected override void UpdateStateIfMouseUpTriggered()
    {
        this.UpdateState(Constant.AvatarState.EATING);
    }
}
