using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Scenario : MonoBehaviour
{
    public string sceneName;
    public bool isInUpgrading;
    

    private void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    private void Update()
    {
        if (!isInUpgrading && Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            PolygonCollider2D collider2D = this.gameObject.GetComponent<PolygonCollider2D>();

            if (collider2D.OverlapPoint(mousePosition))
            {
                LoadScene();
            }
        }
    }
}
