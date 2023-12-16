using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUIManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    // text to display the score
    [SerializeField] private TextMeshProUGUI _scoreText;

    // Start is called before the first frame update
    void Start()
    {
        if (_player == null)
        {
            _player = FindObjectOfType<Player>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        _scoreText.text = _player.Score.ToString("0");
    }
}
