using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private float _minGroundNormalY = .65f;
    [SerializeField] private float _gravityModifier = 1f;
    [SerializeField] private LayerMask _layerMask;
    
    private Rigidbody2D _rigidbody;
    private RaycastHit2D[] _hitBuffer;
    private ContactFilter2D _contactFilter;
    private List<RaycastHit2D> _hitBufferList;
    private Vector2 _velocity;
    private Vector2 _targetVelocity;
    private Vector2 _groundNormal;
    private bool _isGrounded;

    private const float _minMoveDistance = 0.001f;
    private const float _shellRadius = 0.01f;
    
    public Vector2 Velocity => _velocity;
    public bool IsGrounded => _isGrounded;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _hitBuffer = new RaycastHit2D[16];
        _hitBufferList = new List<RaycastHit2D>();
    }

    private void Start()
    {
        _contactFilter.useTriggers = false;
        _contactFilter.SetLayerMask(_layerMask);
        _contactFilter.useLayerMask = true;
    }

    private void Update()
    {
        _targetVelocity = new Vector2(Input.GetAxis("Horizontal"), 0) * _speed;

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _velocity.y = _jumpForce;
        }
    }

    private void FixedUpdate()
    {
        _velocity += _gravityModifier * Physics2D.gravity * Time.deltaTime;
        _velocity.x = _targetVelocity.x;

        _isGrounded = false;

        Vector2 deltaPosition = _velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(_groundNormal.y, -_groundNormal.x);
        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);

        move = Vector2.up * deltaPosition.y;

        Movement(move, true);
    }

    private void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > _minMoveDistance)
        {
            int count = _rigidbody.Cast(move, _contactFilter, _hitBuffer, distance + _shellRadius);

            _hitBufferList.Clear();

            for (int i = 0; i < count; i++)
            {
                _hitBufferList.Add(_hitBuffer[i]);
            }

            foreach (var hit in _hitBufferList)
            {
                Vector2 currentNormal = hit.normal;
                if (currentNormal.y > _minGroundNormalY)
                {
                    _isGrounded = true;
                    if (yMovement)
                    {
                        _groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(_velocity, currentNormal);
                if (projection < 0)
                {
                    _velocity = _velocity - projection * currentNormal;
                }

                float modifiedDistance = hit.distance - _shellRadius;
                distance = Mathf.Min(distance, modifiedDistance);
            }
        }

        _rigidbody.position = _rigidbody.position + move.normalized * distance;
    }
}

