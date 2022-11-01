using UnityEngine;
using System;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private Vector2 _velocitySpeed;

    [SerializeField]
    private Camera _camera;

    private Rigidbody2D _rigidbody2D;
    public bool InterructedWithLadder { get; set; } = false;

    private float _gravityScaleBased;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _gravityScaleBased = _rigidbody2D.gravityScale;
    }

    public void Move(float right)
    {
        if (right == 0f)
            return;

        var offset = Vector2.right * right * _velocitySpeed.x;
        _rigidbody2D.position += offset * Time.fixedDeltaTime;

        var horizontal = _camera.WorldToViewportPoint(_rigidbody2D.position).x;
        TryTranslateToScreen(horizontal);
    }

    public void AddStaticHorizontalVelocity(float vertical)
    {
        ResetGravity();

        if (InterructedWithLadder == false)
            return;

        _rigidbody2D.gravityScale = 0f;
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x,
            vertical * _velocitySpeed.y);
    }

    public void ResetGravity()
    {
        if (_rigidbody2D.gravityScale != _gravityScaleBased)
            _rigidbody2D.gravityScale = _gravityScaleBased;
    }

    private void TryTranslateToScreen(float horizontal)
    {
        var x = 0f;
        if (horizontal < 0f)
            x = _camera.ViewportToWorldPoint(Vector2.right).x;
        else if (horizontal > 1f)
            x = _camera.ViewportToWorldPoint(Vector2.zero).x;
        else
            return;

        transform.position = new Vector2(x, transform.position.y);
    }

}
