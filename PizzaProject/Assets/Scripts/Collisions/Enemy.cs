using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : PhysicsEventBroadcaster
{
    protected Player _player;
    protected bool _right = false;
    protected float RightDirection => _right ? 1f : -1f;
    protected virtual void Awake()
    {
        _player = FindObjectOfType<Player>();
    }
}
