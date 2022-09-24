using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICursor : Singleton<UICursor>
{

    private GameObject cursorGO = null; // курсор

    public GameObject cursorDefault = null; // префаб курсора руки
    public GameObject cursorHandPrefab = null; // префаб курсора руки
    public GameObject cursorLoupePrefab = null; // префаб курсора руки
    public GameObject cursorChopPrefab = null; // префаб курсор рубки деревьев


    private void Start()
    {
        ResetCursor();
        // UnityEngine.Cursor.visible = false;
    }

    public void HandPickUp()
    {
        // ResetCursor();
        cursorDefault.SetActive(false);
        cursorHandPrefab.SetActive(true);
        cursorLoupePrefab.SetActive(false);
        cursorChopPrefab.SetActive(false);
    }
    public void ContainerCursor()
    {
        // ResetCursor();
        cursorDefault.SetActive(false);
        cursorHandPrefab.SetActive(false);
        cursorLoupePrefab.SetActive(true);
        cursorChopPrefab.SetActive(false);
    }

    public void ResetCursor()
    {
        cursorDefault.SetActive(false);
        cursorHandPrefab.SetActive(false);
        cursorLoupePrefab.SetActive(false);
        cursorChopPrefab.SetActive(false);
    }

    public void DefaultCursor()
    {
        // ResetCursor();
        cursorDefault.SetActive(true);
        cursorHandPrefab.SetActive(false);
        cursorLoupePrefab.SetActive(false);
        cursorChopPrefab.SetActive(false);
    }

    public void ChopCursor()
    {
        cursorDefault.SetActive(false);
        cursorHandPrefab.SetActive(false);
        cursorLoupePrefab.SetActive(false);
        cursorChopPrefab.SetActive(true);
    }


    private void Update()
    {

        // Задаем позицию для курсора
        float posX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        float posY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        float posZ = -Camera.main.ScreenToWorldPoint(Input.mousePosition).z;
        transform.position = new Vector3(posX, posY, posZ);

        // Ищем куда попадает курсор
        RaycastHit2D hit = new RaycastHit2D();
        hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);        

        // если куда то попали
        if (hit.collider != null)
        {

            cursorGO = hit.collider.gameObject;
            // Если это предмет, то
            if (cursorGO.GetComponent<Item>()!=null)
            {
                // получем компонент item
                Item item = cursorGO.GetComponent<Item>();
                int itemCode = item.ItemCode;
                ItemDetails itemDetails = ItemManager.Instance.GetItemDetails(itemCode);

                // если предмет можно поднять, то ставим курсор руки
                if (itemDetails.canBePickUp && Vector3.Distance(Player.Instance.transform.position, item.gameObject.transform.position)<Settings.distancePickUpItem)
                {
                    HandPickUp();
                }

                // если это контейнер
                if (itemDetails.itemType.ToString() == "container")
                {
                    ContainerCursor();
                }

                // если это дерево
                if (itemDetails.itemType.ToString() == "tree")
                {
                    ChopCursor();
                }



            } else
            {
                DefaultCursor();
            }
            
        }
        else
        {
            cursorGO = null;
            DefaultCursor();
            // Debug.Log("Рука ушла с предмета");
        }

    }

}
