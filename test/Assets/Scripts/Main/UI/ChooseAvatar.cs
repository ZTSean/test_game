using UnityEngine;
using UnityEngine.UI;

public class ChooseAvatar : MonoBehaviour
{
    public AdventureModalPanel adventureModalPanel;
    public ChooseAvatar otherChooseAvatar;
    public ChooseAvatar otherChooseAvatar2;
    public int avatarIndex = -1;
    private bool isAvailable = true;

    public void OnEnable()
    {
        // TODO: blocked out this gameobject for onclick when avatar is NOT available
        isAvailable = adventureModalPanel.isEnoughSanityAndHungry(adventureModalPanel.selectedIndexAdventure, avatarIndex);
        Image image = this.gameObject.GetComponent<Image>();
        if (isAvailable)
        {
            image.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            image.color = new Color(.5f, .5f, .5f, 1f);
        }
        this.gameObject.GetComponent<Button>().enabled = isAvailable;
        adventureModalPanel.UpdateGoButton(isAvailable);
    }

    public void SelectAvatar()
    {
        if (isAvailable)
        {
            // Update global adevnture modal panel properties
            adventureModalPanel.SetSelectedIndexAvatar(avatarIndex);

            // select role panel with selected color
            Debug.Log("Select role panel: " + this.gameObject.name);
            Button roleButton = this.gameObject.GetComponent<Button>();
            roleButton.Select();

            Image image = this.gameObject.GetComponent<Image>();
            image.color = new Color(1f, 1f, 1f, .5f);
            ResetAllChooseAvatar();
        }
    }

    private void ResetAllChooseAvatar()
    {
        otherChooseAvatar.OnEnable();
        otherChooseAvatar2.OnEnable();
    }
}
