using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UIInventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    private Camera _mainCamera; // ������
    private Grid _grid = null;
    private Canvas _parentCanvas; // ������������ ������
    private Transform _parentItemTransform; // �������� ���� ���������
    private GameObject _draggedItemGO = null; // ����������� �������
    private Image _draggedItemImage = null;

    public Image inventorySlotImage; // �������� � ���������
    public TextMeshProUGUI textCountItems; // ��������� ��������

    [SerializeField] private UIInventoryDownBar _inventoryDownBar = null; // ������ ���������
    [SerializeField] private GameObject _itemPrefab = null; 
    [SerializeField] private int _slotNumber = 0; // ���� ���������
    [SerializeField] private GameObject _inventoryTextBoxDescriptionPrefab = null;

    [HideInInspector] public ItemDetails itemDetails; // ������ �������� � �����
    [HideInInspector] public int itemQuantity; // ���������


    private void Awake()
    {
        _parentCanvas = GetComponentInParent<Canvas>();
        _grid = FindObjectOfType<Grid>(); // �������� ������ ���� �����
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
            
            // Debug.Log("������� ������������� �������");

            // ���������� ����������
            Player.Instance.DisablePlayerInput();

            // ������� ����������� �������
            _draggedItemGO = Instantiate(_inventoryDownBar.draggedItemPrefab, _inventoryDownBar.transform);

            // ��������� �������� ��������
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
            // Debug.Log("������������");
            _draggedItemGO.transform.position = Input.mousePosition;

            // ����������� ���������� ���������� ��������
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
            // Debug.Log("�������� �������������!");
            Destroy(_draggedItemGO); // ���������� ������������ �������
            _draggedItemImage = null;



            if (eventData.pointerCurrentRaycast.gameObject!=null && eventData.pointerCurrentRaycast.gameObject.GetComponent<UIInventorySlot>()!=null)
            {
                // ���� ������ � ���� ��� ���������

                // ���������� � ����� ���� ������
                int toSlotDragged = eventData.pointerCurrentRaycast.gameObject.GetComponent<UIInventorySlot>()._slotNumber;

                // ������ �������� �������
                PlayerInventory.Instance.SwapInventoryItems(_slotNumber, toSlotDragged);

                // ������� ��������� ���������
                DestroyTextBoxDescription();

            } else
            {
                // ���� ������� ����� �������

                Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -_mainCamera.transform.position.z));
                float distanceThrow = Vector3.Distance(mousePosition, Player.Instance.transform.position);

                if (itemDetails!=null && distanceThrow<=Settings.distanceThrowItem)
                {
                    // ������� ���
                    DropSelectedItem();
                }
            }

            //�������� ����������
            Player.Instance.EnablePlayerInput(); 

        }
    }

    private void DropSelectedItem()
    {
        if (itemDetails!=null)
        {
            // ���������� ���� �������
            Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -_mainCamera.transform.position.z));

            // ������� ������ �� �����
            GameObject itemDroppedGO = Instantiate(_itemPrefab, worldPosition, Quaternion.identity, _parentItemTransform);
            Item item = itemDroppedGO.GetComponent<Item>();
            item.ItemCode = itemDetails.itemCode;

            // ��������� � ���� ��������
            SpriteRenderer itemPrefabImage = itemDroppedGO.GetComponentInChildren<SpriteRenderer>();
            itemPrefabImage.sprite = inventorySlotImage.sprite;

            // ������� ������� �� �������� (����������, ����� ������� ������ � ���� �����, � ������� �� ��������)
            // ���� ������� �������� � ���������
            int positionItemInInventory = PlayerInventory.Instance.FindItemInPlayerInventory(item.ItemCode);
            // ����������
            int countItemsDelete = 1;
            // �������
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
    /// �������� ��������� ���������
    /// </summary>
    public void DestroyTextBoxDescription()
    {
        if (_inventoryDownBar.inventoryTextBox!=null)
        {
            Destroy(_inventoryDownBar.inventoryTextBox);
        }
    }

    /// <summary>
    /// ������� �� �����
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        // ���� ������ ���
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("Right click");
            eventData.Reset();
            Player.Instance.EatingFood(0);
        }
    }
}
