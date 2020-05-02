using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropEating : MonoBehaviour
{
    public GameObject tableSlot;
    public Animator animator;
    Vector2 initialPosition;
    private bool isDragging;

    void Start()
    {
        initialPosition = this.gameObject.transform.position;
    }

    private void OnMouseDown()
    {
        Debug.Log("start dragging");
        isDragging = true;
        if (isDragging)
        {
            ChangeAnimation("isDragging", true);
        }
    }

    private void OnMouseUp()
    {
        if (isDragging)
        {
            Debug.Log("Release mouse");
            PolygonCollider2D polygonCollider2D = tableSlot.GetComponent<PolygonCollider2D>();
            if (polygonCollider2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
            {
                Debug.Log("Detect table");
                changePosition(tableSlot.transform.position);
                ChangeAnimation("isEating", true);
                ChangeAnimation("isDragging", false);
            }
            else
            {
                Debug.Log("Reset to initial position");
                changePosition(initialPosition);
                ChangeAnimation("isInitial", true);
                ChangeAnimation("isDragging", false);
            }
        }
        isDragging = false;
    }

    private void Update()
    {
        if (isDragging)
        {
            changePosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    private void ChangeAnimation(string flagName, bool value)
    {
        animator.SetBool(flagName, value);
    }

    private void changePosition(Vector3 newPos)
    {
        Vector2 diff = newPos - transform.position;
        transform.Translate(diff);
    }
}
