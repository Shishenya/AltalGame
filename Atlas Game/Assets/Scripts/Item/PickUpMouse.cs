using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpMouse : MonoBehaviour
{


    [SerializeField] private GameObject _pauseMenuCanvas = null; // ���� �����
    [SerializeField] private GameObject _containerCanvas = null; // ���� ����������

    private void Update()
    {

        // !!!!!!!!!!!!!!!!!!!!
        // �������� �������� �� ����������
        // !!!!!!!!!!!!!!!!!!!!

        if (Input.GetMouseButtonDown(Tags.LeftMouseButton))
        {
            GameObject go = FindObjectPickUp();
            if (go == null) return;

            if (go.GetComponent<Item>() != null)
            {
                // Debug.Log("This is ITEM!");
                Item item = go.GetComponent<Item>(); // �������� �������
                int itemCode = item.ItemCode; // �������� ��� ��������
                ItemDetails itemDetails = ItemManager.Instance.GetItemDetails(itemCode); // �������� ������ ��������

                //
                // ���������� �� Switch
                //

                // ���������. ������� ����� ������� ��� ���?
                if (itemDetails.canBePickUp && Vector3.Distance(Player.Instance.transform.position, item.gameObject.transform.position) < Settings.distancePickUpItem)
                {


                    // ������� �������
                    ItemInInventory newItem = new ItemInInventory(); // ������� �������
                    newItem.itemCode = itemCode;
                    newItem.itemCount = 1;

                    // ��������� ���
                    PlayerInventory.Instance.AddItemInPlayerInventory(newItem);

                    // ������� �������
                    Destroy(go);

                }

                // ��� ���������
                if (itemDetails.itemType.ToString() == "container")
                {
                    Debug.Log("������ � ��������");
                    // ���� ���� ���������� ��������� � ����������
                    if (go.GetComponent<ItemContainer>() != null)
                    {
                        // ������� ���������
                        ItemContainer itemContainer = go.GetComponent<ItemContainer>();

                        // ��������� �������� � ���� ���������-������
                        ContainerMenuManager.Instance.containerItems = itemContainer.itemInContainer;
                        ContainerMenuManager.Instance.currentItemContainer = itemContainer;

                        // ��������� ���� ��������-������
                        _pauseMenuCanvas.SetActive(true);
                        _containerCanvas.SetActive(true);

                    }
                }



            }
        }
    }

    private GameObject FindObjectPickUp()
    {
        RaycastHit2D hit = new RaycastHit2D();
        hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            // Debug.Log(hit.collider.name);
            return hit.collider.gameObject; // ���������� ������
        }
        else
        {
            // Debug.Log("��� � �������!");
            return null;
        }


    }
}
