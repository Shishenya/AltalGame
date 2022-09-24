using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeItem : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Щапускаем тускнение
    /// </summary>
    public void FadeInItem()
    {
        StartCoroutine(FadeInItemRoutine());
    }

    /// <summary>
    /// Убираем тускнение
    /// </summary>
    public void FadeOutItem()
    {
        StartCoroutine(FadeOutItemRoutine()); ;
    }

    /// <summary>
    /// Корутина на туснение поредмета
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeInItemRoutine()
    {
        float currentAlpha = _spriteRenderer.color.a;
        float distance = 1f - currentAlpha;

        while (1f - currentAlpha > 0.01f)
        {
            currentAlpha = currentAlpha + distance / Settings.fadeInSeconds * Time.deltaTime;
            _spriteRenderer.color = new Color(1f, 1f, 1f, currentAlpha);
            yield return null;
        }

        _spriteRenderer.color = new Color(1f, 1f, 1f, 1f);

    }

    /// <summary>
    /// КОрутина на поялвение предмета
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeOutItemRoutine()
    {
        float currentAlpha = _spriteRenderer.color.a;
        float distance = currentAlpha - Settings.targetAlpha;

        while (currentAlpha - Settings.targetAlpha > 0.01f)
        {
            currentAlpha = currentAlpha - distance / Settings.fadeOutSeconds * Time.deltaTime;
            _spriteRenderer.color = new Color(1f, 1f, 1f, currentAlpha);
            yield return null;
        }

        _spriteRenderer.color = new Color(1f, 1f, 1f, Settings.targetAlpha);
    }
}
