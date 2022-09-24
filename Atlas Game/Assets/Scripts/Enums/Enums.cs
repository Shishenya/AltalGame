/// <summary>
/// Enums ��� ����
/// </summary>


// ��� ��������
public enum ItemType
{
    weapon, // ������
    armor, // ������� �������
    container, // ���������
    fire, // ������� ����
    food, // ���
    other, // ������ 
    tree, // ������
    none, // ��� ����
    count // �����
} 

// ����������� ��� �������� - ������
public enum ArmorType
{
    noarmor, // �� �������� ��������� �������
    body, // ����
    helmet, // ������
    gloves, // ��������
    boots, // ������
    none, // ���
    count // �����
}

// ����������� ��� �������� - ������
public enum WeaponType
{
    noweapon, // �� �������� �������
    sword, // ���
    axe, // �����
    dagger, // ������
    bow, // ���
    none, // ��� ����
    count // �����
}

// ����������� ��� - ���
public enum FoodType
{
    nofood, // �� �������� ����
    raw, // ����� ���
    freshFood, // ������ ���
    rottenFood, // ��������� ���
    water, // ����
    dirtyWater, // ������� ����
    none, // ��� ����
    count // �����
}

public enum LoadedScene
{
    Scene1_coast
}

// ������������ ����������� ������ � ���������
public enum SpecialInventorySlot
{
    armor,
    weapon, 
    shield,
    none
}