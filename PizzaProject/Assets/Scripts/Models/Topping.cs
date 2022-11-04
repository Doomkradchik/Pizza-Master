using UnityEngine;

public class Topping : PhysicsEventBroadcaster
{
    [SerializeField]
    private Kind _toppingsKind;

    public void OnPlayerCollided(Player player)
    {
        player.Pizza.AddTopping(new Pizza.ToppingData(_toppingsKind,
            GetComponent<SpriteRenderer>().sprite));

        Destroy(gameObject);
    }

    public enum Kind
    {
        Default,
        Anchovy,
        BellPepper,
        Cheese,
        Mushroom,
        Olive,
        Onion,
        Pepperoni,
        Pineaple
    }
}