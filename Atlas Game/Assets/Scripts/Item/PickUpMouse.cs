using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpMouse : MonoBehaviour
{


    [SerializeField] private GameObject _pauseMenuCanvas = null; // Меню паузы
    [SerializeField] private GameObject _containerCanvas = null; // Меню конейтеров

    private void Update()
    {

        // !!!!!!!!!!!!!!!!!!!!
        // Добавить пооверку на расстояние
        // !!!!!!!!!!!!!!!!!!!!

        if (Input.GetMouseButtonDown(Tags.LeftMouseButton))
        {
            GameObject go = FindObjectPickUp();
            if (go == null) return;

            if (go.GetComponent<Item>() != null)
            {
                // Debug.Log("This is ITEM!");
                Item item = go.GetComponent<Item>(); // получаем предмет
                int itemCode = item.ItemCode; // получаем код предмета
                ItemDetails itemDetails = ItemManager.Instance.GetItemDetails(itemCode); // получаем детали предмета

                //
                // Переделать на Switch
                //

                // Проверяем. предмет можно поднять или нет?
                if (itemDetails.canBePickUp && Vector3.Distance(Player.Instance.transform.position, item.gameObject.transform.position) < Settings.distancePickUpItem)
                {


                    // создаем предмет
                    ItemInInventory newItem = new ItemInInventory(); // создаем предмет
                    newItem.itemCode = itemCode;
                    newItem.itemCount = 1;

                    // Добавляем его
                    PlayerInventory.Instance.AddItemInPlayerInventory(newItem);

                    // удаляем предмет
                    Destroy(go);

                }

                // Это контейнер
                if (itemDetails.itemType.ToString() == "container")
                {
                    Debug.Log("Тыкнул в контйнер");
                    // Если есть компонтент предметов в контейнере
                    if (go.GetComponent<ItemContainer>() != null)
                    {
                        // Достаем компонент
                        ItemContainer itemContainer = go.GetComponent<ItemContainer>();

                        // Добавляем предметы в меню контейнер-рюкзак
                        ContainerMenuManager.Instance.containerItems = itemContainer.itemInContainer;
                        ContainerMenuManager.Instance.currentItemContainer = itemContainer;

                        // Открываем меню контенер-рюкзак
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
            return hit.collider.gameObject; // Возвращаем объект
        }
        else
        {
            // Debug.Log("Тык в пустоту!");
            return null;
        }


    }
}
