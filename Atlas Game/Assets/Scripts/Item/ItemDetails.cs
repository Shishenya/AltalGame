using UnityEngine;

[System.Serializable]
public class ItemDetails
{
    public int itemCode; // Уникальный код
    public string itemDescription; // Описание
    public string itemLongDescription; // Длинное описание
    public Sprite[] itemSpriteArray; // Спрайты предмета

    public ItemType itemType; // тип предмета
    public ArmorType armorType; // расширенный тип - броня
    public WeaponType weaponType; // расширенный тип - оружие
    public FoodType foodType; // расширенный тип для еды

    public bool canBePickUp;

    public bool canBeStacked; // можно ли стакать предметы
    public int maxQuantityInStack; // Макстмальное количество в стаке

    // Оружие
    public int minDamageWeapon; // минимальный урон
    public int maxDamageWeapon; // Максимальный урон
    public float reloadWeapon; // перезарядка оружия

    // Доспехи
    public int defenseLevelArmor; // уровень защиты
    public float maxPointInArmor; // Максимальное количество поинтов в доспехе

    // Еда
    public int hourAfterFoodBecomesRotten; // Время в часах, после которого еда становится протухшей
    public int healthPointStandartFood; // количество HP, которое восстаналивает еда в станлартном виде
    public bool canUseRaw; // можно ли употреблять сырым
    public float multiplicateRawFood; // Мултипрликатор для сырой еды
    public bool canUseRotten; // Можно ли употреблять протухшую еду
    public float multiplicateRottenFood; // мултипликатор для протухшей еды

}
