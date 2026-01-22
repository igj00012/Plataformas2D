using UnityEngine;

public class BossDefeated : MonoBehaviour
{
    [SerializeField] UIManager uiManager;

    private void OnDestroy()
    {
        if (Application.isPlaying)
        {
            AudioManager.instance.StopMusic();

            uiManager.IsBossDefeated(true);
        }
    }
}
