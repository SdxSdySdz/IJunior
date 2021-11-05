using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coin;
    [SerializeField] private Platform[] _platforms;

    private float _offsetY = 1f;

    private void Start()
    {
        foreach (var platform in _platforms)
        {
            Vector3 position = platform.transform.position;
            position.y += _offsetY;
;           position.x += Random.Range(-platform.transform.lossyScale.x, platform.transform.lossyScale.x) / 2f;

            Instantiate(_coin, position, Quaternion.identity);
        }
    }
}
