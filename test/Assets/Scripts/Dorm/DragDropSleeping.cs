using UnityEngine;

public class DragDropSleeping : DragDrop
{
    protected override void OnMouseUpActionTriggered(Slot slot)
    {
        base.OnMouseUpActionTriggered(slot);
        slot.gameObject.SetActive(false);
    }
}
