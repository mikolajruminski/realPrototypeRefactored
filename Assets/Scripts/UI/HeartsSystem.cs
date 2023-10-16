using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartsSystem : MonoBehaviour
{
    private int max_player_Health;
    private int current_player_Health;

    [SerializeField] private Sprite fullHeart, emptyHeart;
    [SerializeField] private Image[] hearts;

    private void Start()
    {
        max_player_Health = PlayerDamage.Instance.GetMaxPlayerHealth();
    }

    private void Update() 
    {
        current_player_Health = PlayerDamage.Instance.Health;
        AdjustHearts();
    }

    private void AdjustHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < current_player_Health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < max_player_Health)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
