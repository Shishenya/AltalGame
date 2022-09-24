using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="so_ItemDetails", menuName ="Scriptable Object/Item/ItemDetals")]
public class SO_ItemDetails : ScriptableObject
{
    public List<ItemDetails> itemDetails;
}
