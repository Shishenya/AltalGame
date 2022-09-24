using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIPauseMenuInventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI textCount = null; // Количество
    public Image spriteInventorySlot = null; // Картинка

    [SerializeField] private int _slotNumber = 0; // 

    // private Vector3 startPosition;
    private Canvas _parentCanvas; // Канвас родителя
    private GameObject _draggedItemGO; // перетаскиваемый предмет

    [SerializeField] PauseMenuInventoryManager pauseMenuInventoryManager = null; //  
    [SerializeField] private GameObject _inventoryTextBoxDescriptionPrefab = null; // Префаб текстовой подсказки

    [HideInInspector] public ItemDetails itemDetails;
    [HideInInspector] public int itemQuantity;


    /// <summary>
    ///  Specail Slot 
    ///  Опеределяем является ли слот специальным, под оружие или броню
    /// </summary>

    public bool specialSlot = false; // Специальный ли слот
    public SpecialInventorySlot specialInventorySlot = SpecialInventorySlot.none; // Тип слота


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

            _draggedItemGO = Instantiate(pauseMenuInventoryManager.inventoryManagementDraggedItemPrefab, pauseMenuInventoryManager.transform);

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

        if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.GetComponent<UIPauseMenuInventorySlot>() != null)
        {
            // если попали в слот для инвентаря

            // Определяем в какой слот попали
            int toSlotDragged = eventData.pointerCurrentRaycast.gameObject.GetComponent<UIPauseMenuInventorySlot>()._slotNumber;
            UIPauseMenuInventorySlot uiNewSlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<UIPauseMenuInventorySlot>();

            // Если слот является специальным
            if (eventData.pointerCurrentRaycast.gameObject.GetComponent<UIPauseMenuInventorySlot>().specialSlot)
            {
                Debug.Log("Попали в спец слот");
                ItemType itemType = itemDetails.itemType; // достаем текущий тип

                if (itemType.ToString() == uiNewSlot.specialInventorySlot.ToString()) {
                    Debug.Log("Типы совпадают");

                    switch(itemType.ToString())
                    {
                        case "weapon":
                            Debug.Log("Это было оружие");
                            PlayerInventory.Instance.weaponCode = itemDetails.itemCode; // достаем код
                            PlayerInventory.Instance.InstantiateWeaponSlot(); // инициализирем оружие в слоте
                            break;

                        default:
                            Debug.Log("неизсвтный слот!");
                            break;
                    }

                }

            } else
            {
                // Меняем предметы местами
                PlayerInventory.Instance.SwapInventoryItems(_slotNumber, toSlotDragged);

            }

            // Удаляем текстовую подсказку
            // DestroyTextBoxDescription();

        }
        else
        {
            // Если попали на иконку удаление предмета
            if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.GetComponent<UIDeleteIconInInventory>() != null)
            {
                PlayerInventory.Instance.DeleteItemInPlayerInventory(_slotNumber, 1);
            }
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        if (itemQuantity != 0)
        {


            pauseMenuInventoryManager.inventoryTextBox = Instantiate(_inventoryTextBoxDescriptionPrefab, transform.position, Quaternion.identity);
            pauseMenuInventoryManager.inventoryTextBox.transform.SetParent(_parentCanvas.transform, false);

            UIInventoryTextBox inventoryTextBox = pauseMenuInventoryManager.inventoryTextBox.GetComponent<UIInventoryTextBox>();

            inventoryTextBox.SetItemDescription(itemDetails.itemDescription, itemDetails.itemType.ToString(), itemDetails.itemLongDescription);

            pauseMenuInventoryManager.inventoryTextBox.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0f);
            pauseMenuInventoryManager.inventoryTextBox.transform.position = new Vector3(transform.position.x, transform.position.y + 50f, transform.position.z);


        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DestroyTextBoxDescription();
    }

    public void DestroyTextBoxDescription()
    {
        if (pauseMenuInventoryManager.inventoryTextBox != null)
        {
            Destroy(pauseMenuInventoryManager.inventoryTextBox);
        }
    }

}
