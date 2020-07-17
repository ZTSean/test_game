using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalAdventurePanel : MonoBehaviour
{
    public Text finalAdventureText;
    public AdventureModalPanel adventureModalPanel;

    public void OnEnable()
    {
        finalAdventureText.text = adventureModalPanel.FinalAdventureText();
    }
}
