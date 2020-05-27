using UnityEngine;

public class DragDropSleeping : DragDrop
{
    public Sprite dropSprite;

    protected override void OnMouseUpActionTriggered(Slot slot)
    {
        Debug.Log("Detect drop slot");
        slot.GetComponent<SpriteRenderer>().sprite = dropSprite;
        this.gameObject.SetActive(false);
    }
}
