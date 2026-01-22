using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] TextMeshProUGUI timer;

    [Header("Screens")]
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject endGameScreen;

    [Header("End Game")]
    [SerializeField] TextMeshProUGUI result;

    [SerializeField] AudioClip music;

    [SerializeField] AudioClip button;

    bool bossDefeated = false;

    void Start()
    {
        pauseMenu.SetActive(false);
        endGameScreen.SetActive(false);

        AudioManager.instance.PlayMusic(music);
    }

    int initialTime = 300;
    float timeIndex = 0f;
    void Update()
    {
        timer.SetText($"{initialTime}");
        timeIndex += Time.deltaTime;

        if (timeIndex >= 1)
        {
            initialTime -= 1;
            timeIndex = 0f;
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            PauseGame();
        }

        if (initialTime <= 0 && !bossDefeated)
        {
            GameOver();
        }
        else if (bossDefeated)
        {
            WinGame();
        }
    }

    void PauseGame()
    {
        pauseMenu.SetActive(true);

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void WinGame()
    {
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        endGameScreen.SetActive(true);

        result.SetText("Well done!");
    }

    public void GameOver()
    {
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        endGameScreen.SetActive(true);

        result.SetText("You die!");
    }

    public void Resume()
    {
        if (pauseMenu.activeInHierarchy)
        {
            AudioManager.instance.PlaySFX(button);

            pauseMenu.SetActive(false);

            Time.timeScale = 1f;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void Exit()
    {
        AudioManager.instance.PlaySFX(button);

        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        AudioManager.instance.PlaySFX(button);

        SceneManager.LoadScene(1);

        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void IsBossDefeated(bool defeated)
    {
        bossDefeated = defeated;
    }
}
