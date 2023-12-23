using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUP: MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActivatePowerUP(other.GetComponent<Player>());
            Destroy(gameObject);
        }
    }

    protected abstract void ActivatePowerUP(Player player);
}
