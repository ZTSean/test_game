using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrogDrop : MonoBehaviour
{
    public GameObject tableSlot;
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
                this.gameObject.transform.position = tableSlot.transform.position;
                // TODO: change to seat sprite
            }
            else
            {
                Debug.Log("Reset to initial position");
                this.gameObject.transform.position = initialPosition;
            }
        }
        isDragging = false;
    }

    private void Update()
    {
        if (isDragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);
        }
    }
}
