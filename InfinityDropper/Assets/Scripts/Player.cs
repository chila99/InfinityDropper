using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 3.5f;
    private PlayerInput _playerInput;
    private Rigidbody _rb;
    private float _score = 0;
    private float _startingY = 0;

    public float Score
    {
        get => _score;
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _playerInput = new PlayerInput();
        _playerInput.Player.Enable();
        _startingY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        _score = -transform.position.y + _startingY;
    }

    void FixedUpdate()
    {
        float x = _playerInput.Player.Movement.ReadValue<Vector2>().x;
        float z = _playerInput.Player.Movement.ReadValue<Vector2>().y;
        Vector3 playerVelocity = new Vector3(x, 0, z) * _speed;
        playerVelocity.y = _rb.velocity.y;
        _rb.velocity = playerVelocity;
    }
}
