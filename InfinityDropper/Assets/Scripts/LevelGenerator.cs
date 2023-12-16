using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
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

    private Queue<GameObject> _platforms = new Queue<GameObject>();
    private Vector3 _lastPlatformPosition;
    

    // Start is called before the first frame update
    void Start()
    {
        if (_player == null)
        {
            _player = FindObjectOfType<Player>();
        }
        _lastPlatformPosition = transform.position;
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
            platform.transform.localPosition = newPlatformPosition;
            _platforms.Enqueue(platform);
            _lastPlatformPosition = newPlatformPosition;
        }
    }

    private void CheckForPlayerOvertaking()
    {
        // when player overpass one platform, generate new ones
        // if the height of the player is less than the height of the platform - the distance to generate
        // get the first platform in the queue
        GameObject firstPlatform = _platforms.Peek();
        if (_player.transform.position.y < firstPlatform.transform.position.y - _distanceToGenerate)
        {
            // destroy the first platform
            Destroy(firstPlatform);
            // remove the first platform from the queue
            _platforms.Dequeue();
        }
    }
}
