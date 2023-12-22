using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [SerializeField] private float _difficultyChangeRate = 10000;
    [Header("Level Generator Parameters")]
    [SerializeField] private LevelGenerator _levelGenerator;
    [Header("X Hole Parameters")]
    [SerializeField] private float _startingMinHoleX = 5;
    [SerializeField] private float _startingMaxHoleX = 8;
    [SerializeField] private float _finalMinHoleX = 1;
    [SerializeField] private float _finaMaxlHoleX = 3;
    [Header("Z Hole Parameters")]
    [SerializeField] private float _startingMinHoleZ = 5;
    [SerializeField] private float _startingMaxHoleZ = 8;
    [SerializeField] private float _finalMinHoleZ = 1;
    [SerializeField] private float _finaMaxlHoleZ = 3;
    [Header("Player Parameters")]
    [SerializeField] private Player _player;
    [SerializeField] private float _finalPlayerSpeed = 10;
    [SerializeField] private float _startingPlayerSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        if (_levelGenerator == null)
        {
            _levelGenerator = FindObjectOfType<LevelGenerator>();
        }
        _levelGenerator.MinHoleX = _startingMinHoleX;
        _levelGenerator.MaxHoleX = _startingMaxHoleX;
        _levelGenerator.MinHoleZ = _startingMinHoleZ;
        _levelGenerator.MaxHoleZ = _startingMaxHoleZ;

        if (_player == null)
        {
            _player = FindObjectOfType<Player>();
        }
        _player.FallSpeed = _startingPlayerSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // based on the player score, change the difficulty with a tan^-1 function
        float interpolate = Mathf.Atan(_player.Score / _difficultyChangeRate) / Mathf.PI * 2;

        // interpolate between the starting and final values
        _levelGenerator.MinHoleX = (int) Mathf.Lerp(_startingMinHoleX, _finalMinHoleX, interpolate);
        _levelGenerator.MaxHoleX = (int) Mathf.Lerp(_startingMaxHoleX, _finaMaxlHoleX, interpolate);
        _levelGenerator.MinHoleZ = (int) Mathf.Lerp(_startingMinHoleZ, _finalMinHoleZ, interpolate);
        _levelGenerator.MaxHoleZ = (int) Mathf.Lerp(_startingMaxHoleZ, _finaMaxlHoleZ, interpolate);
        _player.FallSpeed = Mathf.Lerp(_startingPlayerSpeed, _finalPlayerSpeed, interpolate);
    }
}
