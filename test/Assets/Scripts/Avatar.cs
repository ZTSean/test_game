﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Avatar : MonoBehaviour
{
    public int avatarIndex;
    public Animator animator;
    public EnvManage envManage;
    public Constant.AvatarState state;

    protected Vector2 initialPosition;
    private bool isDragging;
    protected Slot currentDropSlot;

    public virtual string ANIMATOR_IS_DRAGGING_PROPERTY_NAME { get { return "isDragging"; } }
    public virtual string ANIMATOR_IS_DROPPING_PROPERTY_NAME { get { return "isDropping"; } }

    void Start()
    {
        initialPosition = this.gameObject.transform.position;

        // Only show sprite when avatar is idling
        envManage.LoadAvatarDataFromPlayerData(avatarIndex);
        if (state != Constant.AvatarState.IDLING && !isStateMatchScene())
        {
            Debug.Log("Avatar " + avatarIndex + " not idling, hide: " + state);
            this.gameObject.SetActive(false);
        }
    }

    protected void OnMouseDown()
    {
        if (this.state == Constant.AvatarState.IDLING)
        {
            isDragging = true;
        }

        if (isDragging)
        {
            Debug.Log("start dragging");
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
                    if (OnMouseUpActionTriggeredValidation(slot))
                    {
                        currentDropSlot = slot;
                        switch(avatarIndex)
                        {
                            case 0:
                                envManage.player.playerData.avatarData1.slotIndex = currentDropSlot.slotIndex;
                                break;
                            case 1:
                                envManage.player.playerData.avatarData2.slotIndex = currentDropSlot.slotIndex;
                                break;
                            case 2:
                                envManage.player.playerData.avatarData3.slotIndex = currentDropSlot.slotIndex;
                                break;
                        }
                        OnMouseUpActionTriggered();
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

    public virtual void LoadData(AvatarData avatarData, Slot slot)
    {
        this.state = avatarData.state;
        this.currentDropSlot = slot;
        if (slot != null)
        {
            ChangePosition(currentDropSlot.transformToBeSet.position);
            ChangeAnimation(ANIMATOR_IS_DROPPING_PROPERTY_NAME + currentDropSlot.level, true);
            ChangeAnimation(ANIMATOR_IS_DRAGGING_PROPERTY_NAME, false);
        }
        else
        {
            ChangeAnimation(ANIMATOR_IS_DRAGGING_PROPERTY_NAME, false);
            if (currentDropSlot != null)
            {
                ChangeAnimation(ANIMATOR_IS_DROPPING_PROPERTY_NAME + currentDropSlot.level, false);
                currentDropSlot = null;
            }
            ChangePosition(initialPosition);
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

    protected virtual bool OnMouseUpActionTriggeredValidation(Slot slot)
    {
        Debug.Log("Detected slot");
        return true;
    }

    protected virtual void OnMouseUpActionTriggered()
    {
        ChangePosition(currentDropSlot.transformToBeSet.position);
        ChangeAnimation(ANIMATOR_IS_DROPPING_PROPERTY_NAME + currentDropSlot.level, true);
        ChangeAnimation(ANIMATOR_IS_DRAGGING_PROPERTY_NAME, false);
        UpdateStateIfMouseUpTriggered();
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
        this.state = state;
        envManage.player.playerData.UpdateAvatarState(avatarIndex, state, false);
    }

    protected virtual void UpdateStateIfMouseUpTriggered()
    {
    }

    private bool isStateMatchScene()
    {
        Debug.Log(state + " " + envManage.sceneName.ToLower());
        switch (state)
        {
            case Constant.AvatarState.EATING:
                return envManage.sceneName.ToLower().Equals("cafe");
            case Constant.AvatarState.SLEEPING:
                return envManage.sceneName.ToLower().Equals("dorm");
            case Constant.AvatarState.WORKING:
                return envManage.sceneName.ToLower().Equals("factory");
        }
        return false;
    }
}
