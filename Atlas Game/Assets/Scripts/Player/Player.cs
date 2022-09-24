using System.Collections;
using UnityEngine;

public class Player : Singleton<Player>
{

    // Параметры анимации и движения

    private bool _isIdle = true; // стоит на месте
    private bool _isWalk; // Идет
    private bool _isRun; // Бежит
    private bool _isSlink; // крадется
    private bool _isPickUp; // подбирает
    private bool _isChopping; // рубить
    private bool _isFishing; // Ловит рыбу
    private bool _isUseTool; // использует иснтрумент (копает, рубит, ловит рыбу и т.д.) Возможно заменит выше стоящие
    private bool _isHit; // удар ближнего боя
    private bool _isBite; // укус
    private bool _isEat = false; // кушать
    private bool _isCure; // лечить
    private bool _IsSwimming; // плавает

    private float _xInput; // движение по X
    private float _yInput; // движение по Y

    // Комопоненты
    private Rigidbody2D _rb = null; // RigidBody
    private Grid _grid = null;

    // Параметры игрока
    private float _moveSpeed = 3f;

    // Заблокировано ли движение?
    private bool _playerInputIsDisable = false;
    public bool PlayerInputIsDisable { get=>_playerInputIsDisable; set=> _playerInputIsDisable = value; }


    // Анимация персонажа
    private Animator[] _animatorPlayerArray = null; 

    protected override void Awake()
    {
        base.Awake();

        _rb = GetComponent<Rigidbody2D>();

    }

    private void Start()
    {        
        _grid = FindObjectOfType<Grid>(); // Получаем ткущий ГРид карты
        _animatorPlayerArray = GetComponentsInChildren<Animator>(); // Анимации частей тела
    }


    private void Update()
    {
        PlayerMovementInput(); // отслеживаем нажатие клавиш пользователем

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
        // Отслеживаем движение персонажа
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
        // Получаем вектор движения
        Vector2 moveDirection = new Vector2(_xInput * _moveSpeed * Time.deltaTime,
                                            _yInput * _moveSpeed * Time.deltaTime);

        // Двигаемся
        _rb.MovePosition(_rb.position + moveDirection);
        // Debug.Log(transform.position);
        // GetCellPositionPlayer();

    }

    /// <summary>
    /// Определям позицию на Tilemap игрока с учетом отступов
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
    /// Запускам анимацию персонажа в зависимости от параметров
    /// </summary>
    private void SetAnimationPlayer(float x, float y, bool isIdle, bool isEat)
    {
        // Проходим по аниматорам
        foreach (Animator currentAnimator in _animatorPlayerArray)
        {
            currentAnimator.SetFloat("x", x);
            currentAnimator.SetFloat("y", y);
            currentAnimator.SetBool("idle", _isIdle);
            currentAnimator.SetBool("eat", _isEat);
        }
    }

    /// <summary>
    /// Кушаем еду
    /// </summary>
    public void EatingFood(int itemCode)
    {
        StartCoroutine(EatingFoodCorutine(itemCode));
    }

    /// <summary>
    /// Корутина на еду
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
