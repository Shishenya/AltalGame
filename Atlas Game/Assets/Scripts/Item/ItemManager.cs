using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager: Singleton<ItemManager>
{


    [SerializeField] private GameObject _itemPrefab = null; // ������ ��������
    [SerializeField] private SO_ItemDetails so_ItemDetails = null;

    private Dictionary<int, ItemDetails> dictionaryItems;

    protected override void Awake()
    {
        base.Awake();

        // ������� ������� � ����������
        dictionaryItems = new Dictionary<int, ItemDetails>();
        CreateDictionaryItems();


    }

    /// <summary>
    /// ������������� �� ����� ��������
    /// </summary>
    /// <param name="positionInit"> ������� ������ �������� </param>
    /// <param name="itemCode"> ���������� ��� ��������</param>
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
    /// �������� ������� ���� ��������� � ����
    /// </summary>
    private void CreateDictionaryItems()
    {
        foreach (ItemDetails itemDetails in so_ItemDetails.itemDetails)
        {
            dictionaryItems.Add(itemDetails.itemCode, itemDetails);
        }
    }


    /// <summary>
    /// ��������� Item Details �������� �� ��� ����, null, ���� ����� �� ������
    /// </summary>
    /// <param name="itemCode">���������� ��� ��������</param>
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
