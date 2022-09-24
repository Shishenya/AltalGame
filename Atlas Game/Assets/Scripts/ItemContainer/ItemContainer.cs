using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Предметы в контейнере 
/// </summary>

public class ItemContainer : MonoBehaviour
{


    private bool containerBeUsed = false; // открывался ли контейнер

    public int[] itemCodeItems = null; // предметы, которые могут появиться в инвентаре, пока его не открывали
    public int[] minCountItems = null; // минимальное количество этого предмета
    public int[] maxCountItems = null; // максимальное количество предметов в контейнере

    public List<int> currentItemCodeItems = null; // предметы, которые сейчас в контейнере
    public List<int> currentCountItems = null; // количество этих предметов

    [HideInInspector] public List<ItemInInventory> itemInContainer = null; // список предметов в контейрене


    private void Start()
    {
        // Если контейнер еще не открывался, то генерируем предметы внутри него
        if (!containerBeUsed)
        {
            GenerateRandonItemsInContainer(); // генерируем предметы
            itemInContainer = SetItemContainerList(); // создаем список
        }

        // если список предметов нулевой, то создаем его
        if (itemInContainer==null)
            itemInContainer = SetItemContainerList();

        EventHandler.CallInventoryUpdateEvent(); // обновляем инвентарь

    }

    /// <summary>
    /// Генерация предметов в контейнере
    /// </summary>
    private void GenerateRandonItemsInContainer()
    {

        // Идем по всем предметам и...
        for (int i = 0; i < itemCodeItems.Length; i++)
        {
            // генерируем количество предметов
            int randomCount = Random.Range(minCountItems[i], maxCountItems[i]);

            // Ставим Начальное количество предметов
            currentItemCodeItems.Add(itemCodeItems[i]);
            currentCountItems.Add(randomCount);

        }
    }

    /// <summary>
    ///  Создаем список предметов в контейнере
    /// </summary>
    /// <returns></returns>
    public List<ItemInInventory> SetItemContainerList()
    {

        List<ItemInInventory> itemsInCurrentContainer = new List<ItemInInventory>();
        ItemInInventory currentItem;

        for (int i = 0; i < currentItemCodeItems.Count; i++)
        {
            currentItem.itemCode = currentItemCodeItems[i];
            currentItem.itemCount = currentCountItems[i];
            itemsInCurrentContainer.Add(currentItem);
        }

        return itemsInCurrentContainer;

    }

}
