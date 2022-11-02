using UnityEngine;

public class Spring : PhysicsEventBroadcaster
{
    [SerializeField]
    private Jumper.Data _springProperties;

    public void TryAccelerate(Player player)
    {
        if (player.Jumper.IsFalling)
            player.Jumper.PlayJumpAnimation(_springProperties);
    }
}
