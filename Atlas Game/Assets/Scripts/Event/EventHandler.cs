using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class EventHandler 
{


    // ��������� ���������
    public static event Action InventoryUpdateEvent; //

    public static void CallInventoryUpdateEvent()
    {
        if (InventoryUpdateEvent != null)
            InventoryUpdateEvent();
    }

    // ----------------------------------


}

