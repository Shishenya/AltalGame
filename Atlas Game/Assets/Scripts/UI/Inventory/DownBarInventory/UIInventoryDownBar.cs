using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryDownBar : MonoBehaviour
{
    [SerializeField] private Sprite _transparentSprite = null; // ��������� ���������� ��������
    [SerializeField] private UIInventorySlot[] _inventorySlot = null; // ����� ������� ���������

    public GameObject draggedItemPrefab = null; // ������ ��� ��������, ������� ����� "�������"

    [HideInInspector] public GameObject inventoryTextBox; // ��������� ���� ��������� �� ��������

    private void OnEnable()
    {
        EventHandler.InventoryUpdateEvent += InventoryUpdate;
    }


    private void OnDisable()
    {
        EventHandler.InventoryUpdateEvent -= InventoryUpdate;
    }

    // ������� ������
    private void ClearInventorySlots()
    {
        for (int i = 0; i < _inventorySlot.Length; i++)
        {
            _inventorySlot[i].inventorySlotImage.sprite = _transparentSprite;
            _inventorySlot[i].textCountItems.text = "";
            _inventorySlot[i].itemDetails = null;
            _inventorySlot[i].itemQuantity = 0;

        }
    }

    // ���������� ��������� � ������ ����
    private void InventoryUpdate()
    {

        // ������ ������� ������
        ClearInventorySlots();

        // ���� ������ ������ ���� � ���� �������� � ���������
        if (_inventorySlot.Length> 0 && PlayerInventory.Instance.itemInPlayerInventory.Count>0)
        {
            // ���� �� ������
            for (int i = 0; i < _inventorySlot.Length; i++)
            {
                // ���� ������� ���� � ��������
                if (i < PlayerInventory.Instance.itemInPlayerInventory.Count)
                {
                    int itemCode = PlayerInventory.Instance.itemInPlayerInventory[i].itemCode; // ������� ���
                    ItemDetails itemDetails = ItemManager.Instance.GetItemDetails(itemCode); // ������� ������ ��������

                    // ���� ������� ���������� �� ���������� ������ � ������ ����������� ���
                    if (itemDetails!=null)
                    {
                        _inventorySlot[i].inventorySlotImage.sprite = itemDetails.itemSpriteArray[0];
                        _inventorySlot[i].textCountItems.text = PlayerInventory.Instance.itemInPlayerInventory[i].itemCount.ToString();
                        _inventorySlot[i].itemDetails = itemDetails;
                        _inventorySlot[i].itemQuantity = PlayerInventory.Instance.itemInPlayerInventory[i].itemCount;
                    } 
                    else
                    {
                        break;
                    }

                }
            }
        }
    }
}
