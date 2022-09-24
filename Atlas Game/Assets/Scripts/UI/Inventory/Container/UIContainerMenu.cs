using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIContainerMenu : MonoBehaviour
{

    [SerializeField] private Sprite _transparentSprite = null; // ��������� ���������� ��������
    [SerializeField] private UIContainerSlot[] _containerSlots = null; // ����� ����������
    [SerializeField] private UIInventoryBagSlot[] _inventorySlots = null; // ����� ���������

    public GameObject draggedItemPrefab = null; // ������ ��� ��������, ������� ����� "�������"

    [HideInInspector] public GameObject inventoryTextBox; // ��������� ���� ��������� �� ��������

    private void OnEnable()
    {
        ContainerItemsUpdate();
        InventoryUpdate();

        Player.Instance.PlayerInputIsDisable = true;
        EventHandler.InventoryUpdateEvent += ContainerItemsUpdate;
        EventHandler.InventoryUpdateEvent += InventoryUpdate;
    }

    private void OnDisable()
    {
        Player.Instance.PlayerInputIsDisable = false; 
        EventHandler.InventoryUpdateEvent -= ContainerItemsUpdate;
        EventHandler.InventoryUpdateEvent -= InventoryUpdate;
    }

    private void Start()
    {
        ContainerItemsUpdate();
        InventoryUpdate();
    }

    /// <summary>
    /// ������� ������ ����������
    /// </summary>
    private void ClearContainerSlots()
    {
        for (int i = 0; i < _containerSlots.Length; i++)
        {
            _containerSlots[i].spriteContainerSlot.sprite = _transparentSprite;
            _containerSlots[i].textCount.text = "";
            _containerSlots[i].itemDetails = null;
            _containerSlots[i].itemQuantity = 0;

        }
    }

    /// <summary>
    /// ������� ������ ���������
    /// </summary>
    private void ClearInventorySlots()
    {
        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            _inventorySlots[i].spriteInventorySlot.sprite = _transparentSprite;
            _inventorySlots[i].textCount.text = "";
            _inventorySlots[i].itemDetails = null;
            _inventorySlots[i].itemQuantity = 0;

        }
    }


    /// <summary>
    /// ���������� ������ ����������
    /// </summary>
    private void ContainerItemsUpdate()
    {

        // ������ ������� ������
        ClearContainerSlots();

        // ���� ������ ������ ���� � ���� �������� � ���������
        if (_containerSlots.Length > 0 && ContainerMenuManager.Instance.containerItems.Count > 0)
        {
            // ���� �� ������
            for (int i = 0; i < _containerSlots.Length; i++)
            {
                // ���� ������� ���� � ��������
                if (i < ContainerMenuManager.Instance.containerItems.Count)
                {
                    int itemCode = ContainerMenuManager.Instance.containerItems[i].itemCode; // ������� ���
                    ItemDetails itemDetails = ItemManager.Instance.GetItemDetails(itemCode); // ������� ������ ��������

                    // ���� ������� ���������� �� ���������� ������ � ������ ����������� ���
                    if (itemDetails != null)
                    {
                        _containerSlots[i].spriteContainerSlot.sprite = itemDetails.itemSpriteArray[0];
                        _containerSlots[i].textCount.text = ContainerMenuManager.Instance.containerItems[i].itemCount.ToString();
                        _containerSlots[i].itemDetails = itemDetails;
                        _containerSlots[i].itemQuantity = ContainerMenuManager.Instance.containerItems[i].itemCount;
                    }
                    else
                    {
                        break;
                    }

                }
            }
        }
    }


    /// <summary>
    /// ���������� ���������
    /// </summary>
    private void InventoryUpdate()
    {

        // ������ ������� ������
        ClearInventorySlots();

        // ���� ������ ������ ���� � ���� �������� � ���������
        if (_inventorySlots.Length > 0 && PlayerInventory.Instance.itemInPlayerInventory.Count > 0)
        {
            // ���� �� ������
            for (int i = 0; i < _inventorySlots.Length; i++)
            {
                // ���� ������� ���� � ��������
                if (i < PlayerInventory.Instance.itemInPlayerInventory.Count)
                {
                    int itemCode = PlayerInventory.Instance.itemInPlayerInventory[i].itemCode; // ������� ���
                    ItemDetails itemDetails = ItemManager.Instance.GetItemDetails(itemCode); // ������� ������ ��������

                    // ���� ������� ���������� �� ���������� ������ � ������ ����������� ���
                    if (itemDetails != null)
                    {
                        _inventorySlots[i].spriteInventorySlot.sprite = itemDetails.itemSpriteArray[0];
                        _inventorySlots[i].textCount.text = PlayerInventory.Instance.itemInPlayerInventory[i].itemCount.ToString();
                        _inventorySlots[i].itemDetails = itemDetails;
                        _inventorySlots[i].itemQuantity = PlayerInventory.Instance.itemInPlayerInventory[i].itemCount;
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
