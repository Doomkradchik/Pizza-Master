using System;
using UnityEngine;

public sealed class FlyingMonster : Enemy
{
    [SerializeField]
    private float _flyingSpeed;

    private float x_offset;
    private readonly float _ratioY = 3f;
    private readonly float _ratioX = 3f;

    private SpriteRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        var y_delta = _ratioY * Mathf.Cos(Time.realtimeSinceStartup * _ratioX);

        transform.position = Vector2.MoveTowards(transform.position,
            _player.transform.position, _flyingSpeed * Time.fixedDeltaTime);

        transform.position += Vector3.up * y_delta * Time.deltaTime;

        var distance = transform.position.x - _player.transform.position.x;

        _right = distance > 0f;
        _renderer.flipX = _right;
    }
}