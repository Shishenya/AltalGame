using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuInventoryManager : MonoBehaviour
{
    [SerializeField] private UIPauseMenuInventorySlot[] _pauseMenuInventorySlot = null; // слоты
    [SerializeField] private Sprite transparent = null; // прозрачная картинка

    public GameObject inventoryManagementDraggedItemPrefab; // переносимый предмет
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
        // Показывае инвентарь
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
                    int itemCode = PlayerInventory.Instance.itemInPlayerInventory[i].itemCode; // достаем код
                    ItemDetails itemDetails = ItemManager.Instance.GetItemDetails(itemCode); // достаем детали предмета

                    if (itemDetails != null)
                    {
                        // Добавляем спрайт
                        _pauseMenuInventorySlot[i].spriteInventorySlot.sprite = itemDetails.itemSpriteArray[0];

                        // Добавляем количество
                        _pauseMenuInventorySlot[i].textCount.text = PlayerInventory.Instance.itemInPlayerInventory[i].itemCount.ToString();

                        // Добавляем инфу в слот
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
