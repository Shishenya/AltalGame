using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerMenuManager : Singleton<ContainerMenuManager>
{

    [HideInInspector]
    public List<ItemInInventory> containerItems; // �������� � ����������
    [HideInInspector] public ItemContainer currentItemContainer; // ������� ���������

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
    /// ���������� �������� � ���������
    /// </summary>
    /// <param name="itemInInventory"></param>
    public void AddItemInContainerInventory(ItemInInventory itemInInventory)
    {
        // ���� ������� ��������
        int posItem = FindItemInContainerInventory(itemInInventory.itemCode);

        // ���� �������, �� ���������� ����������
        if (posItem != -1)
        {

            // ��� ��� ����� ���� �������� ������������� �������� � �����
            // ---------------------------------
            // ---------------------------------

            ItemInInventory itemInInventoryOld = containerItems[posItem];
            itemInInventory.itemCount += itemInInventoryOld.itemCount;
            containerItems[posItem] = itemInInventory;
        }
        // ����� ������� ����� ������� � ���������
        else
        {
            containerItems.Add(itemInInventory);
        }

        EventHandler.CallInventoryUpdateEvent(); // ����� ������� ������

    }

    /// <summary>
    /// ����� � ����������
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
    /// ����� ������� ������ ����������
    /// </summary>
    /// <param name="fromSlotNumber"></param>
    /// <param name="toSlotNumber"></param>
    public void SwapContainersItems(int fromSlotNumber, int toSlotNumber)
    {
        // ���� ����� ����� ������
        if (fromSlotNumber < ContainerMenuManager.Instance.containerItems.Count &&
            toSlotNumber < ContainerMenuManager.Instance.containerItems.Count &&
            fromSlotNumber != toSlotNumber &&
            fromSlotNumber >= 0 &&
            toSlotNumber >= 0)
        {

            ItemInInventory fromItem = containerItems[fromSlotNumber];
            ItemInInventory toItem = containerItems[toSlotNumber];

            // ����� �������� ���, ���� � ��� ��������� ���� ���������
            if (1 == 1)
            {

            }

            // ������ ������� � UI
            containerItems[toSlotNumber] = fromItem;
            containerItems[fromSlotNumber] = toItem;

            // ������ ������� � ����������
            int tempItemCode = 0;
            int tempItemCount = 0;
            tempItemCode = currentItemContainer.currentItemCodeItems[fromSlotNumber];
            tempItemCount = currentItemContainer.currentCountItems[fromSlotNumber];

            currentItemContainer.currentItemCodeItems[fromSlotNumber] = currentItemContainer.currentItemCodeItems[toSlotNumber];
            currentItemContainer.currentCountItems[fromSlotNumber] = currentItemContainer.currentCountItems[toSlotNumber];

            currentItemContainer.currentItemCodeItems[toSlotNumber] = tempItemCode;
            currentItemContainer.currentCountItems[toSlotNumber] = tempItemCount;

            // ��������� ���������
            EventHandler.CallInventoryUpdateEvent();
        }
    }

    /// <summary>
    /// �������� �������� ������ ����������
    /// </summary>
    /// <param name="position"></param>
    /// <param name="count"></param>
    public void DeleteItemInContainer(int position, int count)
    {
        // ���� ����� ������ ����������
        if (position >= 0 && position < containerItems.Count)
        {
            ItemInInventory itemInInventory = containerItems[position];
            int countNow = itemInInventory.itemCount; // ������ ����������
            int countNew = countNow - count; // ����� ����������

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

        // ��������� ���������
       EventHandler.CallInventoryUpdateEvent();

    }

}
