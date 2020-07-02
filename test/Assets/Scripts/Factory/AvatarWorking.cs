

public class AvatarWorking : Avatar
{
    protected override void UpdateStateIfMouseUpTriggered()
    {
        this.UpdateState(Constant.AvatarState.WORKING);
    }
}