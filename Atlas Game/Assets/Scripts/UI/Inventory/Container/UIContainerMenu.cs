using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIContainerMenu : MonoBehaviour
{

    [SerializeField] private Sprite _transparentSprite = null; // дефолтная прозрачная картинка
    [SerializeField] private UIContainerSlot[] _containerSlots = null; // слоты контейнера
    [SerializeField] private UIInventoryBagSlot[] _inventorySlots = null; // слоты инвентаря

    public GameObject draggedItemPrefab = null; // префаб для предмета, который будем "бросать"

    [HideInInspector] public GameObject inventoryTextBox; // Текстовой бокс подсказки по предмету

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
    /// Очистка слотов контейнера
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
    /// Очистка слотов инвентаря
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
    /// Обновление слотов контейнера
    /// </summary>
    private void ContainerItemsUpdate()
    {

        // Делаем очистку слотов
        ClearContainerSlots();

        // если слотов больше нуля и есть предметы в инвентаре
        if (_containerSlots.Length > 0 && ContainerMenuManager.Instance.containerItems.Count > 0)
        {
            // Идем по слотам
            for (int i = 0; i < _containerSlots.Length; i++)
            {
                // если предмет есть в инвенате
                if (i < ContainerMenuManager.Instance.containerItems.Count)
                {
                    int itemCode = ContainerMenuManager.Instance.containerItems[i].itemCode; // достаем код
                    ItemDetails itemDetails = ItemManager.Instance.GetItemDetails(itemCode); // достаем детали предмета

                    // если предмет существует то запихиваем детали в нижний инвентарный бар
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
    /// Обновление инвентаря
    /// </summary>
    private void InventoryUpdate()
    {

        // Делаем очистку слотов
        ClearInventorySlots();

        // если слотов больше нуля и есть предметы в инвентаре
        if (_inventorySlots.Length > 0 && PlayerInventory.Instance.itemInPlayerInventory.Count > 0)
        {
            // Идем по слотам
            for (int i = 0; i < _inventorySlots.Length; i++)
            {
                // если предмет есть в инвенате
                if (i < PlayerInventory.Instance.itemInPlayerInventory.Count)
                {
                    int itemCode = PlayerInventory.Instance.itemInPlayerInventory[i].itemCode; // достаем код
                    ItemDetails itemDetails = ItemManager.Instance.GetItemDetails(itemCode); // достаем детали предмета

                    // если предмет существует то запихиваем детали в нижний инвентарный бар
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
