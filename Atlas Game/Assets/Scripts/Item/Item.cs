using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private int _itemCode;
    public int ItemCode { get => _itemCode; set => _itemCode = value;  }


}
