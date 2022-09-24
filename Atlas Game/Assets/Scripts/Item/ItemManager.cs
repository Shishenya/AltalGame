using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager: Singleton<ItemManager>
{


    [SerializeField] private GameObject _itemPrefab = null; // префаб предмета
    [SerializeField] private SO_ItemDetails so_ItemDetails = null;

    private Dictionary<int, ItemDetails> dictionaryItems;

    protected override void Awake()
    {
        base.Awake();

        // Создаем словарь с предметами
        dictionaryItems = new Dictionary<int, ItemDetails>();
        CreateDictionaryItems();


    }

    /// <summary>
    /// Инициализация на сцене предмета
    /// </summary>
    /// <param name="positionInit"> Позиция спавна предмета </param>
    /// <param name="itemCode"> Уникальный код предмета</param>
    public void InitItem(Vector3 positionInit, int itemCode)
    {

        ItemDetails itemDetails = GetItemDetails(itemCode);
        if (itemDetails!=null)
        {

            GameObject go = Instantiate(_itemPrefab, positionInit, Quaternion.identity);
            SpriteRenderer spriteGO = go.GetComponentInChildren<SpriteRenderer>();
            spriteGO.sprite = itemDetails.itemSpriteArray[itemDetails.itemSpriteArray.Length - 1];
        }

    }

    /// <summary>
    /// Создание словаря всех предметов в игре
    /// </summary>
    private void CreateDictionaryItems()
    {
        foreach (ItemDetails itemDetails in so_ItemDetails.itemDetails)
        {
            dictionaryItems.Add(itemDetails.itemCode, itemDetails);
        }
    }


    /// <summary>
    /// Получание Item Details предмета по его коду, null, если такой не найден
    /// </summary>
    /// <param name="itemCode">Уникальный код предмета</param>
    /// <returns></returns>
    public ItemDetails GetItemDetails(int itemCode)
    {

        if (dictionaryItems.TryGetValue(itemCode, out ItemDetails itemDetails))
        {
            return itemDetails;
        } else
        {
            return null;
        }

    }

}
