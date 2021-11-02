using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class PlayerAnimator : MonoBehaviour
{
    private PlayerMover _playerMover;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private void Awake()
    {
        _playerMover = GetComponent<PlayerMover>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Run();
        
        if (Input.GetKeyDown(KeyCode.Space) && _playerMover.IsGrounded)
            Jump();
    }

    private void Run()
    {
        var velocity = _playerMover.Velocity;
        if (velocity.x == 0)
        {
            _animator.SetBool("Run", false);
        }
        else
        {
            _animator.SetBool("Run", true);
            _spriteRenderer.flipX = velocity.x < 0;
        }
    }

    private void Jump()
    {
        _animator.SetTrigger("Jump");
    }
}
