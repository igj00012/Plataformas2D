using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] AudioClip button;

    public void StartButton()
    {
        AudioManager.instance.PlaySFX(button);

        SceneManager.LoadScene(1);
    }

    public void QuitButton()
    {
        AudioManager.instance.PlaySFX(button);

        Application.Quit();
    }
}
