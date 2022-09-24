using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIPauseMenuInventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI textCount = null; // ����������
    public Image spriteInventorySlot = null; // ��������

    [SerializeField] private int _slotNumber = 0; // 

    // private Vector3 startPosition;
    private Canvas _parentCanvas; // ������ ��������
    private GameObject _draggedItemGO; // ��������������� �������

    [SerializeField] PauseMenuInventoryManager pauseMenuInventoryManager = null; //  
    [SerializeField] private GameObject _inventoryTextBoxDescriptionPrefab = null; // ������ ��������� ���������

    [HideInInspector] public ItemDetails itemDetails;
    [HideInInspector] public int itemQuantity;


    /// <summary>
    ///  Specail Slot 
    ///  ����������� �������� �� ���� �����������, ��� ������ ��� �����
    /// </summary>

    public bool specialSlot = false; // ����������� �� ����
    public SpecialInventorySlot specialInventorySlot = SpecialInventorySlot.none; // ��� �����


    private void Start()
    {
        _parentCanvas = GetComponentInParent<Canvas>();
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        if (itemDetails != null)
        {

            
            // ���������� ����������
            Player.Instance.DisablePlayerInput();

            _draggedItemGO = Instantiate(pauseMenuInventoryManager.inventoryManagementDraggedItemPrefab, pauseMenuInventoryManager.transform);

            // ��������� �������� ��������
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


        Destroy(_draggedItemGO); // ���������� ������������ �������

        if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.GetComponent<UIPauseMenuInventorySlot>() != null)
        {
            // ���� ������ � ���� ��� ���������

            // ���������� � ����� ���� ������
            int toSlotDragged = eventData.pointerCurrentRaycast.gameObject.GetComponent<UIPauseMenuInventorySlot>()._slotNumber;
            UIPauseMenuInventorySlot uiNewSlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<UIPauseMenuInventorySlot>();

            // ���� ���� �������� �����������
            if (eventData.pointerCurrentRaycast.gameObject.GetComponent<UIPauseMenuInventorySlot>().specialSlot)
            {
                Debug.Log("������ � ���� ����");
                ItemType itemType = itemDetails.itemType; // ������� ������� ���

                if (itemType.ToString() == uiNewSlot.specialInventorySlot.ToString()) {
                    Debug.Log("���� ���������");

                    switch(itemType.ToString())
                    {
                        case "weapon":
                            Debug.Log("��� ���� ������");
                            PlayerInventory.Instance.weaponCode = itemDetails.itemCode; // ������� ���
                            PlayerInventory.Instance.InstantiateWeaponSlot(); // ������������� ������ � �����
                            break;

                        default:
                            Debug.Log("���������� ����!");
                            break;
                    }

                }

            } else
            {
                // ������ �������� �������
                PlayerInventory.Instance.SwapInventoryItems(_slotNumber, toSlotDragged);

            }

            // ������� ��������� ���������
            // DestroyTextBoxDescription();

        }
        else
        {
            // ���� ������ �� ������ �������� ��������
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
