using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    [Header("Hearts")]
    [SerializeField] Image[] hearts;

    [Header("Heart images")]
    [SerializeField] Sprite fullHeart;

    [SerializeField] AudioClip hit;

    private int maxHealth = 5;

    public void UpdateHP(int currentHP)
    {
        for (int i = hearts.Length - 1; i >= 0; --i)
        {
            if (i < currentHP)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].color = Color.black;

                //AudioManager.instance.PlaySFX(hit);
            }
        }
    }

    public float GetMaxHealth() { return maxHealth; }
}
