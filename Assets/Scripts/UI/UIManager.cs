using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] TextMeshProUGUI score;

    [Header("Screens")]
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject endGameScreen;

    [Header("End Game")]
    [SerializeField] TextMeshProUGUI result;
    [SerializeField] TextMeshProUGUI finalScore;

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
        endGameScreen.SetActive(true);

        result.SetText("Level Completed!");

        finalScore.SetText(score.text);
    }

    public void GameOver()
    {
        endGameScreen.SetActive(true);

        result.SetText("You can´t do this");

        finalScore.SetText(score.text);
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

    public void Settings()
    {
        
    }

    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
