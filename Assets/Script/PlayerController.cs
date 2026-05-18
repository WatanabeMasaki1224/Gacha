using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 1.0f;
    [SerializeField] float _jumpPower = 1.0f;
    public Transform _groundCheck;
    [SerializeField] float _checkRedius = 0.2f;
    public LayerMask _groundLayer;
    [SerializeField] Transform _firePoint;
    [SerializeField] GameObject _magicPrefab;
    MagicDataSO _currentMagic;
    [SerializeField] MagicDataSO _defaultMagic;
    Rigidbody2D _rb;
    float _moveInput;
    bool _isGrounded;
    [SerializeField] private Animator _animator;
    int _direction = 1;
    bool _isAttacking = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        if (_defaultMagic != null)
        {
            _currentMagic = _defaultMagic;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _moveInput = 0;
        if (!_isAttacking)
        {
            if (Keyboard.current.aKey.isPressed)
            {
                _moveInput = -1;
                _direction = -1;
                Vector3 scale = transform.localScale;
                scale.x = -Mathf.Abs(scale.x);
                transform.localScale = scale;
            }

            if (Keyboard.current.dKey.isPressed)
            {
                _moveInput = 1;
                _direction = 1;
                Vector3 scale = transform.localScale;
                scale.x = Mathf.Abs(scale.x);
                transform.localScale = scale;
            }
            Jump();
            Attack();
        }

        CheckGround();
        _animator.SetBool("IsRun", Mathf.Abs(_moveInput) > 0.1f);
        _animator.SetBool("IsGround", _isGrounded);
    }

    private void FixedUpdate()
    {
        if (_isAttacking)
        {
            _rb.linearVelocity = new Vector2(0, _rb.linearVelocity.y);
            return;
        }

        _rb.linearVelocity = new Vector2(_moveInput * _moveSpeed, _rb.linearVelocity.y);
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (_currentMagic == null) return;
            _isAttacking = true;
            _animator.SetTrigger("Attack");
        }
    }

    public void FireMagic()
    {
        var obj = Instantiate(_magicPrefab, _firePoint.position, Quaternion.identity);

        obj.GetComponent<MagicController>().Init(_currentMagic, _direction);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _jumpPower);
        }
    }

    void CheckGround()
    {
        _isGrounded = Physics2D.OverlapCircle(
            _groundCheck.position,
            _checkRedius,
            _groundLayer);
    }

    public void SetMagic(MagicDataSO magic)
    {
        _currentMagic = magic;
    }

    public MagicDataSO GetCurrentMagicName()
    {
        return _currentMagic;
    }

    public void EndAttack()
    {
        _isAttacking = false;
    }
}
