using UnityEngine;
using System;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private Camera _camera;

    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Move(float right)
    {
        if (right == 0f)
            return;

        var offset = right * _speed;
        _rigidbody2D.velocity = new Vector2(offset, _rigidbody2D.velocity.y);

        var horizontal = _camera.WorldToViewportPoint(_rigidbody2D.position).x;
        TryTranslateToScreen(horizontal);
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
