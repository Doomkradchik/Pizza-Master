using UnityEngine;

public class Topping : PhysicsEventBroadcaster
{
    [SerializeField]
    private Kind _toppingsKind;
    public Kind ToppingKind => _toppingsKind;

    public void OnPlayerCollided(Player player)
    {
        player.Pizza.AddTopping(this);
        Destroy(gameObject);
    }

    public enum Kind
    {
        Default,
    }
}