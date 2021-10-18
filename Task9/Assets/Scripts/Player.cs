using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private readonly int _maxHealth = 100;
    private readonly int _minHealth = 0;
    private int _health;

    [HideInInspector] public event UnityAction HealthChanged;
    public int MaxHealth => _maxHealth;
    public int MinHealth => _minHealth;
    public float Health => _health;


    private void Awake()
    {
        _health = _maxHealth;
    }


    public void TakeDamage(int damage)
    {
        ChangeHealth(-damage);
    }


    public void TakeHealing(int healingAmount)
    {
        ChangeHealth(healingAmount);
    }


    private void ChangeHealth(int value)
    {
        _health += value;
        _health = Mathf.Clamp(_health, _minHealth, _maxHealth);
        HealthChanged?.Invoke();
    }
}
