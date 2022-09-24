using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerMenuManager : Singleton<ContainerMenuManager>
{

    [HideInInspector]
    public List<ItemInInventory> containerItems; // предметы в контейнере
    [HideInInspector] public ItemContainer currentItemContainer; // текущий контейнер

    protected override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    public void InitialiseItemsInContainer()
    {

    }

    /// <summary>
    /// Добавление предмета в контейнер
    /// </summary>
    /// <param name="itemInInventory"></param>
    public void AddItemInContainerInventory(ItemInInventory itemInInventory)
    {
        // Ищем позицию предмета
        int posItem = FindItemInContainerInventory(itemInInventory.itemCode);

        // если найдена, то добаваляем количество
        if (posItem != -1)
        {

            // Тут еще будет блок проверки максимального предмета в стаке
            // ---------------------------------
            // ---------------------------------

            ItemInInventory itemInInventoryOld = containerItems[posItem];
            itemInInventory.itemCount += itemInInventoryOld.itemCount;
            containerItems[posItem] = itemInInventory;
        }
        // Иначе создаем новый предмет в инвентаре
        else
        {
            containerItems.Add(itemInInventory);
        }

        EventHandler.CallInventoryUpdateEvent(); // далем прозвон ивента

    }

    /// <summary>
    /// Поиск в контейнере
    /// </summary>
    /// <param name="itemCode"></param>
    /// <returns></returns>
    public int FindItemInContainerInventory(int itemCode)
    {

        bool findItem = false;
        int indexItem = -1;


        for (int i = 0; i < containerItems.Count; i++)
        {
            if (itemCode == containerItems[i].itemCode)
            {
                findItem = true;
                indexItem = i;
            }
        }

        return indexItem;

    }

    /// <summary>
    /// Смена позиции внутри контейнера
    /// </summary>
    /// <param name="fromSlotNumber"></param>
    /// <param name="toSlotNumber"></param>
    public void SwapContainersItems(int fromSlotNumber, int toSlotNumber)
    {
        // Если слоты можно менять
        if (fromSlotNumber < ContainerMenuManager.Instance.containerItems.Count &&
            toSlotNumber < ContainerMenuManager.Instance.containerItems.Count &&
            fromSlotNumber != toSlotNumber &&
            fromSlotNumber >= 0 &&
            toSlotNumber >= 0)
        {

            ItemInInventory fromItem = containerItems[fromSlotNumber];
            ItemInInventory toItem = containerItems[toSlotNumber];

            // Здесь написать код, если у них совпадают коды предметов
            if (1 == 1)
            {

            }

            // Меняем местами в UI
            containerItems[toSlotNumber] = fromItem;
            containerItems[fromSlotNumber] = toItem;

            // Меняем местами в контейнере
            int tempItemCode = 0;
            int tempItemCount = 0;
            tempItemCode = currentItemContainer.currentItemCodeItems[fromSlotNumber];
            tempItemCount = currentItemContainer.currentCountItems[fromSlotNumber];

            currentItemContainer.currentItemCodeItems[fromSlotNumber] = currentItemContainer.currentItemCodeItems[toSlotNumber];
            currentItemContainer.currentCountItems[fromSlotNumber] = currentItemContainer.currentCountItems[toSlotNumber];

            currentItemContainer.currentItemCodeItems[toSlotNumber] = tempItemCode;
            currentItemContainer.currentCountItems[toSlotNumber] = tempItemCount;

            // Обновляем инвентари
            EventHandler.CallInventoryUpdateEvent();
        }
    }

    /// <summary>
    /// Удаление предмета внутри контейнера
    /// </summary>
    /// <param name="position"></param>
    /// <param name="count"></param>
    public void DeleteItemInContainer(int position, int count)
    {
        // Если такой индекс существует
        if (position >= 0 && position < containerItems.Count)
        {
            ItemInInventory itemInInventory = containerItems[position];
            int countNow = itemInInventory.itemCount; // Старое количество
            int countNew = countNow - count; // Новое количество

            // currentItemContainer.currentCountItems[position] = countNew;

            if (countNew > 0)
            {
                itemInInventory.itemCount = countNew;
                containerItems[position] = itemInInventory;
            }
            else
            {

                containerItems.RemoveAt(position);
                currentItemContainer.itemInContainer = containerItems;
                // EventHandler.CallInventoryUpdateEvent();

            }

        }

        // Обновляем инвентарь
       EventHandler.CallInventoryUpdateEvent();

    }

}
