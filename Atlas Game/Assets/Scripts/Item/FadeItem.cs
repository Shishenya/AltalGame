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
    /// ��������� ���������
    /// </summary>
    public void FadeInItem()
    {
        StartCoroutine(FadeInItemRoutine());
    }

    /// <summary>
    /// ������� ���������
    /// </summary>
    public void FadeOutItem()
    {
        StartCoroutine(FadeOutItemRoutine()); ;
    }

    /// <summary>
    /// �������� �� �������� ���������
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
    /// �������� �� ��������� ��������
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
