using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuInventoryManager : MonoBehaviour
{
    [SerializeField] private UIPauseMenuInventorySlot[] _pauseMenuInventorySlot = null; // �����
    [SerializeField] private Sprite transparent = null; // ���������� ��������

    public GameObject inventoryManagementDraggedItemPrefab; // ����������� �������
    [HideInInspector] public GameObject inventoryTextBox; 


    private void OnEnable()
    {
        EventHandler.InventoryUpdateEvent += UpdateInventory;
        UpdateInventory();
    }

    private void OnDisable()
    {
        EventHandler.InventoryUpdateEvent -= UpdateInventory;
    }
    private void Start()
    {
        // ��������� ���������
        UpdateInventory();
    }

    public void UpdateInventory()
    {
        ClearInventorySlots();

        if (_pauseMenuInventorySlot.Length > 0 && PlayerInventory.Instance.itemInPlayerInventory.Count > 0)
        {

            for (int i = 0; i < _pauseMenuInventorySlot.Length; i++)
            {

                if (i < PlayerInventory.Instance.itemInPlayerInventory.Count)
                {
                    int itemCode = PlayerInventory.Instance.itemInPlayerInventory[i].itemCode; // ������� ���
                    ItemDetails itemDetails = ItemManager.Instance.GetItemDetails(itemCode); // ������� ������ ��������

                    if (itemDetails != null)
                    {
                        // ��������� ������
                        _pauseMenuInventorySlot[i].spriteInventorySlot.sprite = itemDetails.itemSpriteArray[0];

                        // ��������� ����������
                        _pauseMenuInventorySlot[i].textCount.text = PlayerInventory.Instance.itemInPlayerInventory[i].itemCount.ToString();

                        // ��������� ���� � ����
                        _pauseMenuInventorySlot[i].itemDetails = itemDetails;
                        _pauseMenuInventorySlot[i].itemQuantity = PlayerInventory.Instance.itemInPlayerInventory[i].itemCount;


                    }
                    else
                    {
                        break;
                    }
                }
                    
            }

        }
    }

    public void ClearInventorySlots()
    {

        for (int i = 0; i < _pauseMenuInventorySlot.Length; i++)
        {
            _pauseMenuInventorySlot[i].spriteInventorySlot.sprite = transparent;
            _pauseMenuInventorySlot[i].textCount.text = "";
            _pauseMenuInventorySlot[i].itemDetails = null;
            _pauseMenuInventorySlot[i].itemQuantity = 0;
        }

    }

}
