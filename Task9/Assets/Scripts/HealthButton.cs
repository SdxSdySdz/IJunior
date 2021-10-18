using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class HealthButton : MonoBehaviour
{
    [SerializeField] private int _healthAmount;
    [SerializeField] private Player _player;


    public void ChangePlayerHealth()
    {
        if (_healthAmount > 0)
        {
            _player.TakeHealing(_healthAmount);
        }
        else
        {
            _player.TakeDamage(-_healthAmount);
        }
    }
}
