using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : Singleton<PlayerInventory>
{

    public List<ItemInInventory> itemInPlayerInventory;
    public int weaponCode = 0; // Код для активного оружия

    public GameObject weaponSlot = null; // слот под оружие в инвентаре

    protected override void Awake()
    {
        base.Awake();

        // Тест
        // Test();

    }

    // Тестовый 
    private void Test()
    {
        ItemInInventory ii1 = new ItemInInventory();
        ii1.itemCode = 10000;
        ii1.itemCount = 2;
        AddItemInPlayerInventory(ii1);

        ii1.itemCode = 10001;
        ii1.itemCount = 5;
        AddItemInPlayerInventory(ii1);

        ii1.itemCode = 10001;
        ii1.itemCount = 10;
        AddItemInPlayerInventory(ii1);

        DeleteItemInPlayerInventory(1,15);
    }

    /// <summary>
    /// Добавление предмета в инвентарь
    /// </summary>
    /// <param name="itemInInventory"></param>
    public void AddItemInPlayerInventory(ItemInInventory itemInInventory)
    {
        // Ищем позицию предмета
        int posItem = FindItemInPlayerInventory(itemInInventory.itemCode);

        // если найдена, то добаваляем количество
        if (posItem !=-1)
        {

            // Тут еще будет блок проверки максимального предмета в стаке
            // ---------------------------------
            // ---------------------------------

            ItemInInventory itemInInventoryOld = itemInPlayerInventory[posItem];
            itemInInventory.itemCount += itemInInventoryOld.itemCount;
            itemInPlayerInventory[posItem] = itemInInventory;
        } 
        // Иначе создаем новый предмет в инвентаре
        else
        {
            itemInPlayerInventory.Add(itemInInventory);
        }

        EventHandler.CallInventoryUpdateEvent(); // далем прозвон ивента

    }

    /// <summary>
    /// Удаление предмета из инвентаря игрока
    /// </summary>
    /// <param name="position">Позиция предмета</param>
    /// <param name="count">Количество</param>
    public void DeleteItemInPlayerInventory(int position, int count)
    {
        // Если такой индекс существует
        if (position >= 0 && position < itemInPlayerInventory.Count)
        {
            ItemInInventory itemInInventory = itemInPlayerInventory[position];
            int countNow = itemInInventory.itemCount; // Старое количество
            int countNew = countNow - count; // Новое количество

            if (countNew > 0 )
            {
                itemInInventory.itemCount = countNew;
                itemInPlayerInventory[position] = itemInInventory;
            } 
            else
            {
                itemInPlayerInventory.RemoveAt(position);
            }

        }

        // Обновляем инвентарь
        EventHandler.CallInventoryUpdateEvent();

    }

    /// <summary>
    ///  Поиск предмета в инвентаре с кодом
    /// </summary>
    /// <param name="itemCode">Код предмета</param>
    public int FindItemInPlayerInventory(int itemCode)
    {

        bool findItem = false;
        int indexItem = -1;


        for (int i = 0; i < itemInPlayerInventory.Count; i++)
        {
            if (itemCode == itemInPlayerInventory[i].itemCode)
            {
                findItem = true;
                indexItem = i;
            }
        }

        return indexItem;

    }

    /// <summary>
    /// Перемещение предметов между слотами
    /// </summary>
    /// <param name="slotNumber"></param>
    /// <param name="toSlotNumber"></param>
    public void SwapInventoryItems(int fromSlotNumber, int toSlotNumber)
    {
        // Если слоты можно менять
        if (fromSlotNumber<PlayerInventory.Instance.itemInPlayerInventory.Count &&
            toSlotNumber< PlayerInventory.Instance.itemInPlayerInventory.Count &&
            fromSlotNumber!=toSlotNumber &&
            fromSlotNumber >=0 &&
            toSlotNumber >=0)
        {

            ItemInInventory fromItem = itemInPlayerInventory[fromSlotNumber];
            ItemInInventory toItem = itemInPlayerInventory[toSlotNumber];

            // Здесь написать код, если у них совпадают коды предметов
            if (1==1)
            {

            }

            // Меняем местами
            itemInPlayerInventory[toSlotNumber] = fromItem;
            itemInPlayerInventory[fromSlotNumber] = toItem;


            // Обновляем нижний инвентарь
            EventHandler.CallInventoryUpdateEvent();
        }
    }

    public void TestPrint()
    {
        if (itemInPlayerInventory.Count > 0)
        {
            foreach (ItemInInventory itemInInventory in itemInPlayerInventory)
            {
                Debug.Log("Item: " + itemInInventory.itemCode + "; Count: " + itemInInventory.itemCount);
            }
        } 
        else
        {
            Debug.Log("Инвентарь пустой!");
        }

    }

    public void InstantiateWeaponSlot()
    {
        UIPauseMenuInventorySlot uIInventorySlot =  weaponSlot.GetComponent<UIPauseMenuInventorySlot>(); // поулчаем компонтент
        ItemDetails itemDetailsWeapon = ItemManager.Instance.GetItemDetails(weaponCode); // получаем детали оружия
        uIInventorySlot.itemDetails = itemDetailsWeapon; // задаем в слот оружие
        uIInventorySlot.itemQuantity = 1; // количство один
        weaponSlot.GetComponent<Image>().sprite = itemDetailsWeapon.itemSpriteArray[0]; // вставляем иконку в слот

    }

}
