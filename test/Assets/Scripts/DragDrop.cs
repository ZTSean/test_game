using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    public int avatarIndex;
    public Animator animator;
    public EnvManage envManage;

    Vector2 initialPosition;
    private bool isDragging;

    public const string ANIMATOR_IS_DRAGGING_PROPERTY_NAME = "isDragging";
    public const string ANIMATOR_IS_DROPPING_PROPERTY_NAME = "isDropping";

    void Start()
    {
        initialPosition = this.gameObject.transform.position;
    }

    protected void OnMouseDown()
    {
        Debug.Log("start dragging");
        isDragging = true;
        if (isDragging)
        {
            ChangeAnimation(ANIMATOR_IS_DRAGGING_PROPERTY_NAME, true);
        }
    }

    protected void OnMouseUp()
    {
        if (isDragging)
        {
            Debug.Log("Release mouse");
            bool isDroppedToSlot = false;
            foreach (Slot slot in envManage.slots)
            {
                PolygonCollider2D polygonCollider2D = slot.gameObject.GetComponent<PolygonCollider2D>();
                if (polygonCollider2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
                {
                    OnMouseUpActionTriggered(slot);
                    isDroppedToSlot = true;
                    break;
                }
            }
            if (!isDroppedToSlot)
            {
                OnMouseUpNoActionTriggered();
            }
            isDragging = false;
        }
    }

    private void Update()
    {
        if (isDragging)
        {
            ChangePosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    protected void ChangeAnimation(string flagName, bool value)
    {
        animator.SetBool(flagName, value);
    }

    protected void ChangePosition(Vector3 newPos)
    {
        Vector2 diff = newPos - transform.position;
        transform.Translate(diff);
    }

    protected virtual void OnMouseUpActionTriggered(Slot slot)
    {
        Debug.Log("Detected slot");
        ChangePosition(slot.transform.position);
        ChangeAnimation(ANIMATOR_IS_DROPPING_PROPERTY_NAME, true);
        ChangeAnimation(ANIMATOR_IS_DRAGGING_PROPERTY_NAME, false);
    }

    protected virtual void OnMouseUpNoActionTriggered()
    {
        Debug.Log("No slot delected. Reset to initial position");
        ChangeAnimation(ANIMATOR_IS_DRAGGING_PROPERTY_NAME, false);
        ChangeAnimation(ANIMATOR_IS_DROPPING_PROPERTY_NAME, false);
        ChangePosition(initialPosition);
    }
}
