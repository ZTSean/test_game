using UnityEngine;
using UnityEngine.SceneManagement;


public class EnterGame : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void NewGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}
