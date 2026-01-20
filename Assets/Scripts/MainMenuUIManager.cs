using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    public void Start()
    {
        SceneManager.LoadScene(1);
    }

    public void Settings()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }
}
