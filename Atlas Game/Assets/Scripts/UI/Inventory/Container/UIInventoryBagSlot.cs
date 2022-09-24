using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UIInventoryBagSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public TextMeshProUGUI textCount = null; // Количество
    public Image spriteInventorySlot = null; // Картинка

    private Canvas _parentCanvas; // Канвас родителя
    private GameObject _draggedItemGO; // перетаскиваемый предмет

    [SerializeField] UIContainerMenu uiContainerMenu = null; //  
    [SerializeField] private int _slotNumber = 0; // 

    [HideInInspector] public ItemDetails itemDetails = null;
    [HideInInspector] public int itemQuantity;

    private void Start()
    {
        _parentCanvas = GetComponentInParent<Canvas>();
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        if (itemDetails != null)
        {


            // Блоикируем управление
            Player.Instance.DisablePlayerInput();

            _draggedItemGO = Instantiate(uiContainerMenu.draggedItemPrefab, uiContainerMenu.transform);

            // Добавляем предмету картинку
            Image draggedItemImage = _draggedItemGO.GetComponentInChildren<Image>();
            draggedItemImage.sprite = spriteInventorySlot.sprite;

        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_draggedItemGO != null)
        {
            _draggedItemGO.transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {


        Destroy(_draggedItemGO); // уничтожаем переносмимый предмет

        if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.GetComponent<UIInventoryBagSlot>() != null)
        {
            // если попали в слот для инвентаря

            // Определяем в какой слот попали
            int toSlotDragged = eventData.pointerCurrentRaycast.gameObject.GetComponent<UIInventoryBagSlot>()._slotNumber;

            // Меняем предметы местами
            PlayerInventory.Instance.SwapInventoryItems(_slotNumber, toSlotDragged);

            // Debug.Log("Меняю местами слоты в инвентаре" + _slotNumber + " и " + toSlotDragged);

            // Удаляем текстовую подсказку
            // DestroyTextBoxDescription();

        }
        if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.GetComponent<UIContainerSlot>() != null)
        {
            // Debug.Log("Перетаскиваю в контейнер!");
            // получаем слот, куда перетащили
            UIContainerSlot containerSlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<UIContainerSlot>();

            // если слот не пустой
            if (containerSlot.itemDetails != null)
            {
                // если переносимый предмет равен предмету, куда переносим
                if (containerSlot.itemDetails.itemCode == itemDetails.itemCode)
                {
                    ItemInInventory itemInInventory;
                    itemInInventory.itemCode = itemDetails.itemCode;
                    itemInInventory.itemCount = itemQuantity;
                    PlayerInventory.Instance.DeleteItemInPlayerInventory(_slotNumber, itemQuantity); // удаляем предметы из инвентаря 
                    ContainerMenuManager.Instance.AddItemInContainerInventory(itemInInventory); // добавляем предмет в контейнер
                }
            }
            // если слот пустой
            else
            {
                ItemInInventory itemInInventory;
                itemInInventory.itemCode = itemDetails.itemCode;
                itemInInventory.itemCount = itemQuantity;

                ContainerMenuManager.Instance.AddItemInContainerInventory(itemInInventory); // добавляем предмет в контейнер

                // Debug.Log("А теперь удаляем предмет из инвентаря ");
                PlayerInventory.Instance.DeleteItemInPlayerInventory(_slotNumber, itemQuantity); // удаляем предметы из инвентаря                

                itemDetails = null;
                itemQuantity = 0;
                EventHandler.CallInventoryUpdateEvent();
            }
        }
        else
        {
            // Если попали на иконку удаление предмета
            if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.GetComponent<UIDeleteIconInInventory>() != null)
            {
                // PlayerInventory.Instance.DeleteItemInPlayerInventory(_slotNumber, 1);
            }
        }

    }

}
