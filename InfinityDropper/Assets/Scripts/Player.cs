using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 3.5f;
    private PlayerInput _playerInput;
    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _playerInput = new PlayerInput();
        _playerInput.Player.Enable();
    }

    // Update is called once per frame
    void Update()
    {
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
