using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public float MinHoleX { get { return _minHoleX; } set { _minHoleX = value; } }
    public float MaxHoleX { get { return _maxHoleX; } set { _maxHoleX = value; } }
    public float MinHoleZ { get { return _minHoleZ; } set { _minHoleZ = value; } }
    public float MaxHoleZ { get { return _maxHoleZ; } set { _maxHoleZ = value; } }
    public float PlatformSize { get { return _platformSize; } private set { _platformSize = value; } }
    public Color CurrentPlatformColor { get { return _platforms.Peek().GetComponentInChildren<Renderer>().material.color; } }

    // keep reference to the player so can be created new platforms and
    // deleted old ones
    [Header("Player Parameters")]
    [SerializeField] private Player _player;
    [SerializeField] private float _distanceToGenerate = 10;
    [Header("Level Generation Parameters")]
    [SerializeField] private int _numberOfPlatforms = 10;
    [SerializeField] private float _distanceBetweenPlatforms = 100;
    [Header("Platforms Parameters")]
    [SerializeField] private GameObject _platformPrefab;
    [SerializeField] private float _platformSize = 10;
    [SerializeField] private float _minHoleX = 1;
    [SerializeField] private float _maxHoleX = 5;
    [SerializeField] private float _minHoleZ = 1;
    [SerializeField] private float _maxHoleZ = 5;

    private Queue<GameObject> _platforms = new Queue<GameObject>();
    private Vector3 _lastPlatformPosition;
    private GameObject _roof;

    // Start is called before the first frame update
    void Start()
    {
        if (_player == null)
        {
            _player = FindObjectOfType<Player>();
        }
        _lastPlatformPosition = transform.position;
        _roof = GameObject.CreatePrimitive(PrimitiveType.Cube);
        _roof.transform.parent = transform;
        _roof.transform.localPosition = new Vector3(0, 0, 0);
        _roof.transform.localScale = new Vector3(_platformSize, 1, _platformSize);
        _player.transform.position = new Vector3(0, -_distanceToGenerate, 0);
    }

    // Update is called once per frame
    void Update()
    {
        CheckForCorrectPlatformNumber();
        CheckForPlayerOvertaking();
    }

    private void CheckForCorrectPlatformNumber()
    {
        if (_platforms.Count > _numberOfPlatforms)
        {
            return;
        }
        while (_platforms.Count <= _numberOfPlatforms)
        {
            Vector3 newPlatformPosition = _lastPlatformPosition + Vector3.down * _distanceBetweenPlatforms;
            GameObject platform = Instantiate(_platformPrefab, transform);
            platform.GetComponent<PlatformGenerator>().GeneratePlatform(_platformSize, _minHoleX, _maxHoleX, _minHoleZ, _maxHoleZ, _distanceBetweenPlatforms);
            platform.transform.localPosition = newPlatformPosition;
            _platforms.Enqueue(platform);
            _lastPlatformPosition = newPlatformPosition;
        }
    }

    private void CheckForPlayerOvertaking()
    {
        // when player overpass two platform, generate new ones
        // if the height of the player is less than the height of the platform - the distance to generate
        // get the first platform in the queue
        GameObject firstPlatform = _platforms.Peek();
        if (_player.transform.position.y < firstPlatform.transform.position.y - _distanceToGenerate)
        {
            // destroy the first platform
            _roof.transform.position = firstPlatform.transform.position;
            Destroy(firstPlatform);
            // remove the first platform from the queue
            _platforms.Dequeue();
        }
    }
}
