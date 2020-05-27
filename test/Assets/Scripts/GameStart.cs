using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public void StartNewGame()
    {
        PlayerData newPlayerData = new PlayerData();
        SaveSystem.SavePlayer(newPlayerData);
        SceneManager.LoadScene(1); // load main scene
    }

    public void LoadGame()
    {
        if (SaveSystem.LoadPlayer() != null)
        {
            SceneManager.LoadScene(1); // load main scene
        }
    }
}
