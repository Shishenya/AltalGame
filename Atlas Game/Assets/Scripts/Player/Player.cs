using System.Collections;
using UnityEngine;

public class Player : Singleton<Player>
{

    // ��������� �������� � ��������

    private bool _isIdle = true; // ����� �� �����
    private bool _isWalk; // ����
    private bool _isRun; // �����
    private bool _isSlink; // ��������
    private bool _isPickUp; // ���������
    private bool _isChopping; // ������
    private bool _isFishing; // ����� ����
    private bool _isUseTool; // ���������� ���������� (������, �����, ����� ���� � �.�.) �������� ������� ���� �������
    private bool _isHit; // ���� �������� ���
    private bool _isBite; // ����
    private bool _isEat = false; // ������
    private bool _isCure; // ������
    private bool _IsSwimming; // �������

    private float _xInput; // �������� �� X
    private float _yInput; // �������� �� Y

    // �����������
    private Rigidbody2D _rb = null; // RigidBody
    private Grid _grid = null;

    // ��������� ������
    private float _moveSpeed = 3f;

    // ������������� �� ��������?
    private bool _playerInputIsDisable = false;
    public bool PlayerInputIsDisable { get=>_playerInputIsDisable; set=> _playerInputIsDisable = value; }


    // �������� ���������
    private Animator[] _animatorPlayerArray = null; 

    protected override void Awake()
    {
        base.Awake();

        _rb = GetComponent<Rigidbody2D>();

    }

    private void Start()
    {        
        _grid = FindObjectOfType<Grid>(); // �������� ������ ���� �����
        _animatorPlayerArray = GetComponentsInChildren<Animator>(); // �������� ������ ����
    }


    private void Update()
    {
        PlayerMovementInput(); // ����������� ������� ������ �������������

        TestInput();

    }

    private void FixedUpdate()
    {
        if (!PlayerInputIsDisable)
        {
            PlayerMove();
        }

        SetAnimationPlayer(_xInput, _yInput, _isIdle, _isEat);

    }

    private void TestInput()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("test Inventory!");
            PlayerInventory.Instance.TestPrint();
        }
    }

    private void PlayerMovementInput()
    {
        // ����������� �������� ���������
        _xInput = Input.GetAxisRaw("Horizontal");
        _yInput = Input.GetAxisRaw("Vertical");

        if (_xInput==0 && _yInput==0)
        {
            _isIdle = true;
        } else
        {
            _isIdle = false;
        }

    }

    private void PlayerMove()
    {
        // �������� ������ ��������
        Vector2 moveDirection = new Vector2(_xInput * _moveSpeed * Time.deltaTime,
                                            _yInput * _moveSpeed * Time.deltaTime);

        // ���������
        _rb.MovePosition(_rb.position + moveDirection);
        // Debug.Log(transform.position);
        // GetCellPositionPlayer();

    }

    /// <summary>
    /// ��������� ������� �� Tilemap ������ � ������ ��������
    /// </summary>
    private void GetCellPositionPlayer()
    {
        Vector3 posPlayerByIndent = new Vector3(transform.position.x + Settings.xIndent,
                                                    transform.position.y + Settings.yIndent,
                                                    transform.position.z);
        if (_grid != null)
        {
            Vector3Int cellPosition = _grid.WorldToCell(posPlayerByIndent);
            // Debug.Log(cellPosition);
        }
    }

    public void EnablePlayerInput()
    {
        PlayerInputIsDisable = false;
    }

    public void DisablePlayerInput()
    {
        PlayerInputIsDisable = true;
    }

    /// <summary>
    /// �������� �������� ��������� � ����������� �� ����������
    /// </summary>
    private void SetAnimationPlayer(float x, float y, bool isIdle, bool isEat)
    {
        // �������� �� ����������
        foreach (Animator currentAnimator in _animatorPlayerArray)
        {
            currentAnimator.SetFloat("x", x);
            currentAnimator.SetFloat("y", y);
            currentAnimator.SetBool("idle", _isIdle);
            currentAnimator.SetBool("eat", _isEat);
        }
    }

    /// <summary>
    /// ������ ���
    /// </summary>
    public void EatingFood(int itemCode)
    {
        StartCoroutine(EatingFoodCorutine(itemCode));
    }

    /// <summary>
    /// �������� �� ���
    /// </summary>
    /// <param name="itemCode"></param>
    /// <returns></returns>
    private IEnumerator EatingFoodCorutine(int itemCode) {
        _isEat = true;
        _isIdle = true;
        DisablePlayerInput();
        SetAnimationPlayer(_xInput, _yInput,  _isIdle,  _isEat);
        yield return new WaitForSeconds(Settings.eatingFoodTime);
        _isEat = false;
        _isIdle = true;
        SetAnimationPlayer(_xInput, _yInput, _isIdle, _isEat);
        EnablePlayerInput();
    }


}
