using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIInventoryTextBox : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI itemName = null;
    [SerializeField] private TextMeshProUGUI itemType = null;
    [SerializeField] private TextMeshProUGUI itemDescription = null;

    public void SetItemDescription(string name, string typeItemString, string description)
    {

        itemName.text = name;
        itemType.text = GetRussianStrinItemType(typeItemString);
        itemDescription.text = description;

    }

    private string GetRussianStrinItemType(string typeItemString)
    {
        switch (typeItemString)
        {
            case "weapon": return "Оружие";
            case "armor": return "Броня";
            case "food": return "Еда";
            case "other": return "Разное";

            default:
            return "Неизвестный тип";

        }
    }


}
