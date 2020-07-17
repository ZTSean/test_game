using UnityEngine;
using UnityEngine.UI;

public class ChooseAvatar : MonoBehaviour
{
    public AdventureModalPanel adventureModalPanel;
    public int avatarIndex = -1;

    private void Start()
    {
        // TODO: blocked out this gameobject for onclick when avatar is NOT available
    }

    public void SelectAvatar()
    {
        // Update global adevnture modal panel properties
        adventureModalPanel.SetSelectedIndexAvatar(avatarIndex);

        // select role panel with selected color
        Debug.Log("Select role panel: " + this.gameObject.name);
        Button roleButton = this.gameObject.GetComponent<Button>();
        roleButton.Select();

        Image image = this.gameObject.GetComponent<Image>();
        image.color = new Color(1f, 1f, 1f, .5f);
    }
}
