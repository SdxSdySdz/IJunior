using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _pathDuration;
    [SerializeField] private Transform[] _waypointTransforms;

    private Vector3[] _waypoints;

    private void Start()
    {
        _waypoints = new Vector3[_waypointTransforms.Length];

        for (int i = 0; i < _waypointTransforms.Length; i++)
        {
            _waypoints[i] = _waypointTransforms[i].position;
        }

        transform.position = _waypoints[0];

        Tween tween = transform.DOPath(_waypoints, _pathDuration, PathType.Linear).SetOptions(true).SetEase(Ease.Linear);
        tween.SetLoops(-1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            player.Die();
        }
    }
}
