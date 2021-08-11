using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public class Player : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private int _coinAmount;

    private void OnEnable()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Coin coin))
        {
            _coinAmount++;
            coin.Disappear();
        }
    }

    public void Run(Vector2 velocity)
    {
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

    public void Jump()
    {
        _animator.SetTrigger("Jump");
    }
}
