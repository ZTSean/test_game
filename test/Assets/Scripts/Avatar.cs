using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Avatar : MonoBehaviour
{
    public int avatarIndex;
    public Animator animator;
    public EnvManage envManage;
    public Constant.AvatarState state;

    Vector2 initialPosition;
    private bool isDragging;
    private Slot currentDropSlot;

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
                    if (OnMouseUpActionTriggered(slot))
                    {
                        currentDropSlot = slot;
                        isDroppedToSlot = true;
                    }
                    else
                    {
                        // Not able to drop to slot since the sanity/hungry/material is NOT enough
                        // popup window to show it
                    }
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

    protected void CompleteCurrentState()
    {
        switch (state)
        {
            case Constant.AvatarState.EATING:
                break;
            case Constant.AvatarState.SLEEPING:
                break;
            case Constant.AvatarState.WORKING:
                break;
            case Constant.AvatarState.ADVENTURE:
                break;
        }
        state = Constant.AvatarState.IDLING;

        bool isEndDayStateLoop = false;
        envManage.player.playerData.dayStateLoopCount++;
        if (envManage.player.playerData.dayStateLoopCount > 0 && envManage.player.playerData.dayStateLoopCount % Constant.DAY_STATE_LOOP_THRESHOLD == 0)
        {
            envManage.player.playerData.dayStateLoopCount %= Constant.DAY_STATE_LOOP_THRESHOLD;

            isEndDayStateLoop = true;
        }

        // Update playerData to save to file
        envManage.player.playerData.UpdateAvatarState(avatarIndex, state, isEndDayStateLoop);
    }

    protected virtual bool OnMouseUpActionTriggered(Slot slot)
    {
        Debug.Log("Detected slot");
        ChangePosition(slot.transformToBeSet.position);
        ChangeAnimation(ANIMATOR_IS_DROPPING_PROPERTY_NAME + slot.level, true);
        ChangeAnimation(ANIMATOR_IS_DRAGGING_PROPERTY_NAME, false);
        UpdateStateIfMouseUpTriggered();
        return true;
    }

    protected virtual void OnMouseUpNoActionTriggered()
    {
        Debug.Log("No slot delected. Reset to initial position");
        ChangeAnimation(ANIMATOR_IS_DRAGGING_PROPERTY_NAME, false);
        if (currentDropSlot != null)
        {
            ChangeAnimation(ANIMATOR_IS_DROPPING_PROPERTY_NAME + currentDropSlot.level, false);
            currentDropSlot = null;
        }
        ChangePosition(initialPosition);
        UpdateState(Constant.AvatarState.IDLING);
    }

    protected void UpdateState(Constant.AvatarState state)
    {
        // This update state will only be triggered when mouse is up/down, does not mean it finish current task
        envManage.player.playerData.UpdateAvatarState(avatarIndex, state, false);
    }

    protected virtual void UpdateStateIfMouseUpTriggered()
    {
    }
}
