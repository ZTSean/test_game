using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GeneralModalPanel : MonoBehaviour, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Button closeButton;
    public GameObject generalModalPanelObject;

    private static GeneralModalPanel generalModalPanel;

    /*
    public static GeneralModalPanel Instance()
    {
        if (!generalModalPanel)
        {
            generalModalPanel = FindObjectOfType(typeof(GeneralModalPanel)) as GeneralModalPanel;
            if (!generalModalPanel)
                Debug.LogError("There needs to be one active GeneralModalPanel script on a GameObject in your scene.");
        }

        return generalModalPanel;
    }

    public void Close (UnityAction closeEvent)
    {
        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(closeEvent);
        closeButton.onClick.AddListener(ClosePanel);

        closeButton.gameObject.SetActive(true);
    }

    void ClosePanel()
    {
        generalModalPanelObject.SetActive(false);
    }*/

    private bool mouseIsOver = false;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        //Close the Window on Deselect only if a click occurred outside this panel
        if (!mouseIsOver && generalModalPanelObject.activeSelf == true)
        {
            generalModalPanelObject.SetActive(false);
            Debug.Log(generalModalPanelObject.name + " is " + generalModalPanelObject.activeSelf);
        }
        else
        {
            Debug.Log(generalModalPanelObject.name + " is " + generalModalPanelObject.activeSelf);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseIsOver = true;
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseIsOver = false;
        EventSystem.current.SetSelectedGameObject(gameObject);
    }
}
