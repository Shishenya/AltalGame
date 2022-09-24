using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UIContainerSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler /*IPointerEnterHandler, IPointerExitHandler*/
{
    public TextMeshProUGUI textCount = null; // ����������
    public Image spriteContainerSlot = null; // ��������

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


        Destroy(_draggedItemGO); // ���������� ������������ �������

        // ���� ������ �� ���� ����������
        if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.GetComponent<UIContainerSlot>() != null)
        {
            // ���� ������ � ���� ��� ���������

            // ���������� � ����� ���� ������
            int toSlotDragged = eventData.pointerCurrentRaycast.gameObject.GetComponent<UIContainerSlot>()._slotNumber;

            // ������ �������� �������
            ContainerMenuManager.Instance.SwapContainersItems(_slotNumber, toSlotDragged);

            // Debug.Log("����� ������� ����� � ����������" + _slotNumber + " � " + toSlotDragged);

            // ������� ��������� ���������
            // DestroyTextBoxDescription();

        }
        // ���� ������ �� ���� ���������
        else if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.GetComponent<UIInventoryBagSlot>() != null)
        {
            // Debug.Log("������������ � ���������!");
            // �������� ����, ���� ����������
            UIInventoryBagSlot bagSlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<UIInventoryBagSlot>();

            // ���� ���� �� ������
            if (bagSlot.itemDetails!=null)
            {
                // ���� ����������� ������� ����� ��������, ���� ���������
                if (bagSlot.itemDetails.itemCode == itemDetails.itemCode)
                {
                    ItemInInventory itemInInventory;
                    itemInInventory.itemCode = itemDetails.itemCode;
                    itemInInventory.itemCount = itemQuantity;
                    ContainerMenuManager.Instance.DeleteItemInContainer(_slotNumber, itemQuantity); // ������� �������� �� ���������� 
                    PlayerInventory.Instance.AddItemInPlayerInventory(itemInInventory); // ��������� ������� � ���������
                }
            }
            // ���� ���� ������
            else
            {
                ItemInInventory itemInInventory;
                itemInInventory.itemCode = itemDetails.itemCode;
                itemInInventory.itemCount = itemQuantity;

                PlayerInventory.Instance.AddItemInPlayerInventory(itemInInventory); // ��������� ������� � ���������

                Debug.Log("� ������ ������� ������� �� ���������� ");
                ContainerMenuManager.Instance.DeleteItemInContainer(_slotNumber, itemQuantity); // ������� �������� �� ����������                

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
