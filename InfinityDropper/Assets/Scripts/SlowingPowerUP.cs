using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowingPowerUP : PowerUP
{
    [SerializeField] private float _velocityReduction = 2.5f;

    protected override void ActivatePowerUP(Player player)
    {
        player.FallSpeed -= _velocityReduction;
        if (player.FallSpeed < player.MinFallSpeed)
        {
            player.FallSpeed = player.MinFallSpeed;
        }
    }
}
