using UnityEngine;
using UnityEngine.UI;

public class ChooseRole : MonoBehaviour
{
    public void SelectRole(string role)
    {
        if (role == "test")
        {
            // select role panel with selected color
            Debug.Log("Select role panel: " + this.gameObject.name);
            Button roleButton = this.gameObject.GetComponent<Button>();
            roleButton.Select();
        }
    }
}
