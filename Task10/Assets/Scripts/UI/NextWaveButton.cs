using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextWaveButton : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Button _button;

    private void OnEnable()
    {
        _spawner.AllEnemySpawned += OnAllEnemySpawned;
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDisable()
    {
        _spawner.AllEnemySpawned -= OnAllEnemySpawned;
        _button.onClick.RemoveListener(OnButtonClicked);
    }


    private void Start()
    {
        _button.gameObject.SetActive(false);
    }


    public void OnAllEnemySpawned()
    {
        Debug.LogError("All enemy spawned");
        _button.gameObject.SetActive(true);
    }


    public void OnButtonClicked()
    {
        _spawner.NextWave();
        _button.gameObject.SetActive(false);
    }
}
