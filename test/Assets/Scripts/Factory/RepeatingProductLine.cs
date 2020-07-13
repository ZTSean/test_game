using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingProductLine : MonoBehaviour
{
    float scrollSpeed = 5.1f;
    Vector2 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float newPos = Mathf.Repeat(Time.time * scrollSpeed, 12.48f);
        transform.position = startPos + Vector2.right * newPos;
    }
}
