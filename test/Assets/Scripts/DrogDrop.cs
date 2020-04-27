using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrogDrop : MonoBehaviour
{
    public GameObject tableSlot;
    public Sprite initialSprite;
    public Sprite draggedSprite;
    public Sprite actionSprite;
    Vector2 initialPosition;
    private bool isDragging;
    private float scale;

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
            ChangeSprite(draggedSprite);
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
                // TODO: change to seat sprite
                ChangeSprite(actionSprite);
            }
            else
            {
                Debug.Log("Reset to initial position");
                changePosition(initialPosition);
                ChangeSprite(initialSprite);
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

    private void ChangeSprite(Sprite sprite)
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    private void changePosition(Vector3 newPos)
    {
        Vector2 diff = newPos - transform.position;
        transform.Translate(diff);
    }
}
