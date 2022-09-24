using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �������� � ���������� 
/// </summary>

public class ItemContainer : MonoBehaviour
{


    private bool containerBeUsed = false; // ���������� �� ���������

    public int[] itemCodeItems = null; // ��������, ������� ����� ��������� � ���������, ���� ��� �� ���������
    public int[] minCountItems = null; // ����������� ���������� ����� ��������
    public int[] maxCountItems = null; // ������������ ���������� ��������� � ����������

    public List<int> currentItemCodeItems = null; // ��������, ������� ������ � ����������
    public List<int> currentCountItems = null; // ���������� ���� ���������

    [HideInInspector] public List<ItemInInventory> itemInContainer = null; // ������ ��������� � ����������


    private void Start()
    {
        // ���� ��������� ��� �� ����������, �� ���������� �������� ������ ����
        if (!containerBeUsed)
        {
            GenerateRandonItemsInContainer(); // ���������� ��������
            itemInContainer = SetItemContainerList(); // ������� ������
        }

        // ���� ������ ��������� �������, �� ������� ���
        if (itemInContainer==null)
            itemInContainer = SetItemContainerList();

        EventHandler.CallInventoryUpdateEvent(); // ��������� ���������

    }

    /// <summary>
    /// ��������� ��������� � ����������
    /// </summary>
    private void GenerateRandonItemsInContainer()
    {

        // ���� �� ���� ��������� �...
        for (int i = 0; i < itemCodeItems.Length; i++)
        {
            // ���������� ���������� ���������
            int randomCount = Random.Range(minCountItems[i], maxCountItems[i]);

            // ������ ��������� ���������� ���������
            currentItemCodeItems.Add(itemCodeItems[i]);
            currentCountItems.Add(randomCount);

        }
    }

    /// <summary>
    ///  ������� ������ ��������� � ����������
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
