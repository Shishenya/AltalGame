using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : Singleton<PlayerInventory>
{

    public List<ItemInInventory> itemInPlayerInventory;
    public int weaponCode = 0; // ��� ��� ��������� ������

    public GameObject weaponSlot = null; // ���� ��� ������ � ���������

    protected override void Awake()
    {
        base.Awake();

        // ����
        // Test();

    }

    // �������� 
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
    /// ���������� �������� � ���������
    /// </summary>
    /// <param name="itemInInventory"></param>
    public void AddItemInPlayerInventory(ItemInInventory itemInInventory)
    {
        // ���� ������� ��������
        int posItem = FindItemInPlayerInventory(itemInInventory.itemCode);

        // ���� �������, �� ���������� ����������
        if (posItem !=-1)
        {

            // ��� ��� ����� ���� �������� ������������� �������� � �����
            // ---------------------------------
            // ---------------------------------

            ItemInInventory itemInInventoryOld = itemInPlayerInventory[posItem];
            itemInInventory.itemCount += itemInInventoryOld.itemCount;
            itemInPlayerInventory[posItem] = itemInInventory;
        } 
        // ����� ������� ����� ������� � ���������
        else
        {
            itemInPlayerInventory.Add(itemInInventory);
        }

        EventHandler.CallInventoryUpdateEvent(); // ����� ������� ������

    }

    /// <summary>
    /// �������� �������� �� ��������� ������
    /// </summary>
    /// <param name="position">������� ��������</param>
    /// <param name="count">����������</param>
    public void DeleteItemInPlayerInventory(int position, int count)
    {
        // ���� ����� ������ ����������
        if (position >= 0 && position < itemInPlayerInventory.Count)
        {
            ItemInInventory itemInInventory = itemInPlayerInventory[position];
            int countNow = itemInInventory.itemCount; // ������ ����������
            int countNew = countNow - count; // ����� ����������

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

        // ��������� ���������
        EventHandler.CallInventoryUpdateEvent();

    }

    /// <summary>
    ///  ����� �������� � ��������� � �����
    /// </summary>
    /// <param name="itemCode">��� ��������</param>
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
    /// ����������� ��������� ����� �������
    /// </summary>
    /// <param name="slotNumber"></param>
    /// <param name="toSlotNumber"></param>
    public void SwapInventoryItems(int fromSlotNumber, int toSlotNumber)
    {
        // ���� ����� ����� ������
        if (fromSlotNumber<PlayerInventory.Instance.itemInPlayerInventory.Count &&
            toSlotNumber< PlayerInventory.Instance.itemInPlayerInventory.Count &&
            fromSlotNumber!=toSlotNumber &&
            fromSlotNumber >=0 &&
            toSlotNumber >=0)
        {

            ItemInInventory fromItem = itemInPlayerInventory[fromSlotNumber];
            ItemInInventory toItem = itemInPlayerInventory[toSlotNumber];

            // ����� �������� ���, ���� � ��� ��������� ���� ���������
            if (1==1)
            {

            }

            // ������ �������
            itemInPlayerInventory[toSlotNumber] = fromItem;
            itemInPlayerInventory[fromSlotNumber] = toItem;


            // ��������� ������ ���������
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
            Debug.Log("��������� ������!");
        }

    }

    public void InstantiateWeaponSlot()
    {
        UIPauseMenuInventorySlot uIInventorySlot =  weaponSlot.GetComponent<UIPauseMenuInventorySlot>(); // �������� ����������
        ItemDetails itemDetailsWeapon = ItemManager.Instance.GetItemDetails(weaponCode); // �������� ������ ������
        uIInventorySlot.itemDetails = itemDetailsWeapon; // ������ � ���� ������
        uIInventorySlot.itemQuantity = 1; // ��������� ����
        weaponSlot.GetComponent<Image>().sprite = itemDetailsWeapon.itemSpriteArray[0]; // ��������� ������ � ����

    }

}
