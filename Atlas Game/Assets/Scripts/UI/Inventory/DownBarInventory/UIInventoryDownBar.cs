using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryDownBar : MonoBehaviour
{
    [SerializeField] private Sprite _transparentSprite = null; // дефолтная прозрачная картинка
    [SerializeField] private UIInventorySlot[] _inventorySlot = null; // слоты нижнего инвентаря

    public GameObject draggedItemPrefab = null; // префаб для предмета, который будем "бросать"

    [HideInInspector] public GameObject inventoryTextBox; // Текстовой бокс подсказки по предмету

    private void OnEnable()
    {
        EventHandler.InventoryUpdateEvent += InventoryUpdate;
    }


    private void OnDisable()
    {
        EventHandler.InventoryUpdateEvent -= InventoryUpdate;
    }

    // Очистка слотов
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

    // Обновление инвентаря в нижнем баре
    private void InventoryUpdate()
    {

        // Делаем очистку слотов
        ClearInventorySlots();

        // если слотов больше нуля и есть предметы в инвентаре
        if (_inventorySlot.Length> 0 && PlayerInventory.Instance.itemInPlayerInventory.Count>0)
        {
            // Идем по слотам
            for (int i = 0; i < _inventorySlot.Length; i++)
            {
                // если предмет есть в инвенате
                if (i < PlayerInventory.Instance.itemInPlayerInventory.Count)
                {
                    int itemCode = PlayerInventory.Instance.itemInPlayerInventory[i].itemCode; // достаем код
                    ItemDetails itemDetails = ItemManager.Instance.GetItemDetails(itemCode); // достаем детали предмета

                    // если предмет существует то запихиваем детали в нижний инвентарный бар
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
