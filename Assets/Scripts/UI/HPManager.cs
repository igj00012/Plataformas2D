using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    [Header("Hearts")]
    [SerializeField] Image[] hearts;


    [SerializeField] AudioClip hit;

    public void UpdateHP(int currentHP)
    {
        for (int i = hearts.Length - 1; i >= 0; --i)
        {
            if (i < currentHP)
            {
                hearts[i].color = Color.white;
            }
            else
            {
                hearts[i].color = Color.black;

                //AudioManager.instance.PlaySFX(hit);
            }
        }
    }
}
