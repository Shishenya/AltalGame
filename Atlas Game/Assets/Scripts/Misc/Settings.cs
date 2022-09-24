using UnityEngine;

public static class Settings
{
    // ������� ��� ���� ������, ���� �������� ������������� ���������� � �����
    public static float xIndent = 0f;
    public static float yIndent = 0f;


    // ��������� ��������
    public const float fadeInSeconds = 0.3f;
    public const float fadeOutSeconds = 0.3f;
    public const float targetAlpha = 0.45f;

    // ������ � ������� ���������
    public const float distancePickUpItem = 1f; // ����������, �� ������� ����� ����� �������
    public const float distanceThrowItem = 2.5f; // ����������, �� ������� ����� ��������������
    public static Color greenColor = new Color(0f,255f,0f, 200f); // ��� ��������, ������� ����� �������� / �������
    public static Color redColor = new Color(255f, 0f, 0f, 200f); // ��� ��������, ������� ������ �������� / �������

    // ���
    public const float eatingFoodTime = 1f;

}
