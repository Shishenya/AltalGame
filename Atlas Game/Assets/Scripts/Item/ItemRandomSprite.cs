using UnityEngine;

public class ItemRandomSprite : MonoBehaviour
{
    private SpriteRenderer itemSpriteRenderer;
    private ItemDetails itemDetails;
    private int itemCode;


    private void Start()
    {
        // получаем компонент спрайта
        itemSpriteRenderer = GetComponentInChildren<SpriteRenderer>();

        // получаем ItemCode
        itemCode = gameObject.GetComponent<Item>().ItemCode;

        // ѕолучаем itemDetails
        itemDetails = ItemManager.Instance.GetItemDetails(itemCode);

        // получаем список спрайтов
        Sprite[] spriteArray = itemDetails.itemSpriteArray;

        int radnomIndex = 0; // номер спрайта

        // если спрайтов больше нул€, выбираем случайный
        if (spriteArray.Length>1)
        {
            radnomIndex = Random.Range(0, spriteArray.Length);
        } 

        // —прайтуем
        itemSpriteRenderer.sprite = spriteArray[radnomIndex];

    }

}
