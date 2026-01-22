using UnityEngine;

public class BossDefeated : MonoBehaviour
{
    [SerializeField] UIManager uiManager;

    private void OnDestroy()
    {
        uiManager.WinGame();    
    }
}
