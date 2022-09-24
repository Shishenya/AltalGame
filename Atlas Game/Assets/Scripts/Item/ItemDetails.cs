using UnityEngine;

[System.Serializable]
public class ItemDetails
{
    public int itemCode; // ���������� ���
    public string itemDescription; // ��������
    public string itemLongDescription; // ������� ��������
    public Sprite[] itemSpriteArray; // ������� ��������

    public ItemType itemType; // ��� ��������
    public ArmorType armorType; // ����������� ��� - �����
    public WeaponType weaponType; // ����������� ��� - ������
    public FoodType foodType; // ����������� ��� ��� ���

    public bool canBePickUp;

    public bool canBeStacked; // ����� �� ������� ��������
    public int maxQuantityInStack; // ������������ ���������� � �����

    // ������
    public int minDamageWeapon; // ����������� ����
    public int maxDamageWeapon; // ������������ ����
    public float reloadWeapon; // ����������� ������

    // �������
    public int defenseLevelArmor; // ������� ������
    public float maxPointInArmor; // ������������ ���������� ������� � �������

    // ���
    public int hourAfterFoodBecomesRotten; // ����� � �����, ����� �������� ��� ���������� ���������
    public int healthPointStandartFood; // ���������� HP, ������� �������������� ��� � ����������� ����
    public bool canUseRaw; // ����� �� ����������� �����
    public float multiplicateRawFood; // �������������� ��� ����� ���
    public bool canUseRotten; // ����� �� ����������� ��������� ���
    public float multiplicateRottenFood; // ������������� ��� ��������� ���

}
