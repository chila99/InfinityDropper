using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Slider _dashSlider;
    [SerializeField] private Color _canDashColor;
    [SerializeField] private Color _cantDashColor;

    // Start is called before the first frame update
    void Start()
    {
        if (_player == null)
        {
            _player = FindObjectOfType<Player>();
        }
        _dashSlider.maxValue = _player.DashRechargeTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(_player.CanDash)
        {
            _dashSlider.value = _player.DashRechargeTime;
            _dashSlider.fillRect.GetComponent<Image>().color = _canDashColor;   
        }
        else
        {
            _dashSlider.value = _player.DashRechargeTimeLeft;
            _dashSlider.fillRect.GetComponent<Image>().color = _cantDashColor;
        }
        
    }
}
