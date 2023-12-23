using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float FallSpeed { get => _fallSpeed; set => _fallSpeed = value; }
    public float MinFallSpeed { get => _minFallSpeed; private set => _minFallSpeed = value; }
    public float MaxFallSpeed { get => _maxFallSpeed; private set => _maxFallSpeed = value; }
    [SerializeField] private float _speed = 3.5f;
    [SerializeField] private float _dashSpeed = 1000f;
    [SerializeField] private float _dashRechargeTime = 2f;
    [SerializeField] private float _minFallSpeed = 10f;
    [SerializeField] private float _maxFallSpeed = 20f;
    [SerializeField] private TrailRenderer _trailRenderer;
    [SerializeField] private LevelGenerator _levelGenerator;
    private PlayerInput _playerInput;
    private Rigidbody _rb;
    private float _fallSpeed = 1.5f;
    private float _score = 0;
    private float _startingY = 0;
    private float _lastTimeDash = 0;
    private bool _isDashing = false;

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
        _trailRenderer = GetComponent<TrailRenderer>();
        _levelGenerator = FindObjectOfType<LevelGenerator>();
        _playerInput = new PlayerInput();
        _playerInput.Player.Enable();
        _startingY = transform.position.y;
        _fallSpeed = _minFallSpeed;
        _rb.velocity = new Vector3(0, -_fallSpeed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        _score = -transform.position.y + _startingY;

        if(_playerInput.Player.Dash.triggered && CanDash && (_rb.velocity.x != 0 || _rb.velocity.z != 0))
        {
            StartCoroutine(Dash());
        }
        if(_isDashing)
        {
            _lastTimeDash = Time.time;
        }
        Color currentPlatformColor = _levelGenerator.CurrentPlatformColor;
        transform.GetComponent<MeshRenderer>().material.color = new Color(1 - currentPlatformColor.r, 1 - currentPlatformColor.g, 1 - currentPlatformColor.b);
        _trailRenderer.material = transform.GetComponent<MeshRenderer>().material;
    }

    void FixedUpdate()
    {
        if (!_isDashing)
        {
            float x = _playerInput.Player.Movement.ReadValue<Vector3>().x;
            float z = _playerInput.Player.Movement.ReadValue<Vector3>().y;
            Vector3 playerVelocity = new Vector3(x, 0, z) * _speed;
            playerVelocity.y = _rb.velocity.y;
            _rb.velocity = playerVelocity;
        }
        _rb.velocity = new Vector3(_rb.velocity.x, -_fallSpeed, _rb.velocity.z);
    }

    private IEnumerator Dash()
    {
        _isDashing = true;
        _lastTimeDash = Time.time;
        Vector3 dashDirection = _rb.velocity;
        float rbYVelocity = _rb.velocity.y;
        dashDirection.y = 0;
        dashDirection.Normalize();
        Vector3 newVelocity = _rb.velocity;
        newVelocity.y = 0;
        newVelocity += dashDirection * _dashSpeed;
        _rb.velocity = newVelocity; 
        _trailRenderer.emitting = true;

        yield return new WaitForSeconds(_trailRenderer.time);
        _isDashing = false;
        _trailRenderer.emitting = false;
        _rb.velocity = new Vector3(0, rbYVelocity, 0);
    }
}
