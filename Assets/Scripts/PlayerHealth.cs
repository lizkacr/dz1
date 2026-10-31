using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 3;
    [SerializeField] private float _invulnerabilityTime = 1f;

    private float _lastDamageTime = -999f;

    public event Action<int, int> Changed;
    public event Action Died;

    public int Current { get; private set; }

    public int Max => _maxHealth;

    private void Awake()
    {
        Current = _maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (Time.time - _lastDamageTime < _invulnerabilityTime)
        {
            return;
        }

        _lastDamageTime = Time.time;
        Current = Mathf.Max(0, Current - amount);
        Changed?.Invoke(Current, _maxHealth);

        if (Current == 0)
        {
            Died?.Invoke();
        }
    }

    public void Heal(int amount)
    {
        Current = Mathf.Min(_maxHealth, Current + amount);
        Changed?.Invoke(Current, _maxHealth);
    }
}