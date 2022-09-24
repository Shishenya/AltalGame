using UnityEngine;

public static class Settings
{
    // Отступы для тела игрока, чобы правильо определитьего координаты в сетке
    public static float xIndent = 0f;
    public static float yIndent = 0f;


    // Затухание предмета
    public const float fadeInSeconds = 0.3f;
    public const float fadeOutSeconds = 0.3f;
    public const float targetAlpha = 0.45f;

    // Подбор и перенос предметов
    public const float distancePickUpItem = 1f; // Расстояние, на котором можно взять предмет
    public const float distanceThrowItem = 2.5f; // Расстояние, на которое можно юроситьпредмет
    public static Color greenColor = new Color(0f,255f,0f, 200f); // Для предмета, который можно положить / бросить
    public static Color redColor = new Color(255f, 0f, 0f, 200f); // Для предмета, который нельзя положить / бросить

    // Еда
    public const float eatingFoodTime = 1f;

}
