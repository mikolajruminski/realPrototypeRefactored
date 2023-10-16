using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitFlash : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Color flashColor = new Color(255, 255, 255);
    // Start is called before the first frame update
    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        PlayerDamage.Instance.OnGetDamaged += Player_Instance_OnGetDamaged;
    }

    private void Player_Instance_OnGetDamaged(object sender, EventArgs e)
    {
        StartCoroutine(PlayerHitFlashing(4));
    }

    private IEnumerator PlayerHitFlashing(int howManyFlashes)
    {
        int currentHitFlashes = howManyFlashes;

        while (currentHitFlashes > 0)
        {
            spriteRenderer.color = flashColor;
            yield return null;
            spriteRenderer.color = originalColor;
            yield return new WaitForEndOfFrame();
            yield return null;
            currentHitFlashes--;
        }
    }
}
