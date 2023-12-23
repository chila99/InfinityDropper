using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUP: MonoBehaviour
{
    [SerializeField] protected GameObject _powerUPEffect;
    [SerializeField] protected AudioSource _audioSource;

    protected void Start()
    {
        if(_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<Collider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            GetComponentInChildren<SpriteRenderer>().enabled = false;
            Instantiate(_powerUPEffect, transform.position, _powerUPEffect.transform.rotation, transform);
            _audioSource.Play();
            StartCoroutine(ActivatePowerUP(other.GetComponent<Player>(), _powerUPEffect.GetComponent<ParticleSystem>().main.duration));
        }
    }

    protected abstract IEnumerator ActivatePowerUP(Player player, float effectDuration);
}
