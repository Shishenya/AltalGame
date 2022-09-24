using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UIInventoryBagSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public TextMeshProUGUI textCount = null; // ����������
    public Image spriteInventorySlot = null; // ��������

    private Canvas _parentCanvas; // ������ ��������
    private GameObject _draggedItemGO; // ��������������� �������

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


            // ���������� ����������
            Player.Instance.DisablePlayerInput();

            _draggedItemGO = Instantiate(uiContainerMenu.draggedItemPrefab, uiContainerMenu.transform);

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

        if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.GetComponent<UIInventoryBagSlot>() != null)
        {
            // ���� ������ � ���� ��� ���������

            // ���������� � ����� ���� ������
            int toSlotDragged = eventData.pointerCurrentRaycast.gameObject.GetComponent<UIInventoryBagSlot>()._slotNumber;

            // ������ �������� �������
            PlayerInventory.Instance.SwapInventoryItems(_slotNumber, toSlotDragged);

            // Debug.Log("����� ������� ����� � ���������" + _slotNumber + " � " + toSlotDragged);

            // ������� ��������� ���������
            // DestroyTextBoxDescription();

        }
        if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.GetComponent<UIContainerSlot>() != null)
        {
            // Debug.Log("������������ � ���������!");
            // �������� ����, ���� ����������
            UIContainerSlot containerSlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<UIContainerSlot>();

            // ���� ���� �� ������
            if (containerSlot.itemDetails != null)
            {
                // ���� ����������� ������� ����� ��������, ���� ���������
                if (containerSlot.itemDetails.itemCode == itemDetails.itemCode)
                {
                    ItemInInventory itemInInventory;
                    itemInInventory.itemCode = itemDetails.itemCode;
                    itemInInventory.itemCount = itemQuantity;
                    PlayerInventory.Instance.DeleteItemInPlayerInventory(_slotNumber, itemQuantity); // ������� �������� �� ��������� 
                    ContainerMenuManager.Instance.AddItemInContainerInventory(itemInInventory); // ��������� ������� � ���������
                }
            }
            // ���� ���� ������
            else
            {
                ItemInInventory itemInInventory;
                itemInInventory.itemCode = itemDetails.itemCode;
                itemInInventory.itemCount = itemQuantity;

                ContainerMenuManager.Instance.AddItemInContainerInventory(itemInInventory); // ��������� ������� � ���������

                // Debug.Log("� ������ ������� ������� �� ��������� ");
                PlayerInventory.Instance.DeleteItemInPlayerInventory(_slotNumber, itemQuantity); // ������� �������� �� ���������                

                itemDetails = null;
                itemQuantity = 0;
                EventHandler.CallInventoryUpdateEvent();
            }
        }
        else
        {
            // ���� ������ �� ������ �������� ��������
            if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.GetComponent<UIDeleteIconInInventory>() != null)
            {
                // PlayerInventory.Instance.DeleteItemInPlayerInventory(_slotNumber, 1);
            }
        }

    }

}
