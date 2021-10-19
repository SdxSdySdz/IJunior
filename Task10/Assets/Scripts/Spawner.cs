using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Player _player;

    private Wave _currentWave;
    private int _currentWaveNumber = 0;
    private float _timeAfterLastSpawn;
    private int _spawnedCount;

    public event UnityAction AllEnemySpawned;

    private void Start()
    {
        SetWave(0);
    }

    private void Update()
    {
        if (_currentWave == null) return;
        _timeAfterLastSpawn += Time.deltaTime;

        if (_timeAfterLastSpawn >= _currentWave.Delay)
        {
            InstantiateEnemy();
            _spawnedCount++;
            _timeAfterLastSpawn = 0;
        }

        if (_currentWave.Count <= _spawnedCount)
        {
            if (_waves.Count > _currentWaveNumber + 1)
            {
                AllEnemySpawned?.Invoke(); 
            }

            _currentWave = null;
        }
    }

    private void InstantiateEnemy()
    {;
        Enemy enemy = Instantiate(_currentWave.Template, _spawnPoint.position, _spawnPoint.rotation, _spawnPoint).GetComponent<Enemy>();
        enemy.Init(_player);
        enemy.Dying += OnEnemyDying;
    }

    private void SetWave(int waveIndex)
    {
        _currentWave = _waves[waveIndex];
    }

    public void NextWave()
    {
        SetWave(++_currentWaveNumber);
        _spawnedCount = 0;
    }

    private void OnEnemyDying(Enemy enemy)
    {
        enemy.Dying -= OnEnemyDying;

        _player.AddMoney(enemy.Reward);
    }
}

[System.Serializable]
public class Wave
{
    public GameObject Template;
    public float Delay;
    public int Count;
}
