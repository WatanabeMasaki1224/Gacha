using UnityEngine;

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

    Rigidbody2D _rb;
    float _moveInput;
    bool _isGrounded;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _moveInput = Input.GetAxisRaw("Horizontal");
        CheckGround();
        Jump();
        Attack();
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = new Vector2(_moveInput * _moveSpeed, _rb.linearVelocity.y);
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (_currentMagic == null) return;
            var obj = Instantiate(_magicPrefab, _firePoint.position, transform.rotation);
            obj.GetComponent<MagicController>().Init(_currentMagic);
        }
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
}
