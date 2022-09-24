using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{

    public GameObject pauseMenuCanvas = null; // �� �������� ���� �����
    public PauseMenuInventoryManager pauseMenuInventoryManager = null;

    public GameObject inventoryMenu = null; // �� ������������ ���� ������
    private bool _inventoryMenuVisible = false;

    public GameObject containerToBag = null; // �� ��������� + ������

    public GameObject inventoryItemDraggedPrefab = null; // ������ ������������ ��������

    
    protected override void Awake()
    {
        base.Awake();
    }

    public void EnableInventory()
    {
        // pauseMenuInventoryManager.UpdateInventory();
        pauseMenuCanvas.SetActive(true);
        inventoryMenu.SetActive(true);
        _inventoryMenuVisible = true;
    }

    public void DisableInventory()
    {
        pauseMenuCanvas.SetActive(false);
        inventoryMenu.SetActive(false);
        _inventoryMenuVisible = false;
    }

    public void DisableContainerToBagUI ()
    {
        pauseMenuCanvas.SetActive(false);
        containerToBag.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (_inventoryMenuVisible)
            {
                DisableInventory();
                Player.Instance.PlayerInputIsDisable = false;
            }
            else
            {
                EnableInventory();
                Player.Instance.PlayerInputIsDisable = true;
            }
        }
    }

}
