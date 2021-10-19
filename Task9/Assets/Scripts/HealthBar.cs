using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Player _player;

    private Slider _slider;
    private Coroutine _changingValueCoroutine;
    private readonly float _changingValueSpeed = 5f;

    private void OnEnable()
    {
        _player.HealthChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnValueChanged;
    }

    private void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.minValue = _player.MinHealth;
        _slider.maxValue = _player.MaxHealth;
        _slider.value = _player.Health;
    }

    public void OnValueChanged()
    {
        if (_changingValueCoroutine != null)
        {
            StopCoroutine(_changingValueCoroutine);
        }

        _changingValueCoroutine = StartCoroutine(ChangeSliderValue());
    }

    private IEnumerator ChangeSliderValue()
    {
        var waitForEndOfFrame = new WaitForEndOfFrame();
        float accuracy = 0.01f;
        float difference = Mathf.Abs(_slider.value - _player.Health);
        
        while (difference > accuracy)
        {
            difference = Mathf.Abs(_slider.value - _player.Health);
            _slider.value = Mathf.MoveTowards(_slider.value, _player.Health, Time.deltaTime * difference * _changingValueSpeed);
            yield return waitForEndOfFrame;
        }
        _slider.value = _player.Health;
    }
}
