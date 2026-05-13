using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public GameObject modesPanel;
    public void LoadGameScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenModesPanel()
    {
        modesPanel?.SetActive(true);
    }

    public void CloseModesPanel()
    {
        modesPanel?.SetActive(false);
    }
}
