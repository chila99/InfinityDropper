using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 3.5f;
    [SerializeField] private float _dashSpeed = 1000f;
    [SerializeField] private float _dashRechargeTime = 2f;
    [SerializeField] private float _fallAcceleration = 0.5f;
    [SerializeField] private float _startingFallSpeed = 1.5f;
    [SerializeField] private float _maxFallSpeed = 3.5f;
    private PlayerInput _playerInput;
    private Rigidbody _rb;
    private float _score = 0;
    private float _startingY = 0;
    private float _lastTimeDash = 0;

    public float Score
    {
        get => _score;
    }

    public bool CanDash
    {
        get => Time.time - _lastTimeDash > _dashRechargeTime;
    }

    public float DashRechargeTime
    {
        get => _dashRechargeTime;
    }

    public float DashRechargeTimeLeft
    {
        get => _dashRechargeTime - (Time.time - _lastTimeDash);
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _playerInput = new PlayerInput();
        _playerInput.Player.Enable();
        _startingY = transform.position.y;
        _rb.velocity = new Vector3(0, -_startingFallSpeed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        _score = -transform.position.y + _startingY;

        if(_playerInput.Player.Dash.triggered && (Time.time - _lastTimeDash > _dashRechargeTime) && (_rb.velocity.x != 0 || _rb.velocity.z != 0))
        {
            _lastTimeDash = Time.time;
            Vector3 dashDirection = _rb.velocity.normalized;
            dashDirection.y = 0;
            _rb.AddForce(dashDirection * _dashSpeed, ForceMode.Acceleration);
        }
    }

    void FixedUpdate()
    {
        float x = _playerInput.Player.Movement.ReadValue<Vector2>().x;
        float z = _playerInput.Player.Movement.ReadValue<Vector2>().y;
        Vector3 playerVelocity = new Vector3(x, 0, z) * _speed;
        playerVelocity.y = _rb.velocity.y;
        _rb.velocity = playerVelocity;
        if (_rb.velocity.y < -_maxFallSpeed)
        {
            // remove all forces
            _rb.velocity = new Vector3(_rb.velocity.x, -_maxFallSpeed, _rb.velocity.z);
            return;
        }
        _rb.AddForce(Vector3.down * _fallAcceleration, ForceMode.Acceleration);
    }
}
