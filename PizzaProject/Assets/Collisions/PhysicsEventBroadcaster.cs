using UnityEngine;

public abstract class PhysicsEventBroadcaster : MonoBehaviour
{
    protected object Model => this;
    private PhysicsRouter _router;

    protected virtual void Awake()
    {
        PhysicsCompositeRoot.RegisterPhysicsModel(this);
    }

    protected virtual void OnDisable()
    {
        PhysicsCompositeRoot.DisregisterPhysicsModel(this);
    }

    public void Init(PhysicsRouter router)
    {
        _router = router;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_router == null)
            throw new System.InvalidOperationException();

        if (collider.TryGetComponent(out PhysicsEventBroadcaster broadcaster))
            _router.TryAddCollision(Model, broadcaster.Model);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_router == null)
            throw new System.InvalidOperationException();

        if (collision.gameObject.TryGetComponent(out PhysicsEventBroadcaster broadcaster))
            _router.TryAddCollision(Model, broadcaster.Model);
    }
}