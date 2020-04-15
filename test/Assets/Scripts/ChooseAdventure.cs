using UnityEngine;
using UnityEngine.UI;

public class ChooseAdventure : MonoBehaviour
{
    public Text description;
    public Text resource;

    public void SelectAdventure(string adventure)
    {
        if (adventure == "test")
        {
            // update adventure description
            Debug.Log("Select adventure description: " + description.text);
            description.text = "aaa";

            // update adventure resoures
            Debug.Log("Select adventure resource: " + resource.text);
            resource.text = "aaa";

            // select button with selected color
            Debug.Log("Select button: " + this.gameObject.name);
            Button adventureButton = this.gameObject.GetComponent<Button>();
            adventureButton.Select();
        }
    }
}
