using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UIContainerSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler /*IPointerEnterHandler, IPointerExitHandler*/
{
    public TextMeshProUGUI textCount = null; // Количество
    public Image spriteContainerSlot = null; // Картинка

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
            draggedItemImage.sprite = spriteContainerSlot.sprite;

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

        // Если попали на слот контейнера
        if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.GetComponent<UIContainerSlot>() != null)
        {
            // если попали в слот для инвентаря

            // Определяем в какой слот попали
            int toSlotDragged = eventData.pointerCurrentRaycast.gameObject.GetComponent<UIContainerSlot>()._slotNumber;

            // Меняем предметы местами
            ContainerMenuManager.Instance.SwapContainersItems(_slotNumber, toSlotDragged);

            // Debug.Log("Меняю местами слоты в контейнере" + _slotNumber + " и " + toSlotDragged);

            // Удаляем текстовую подсказку
            // DestroyTextBoxDescription();

        }
        // если попали на слот инвентаря
        else if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.GetComponent<UIInventoryBagSlot>() != null)
        {
            // Debug.Log("Перетаскиваю в инвентарь!");
            // получаем слот, куда перетащили
            UIInventoryBagSlot bagSlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<UIInventoryBagSlot>();

            // если слот не пустой
            if (bagSlot.itemDetails!=null)
            {
                // если переносимый предмет равен предмету, куда переносим
                if (bagSlot.itemDetails.itemCode == itemDetails.itemCode)
                {
                    ItemInInventory itemInInventory;
                    itemInInventory.itemCode = itemDetails.itemCode;
                    itemInInventory.itemCount = itemQuantity;
                    ContainerMenuManager.Instance.DeleteItemInContainer(_slotNumber, itemQuantity); // удаляем предметы из контейнера 
                    PlayerInventory.Instance.AddItemInPlayerInventory(itemInInventory); // добавляем предмет в инвентарь
                }
            }
            // если слот пустой
            else
            {
                ItemInInventory itemInInventory;
                itemInInventory.itemCode = itemDetails.itemCode;
                itemInInventory.itemCount = itemQuantity;

                PlayerInventory.Instance.AddItemInPlayerInventory(itemInInventory); // добавляем предмет в инвентарь

                Debug.Log("А теперь удаляем предмет из контейнера ");
                ContainerMenuManager.Instance.DeleteItemInContainer(_slotNumber, itemQuantity); // удаляем предметы из контейнера                

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
