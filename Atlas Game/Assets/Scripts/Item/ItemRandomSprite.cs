using UnityEngine;

public class ItemRandomSprite : MonoBehaviour
{
    private SpriteRenderer itemSpriteRenderer;
    private ItemDetails itemDetails;
    private int itemCode;


    private void Start()
    {
        // �������� ��������� �������
        itemSpriteRenderer = GetComponentInChildren<SpriteRenderer>();

        // �������� ItemCode
        itemCode = gameObject.GetComponent<Item>().ItemCode;

        // �������� itemDetails
        itemDetails = ItemManager.Instance.GetItemDetails(itemCode);

        // �������� ������ ��������
        Sprite[] spriteArray = itemDetails.itemSpriteArray;

        int radnomIndex = 0; // ����� �������

        // ���� �������� ������ ����, �������� ���������
        if (spriteArray.Length>1)
        {
            radnomIndex = Random.Range(0, spriteArray.Length);
        } 

        // ���������
        itemSpriteRenderer.sprite = spriteArray[radnomIndex];

    }

}
