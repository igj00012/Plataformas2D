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

    void Start()
    {
        pauseMenu.SetActive(false);
        endGameScreen.SetActive(false);
    }

    int initialTime = 999;
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

        result.SetText("Level Completed!");
    }

    public void GameOver()
    {
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        endGameScreen.SetActive(true);

        result.SetText("You can´t do this");
    }

    public void Resume()
    {
        if (pauseMenu.activeInHierarchy)
        {
            pauseMenu.SetActive(false);

            Time.timeScale = 1f;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(1);
    }
}
