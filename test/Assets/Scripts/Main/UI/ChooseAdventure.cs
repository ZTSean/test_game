using UnityEngine;
using UnityEngine.UI;

public class ChooseAdventure : MonoBehaviour
{
    public AdventureModalPanel adventureModalPanel;
    public int adventureIndex = -1;
    public Text description;
    public Text resource;

    public void SelectAdventure()
    {
        Adventure adventure = adventureModalPanel.DetermineAdventure(adventureIndex);

        // update adventure description
        description.text = adventure.description;
        Debug.Log("Select adventure description: " + description.text);

        //TODO: update adventure resoures based on items
        resource.text = "aaa";
        Debug.Log("Select adventure resource: " + resource.text);

        // Update global adevnture modal panel properties
        adventureModalPanel.SetSelectedIndexAdventure(adventure.index);

        // select button with selected color
        Debug.Log("Select button: " + this.gameObject.name);
        Button adventureButton = this.gameObject.GetComponent<Button>();
        adventureButton.Select();

        Image image = this.gameObject.GetComponent<Image>();
        image.color = new Color(1f, 1f, 1f, .5f);
    }
}
