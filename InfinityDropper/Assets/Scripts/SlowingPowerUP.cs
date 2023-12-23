using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowingPowerUP : PowerUP
{
    [SerializeField] private float _velocityReduction = 2.5f;

    protected override IEnumerator ActivatePowerUP(Player player, float effectDuration)
    {
        player.FallSpeed -= _velocityReduction;
        if (player.FallSpeed < player.MinFallSpeed)
        {
            player.FallSpeed = player.MinFallSpeed;
        }
        yield return new WaitForSeconds(effectDuration);
        Destroy(gameObject);
    }
}
