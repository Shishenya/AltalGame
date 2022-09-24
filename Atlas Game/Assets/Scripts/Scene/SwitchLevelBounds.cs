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
        // �������� ������� �� ����
        PolygonCollider2D polygonLevelBounds = GameObject.FindGameObjectWithTag(Tags.LevelBounds).GetComponent<PolygonCollider2D>();
        
        // ��������� ������� � ������
        CinemachineConfiner cinemachineConfiner = GetComponent<CinemachineConfiner>();
        cinemachineConfiner.m_BoundingShape2D = polygonLevelBounds;

        // ������� ���
        cinemachineConfiner.InvalidatePathCache();

    }

}
