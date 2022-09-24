using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UIInventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    private Camera _mainCamera; // камера
    private Grid _grid = null;
    private Canvas _parentCanvas; // Родительский канвас
    private Transform _parentItemTransform; // родитель всех предметов
    private GameObject _draggedItemGO = null; // переносимый предмет
    private Image _draggedItemImage = null;

    public Image inventorySlotImage; // картинка с предметом
    public TextMeshProUGUI textCountItems; // колчество предмета

    [SerializeField] private UIInventoryDownBar _inventoryDownBar = null; // нижний инвентарь
    [SerializeField] private GameObject _itemPrefab = null; 
    [SerializeField] private int _slotNumber = 0; // Слот инвентаря
    [SerializeField] private GameObject _inventoryTextBoxDescriptionPrefab = null;

    [HideInInspector] public ItemDetails itemDetails; // детали предмета в слоте
    [HideInInspector] public int itemQuantity; // количство


    private void Awake()
    {
        _parentCanvas = GetComponentInParent<Canvas>();
        _grid = FindObjectOfType<Grid>(); // Получаем ткущий ГРид карты
    }

    private void Start()
    {
        _mainCamera = Camera.main;
        _parentItemTransform = GameObject.FindGameObjectWithTag(Tags.ItemsParrent).transform;
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        if (itemDetails!=null)
        {
            
            // Debug.Log("начинаю перетаскивать предмет");

            // Блоикируем управление
            Player.Instance.DisablePlayerInput();

            // создаем переносимый предмет
            _draggedItemGO = Instantiate(_inventoryDownBar.draggedItemPrefab, _inventoryDownBar.transform);

            // Добавляем предмету картинку
            Image draggedItemImage = _draggedItemGO.GetComponentInChildren<Image>();
            _draggedItemImage = draggedItemImage;
            draggedItemImage.sprite = inventorySlotImage.sprite;
            draggedItemImage.color = Settings.redColor;

        }
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (_draggedItemGO!=null)
        {
            // Debug.Log("перетаскиваю");
            _draggedItemGO.transform.position = Input.mousePosition;

            // Опоределяем расстояние бросаемого предмета
            Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -_mainCamera.transform.position.z));
            float distanceThrow = Vector3.Distance(mousePosition, Player.Instance.transform.position);

            
            if (distanceThrow >= Settings.distanceThrowItem)
            {

                _draggedItemImage.color = Settings.redColor;
            } 
            else
            {
                _draggedItemImage.color = Settings.greenColor;
            }

        }
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if (_draggedItemGO!=null)
        {
            // Debug.Log("Закончил перетаскивать!");
            Destroy(_draggedItemGO); // уничтожаем переносмимый предмет
            _draggedItemImage = null;



            if (eventData.pointerCurrentRaycast.gameObject!=null && eventData.pointerCurrentRaycast.gameObject.GetComponent<UIInventorySlot>()!=null)
            {
                // если попали в слот для инвентаря

                // Определяем в какой слот попали
                int toSlotDragged = eventData.pointerCurrentRaycast.gameObject.GetComponent<UIInventorySlot>()._slotNumber;

                // Меняем предметы местами
                PlayerInventory.Instance.SwapInventoryItems(_slotNumber, toSlotDragged);

                // Удаляем текстовую подсказку
                DestroyTextBoxDescription();

            } else
            {
                // если предмет можно бросать

                Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -_mainCamera.transform.position.z));
                float distanceThrow = Vector3.Distance(mousePosition, Player.Instance.transform.position);

                if (itemDetails!=null && distanceThrow<=Settings.distanceThrowItem)
                {
                    // Бросаме его
                    DropSelectedItem();
                }
            }

            //включаем управление
            Player.Instance.EnablePlayerInput(); 

        }
    }

    private void DropSelectedItem()
    {
        if (itemDetails!=null)
        {
            // Определяем куда бросаем
            Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -_mainCamera.transform.position.z));

            // СОздаем префаб на сцене
            GameObject itemDroppedGO = Instantiate(_itemPrefab, worldPosition, Quaternion.identity, _parentItemTransform);
            Item item = itemDroppedGO.GetComponent<Item>();
            item.ItemCode = itemDetails.itemCode;

            // Добавляем к нему картинку
            SpriteRenderer itemPrefabImage = itemDroppedGO.GetComponentInChildren<SpriteRenderer>();
            itemPrefabImage.sprite = inventorySlotImage.sprite;

            // Удаляем предмет из инветаря (переписать, нужно удалять именно с того слота, в котором мы выбирали)
            // Ищем позицию предмета в инвентаре
            int positionItemInInventory = PlayerInventory.Instance.FindItemInPlayerInventory(item.ItemCode);
            // Количество
            int countItemsDelete = 1;
            // Удаляем
            PlayerInventory.Instance.DeleteItemInPlayerInventory(positionItemInInventory, countItemsDelete);


        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        if (itemQuantity!=0)
        {


            _inventoryDownBar.inventoryTextBox = Instantiate(_inventoryTextBoxDescriptionPrefab, transform.position, Quaternion.identity);
            _inventoryDownBar.inventoryTextBox.transform.SetParent(_parentCanvas.transform, false);

            UIInventoryTextBox inventoryTextBox = _inventoryDownBar.inventoryTextBox.GetComponent<UIInventoryTextBox>();

            inventoryTextBox.SetItemDescription(itemDetails.itemDescription, itemDetails.itemType.ToString(), itemDetails.itemLongDescription);

            _inventoryDownBar.inventoryTextBox.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0f);
            _inventoryDownBar.inventoryTextBox.transform.position = new Vector3(transform.position.x, transform.position.y + 50f, transform.position.z);


        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DestroyTextBoxDescription();
    }


    /// <summary>
    /// УДаление текстовой подсказки
    /// </summary>
    public void DestroyTextBoxDescription()
    {
        if (_inventoryDownBar.inventoryTextBox!=null)
        {
            Destroy(_inventoryDownBar.inventoryTextBox);
        }
    }

    /// <summary>
    /// нажатие по слоту
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        // если нажата ПКМ
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("Right click");
            eventData.Reset();
            Player.Instance.EatingFood(0);
        }
    }
}
