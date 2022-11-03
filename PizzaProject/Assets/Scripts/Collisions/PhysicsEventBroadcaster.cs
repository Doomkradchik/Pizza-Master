using UnityEngine;

public abstract class PhysicsEventBroadcaster : MonoBehaviour
{
    protected object Model => this;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out PhysicsEventBroadcaster broadcaster))
            PhysicsCompositeRoot._physicsRouter.TryAddCollision(Model, broadcaster.Model);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.TryGetComponent(out PhysicsEventBroadcaster broadcaster))
            PhysicsCompositeRoot._physicsRouter.TryAddCollision(Model, broadcaster.Model);
    }
}