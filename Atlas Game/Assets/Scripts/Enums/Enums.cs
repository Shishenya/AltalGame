/// <summary>
/// Enums для игры
/// </summary>


// Тип предмета
public enum ItemType
{
    weapon, // оружие
    armor, // элемент доспеха
    container, // Контейнер
    fire, // элемент огня
    food, // еда
    other, // разное 
    tree, // дерево
    none, // нет типа
    count // финиш
} 

// Расширенный тип предмета - доспех
public enum ArmorType
{
    noarmor, // не является элементом доспеха
    body, // тело
    helmet, // голова
    gloves, // перчатки
    boots, // сапоги
    none, // нет
    count // финиш
}

// Расширенный тип предмета - оружие
public enum WeaponType
{
    noweapon, // не является оружием
    sword, // меч
    axe, // топор
    dagger, // кинжал
    bow, // лук
    none, // нет типа
    count // финиш
}

// Расширенный тип - еда
public enum FoodType
{
    nofood, // не является едой
    raw, // сырая еда
    freshFood, // свежая еда
    rottenFood, // протухшая еда
    water, // вода
    dirtyWater, // грязная вода
    none, // нет типа
    count // финиш
}

public enum LoadedScene
{
    Scene1_coast
}

// перечисление специальных слотов в инвентаре
public enum SpecialInventorySlot
{
    armor,
    weapon, 
    shield,
    none
}