using UnityEngine;
using Cinemachine;

public class SwitchLevelBounds : MonoBehaviour
{
    void Start()
    {
        SwitchBounds();
    }

    private void SwitchBounds()
    {
        // Получаем полигон по тегу
        PolygonCollider2D polygonLevelBounds = GameObject.FindGameObjectWithTag(Tags.LevelBounds).GetComponent<PolygonCollider2D>();
        
        // Вставляем полигон в камеру
        CinemachineConfiner cinemachineConfiner = GetComponent<CinemachineConfiner>();
        cinemachineConfiner.m_BoundingShape2D = polygonLevelBounds;

        // Очищаем кэш
        cinemachineConfiner.InvalidatePathCache();

    }

}
