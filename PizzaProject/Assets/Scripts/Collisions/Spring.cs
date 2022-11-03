using UnityEngine;

public class Spring : PhysicsEventBroadcaster
{
    [SerializeField]
    private Jumper.Data _springProperties;

    [SerializeField]
    private Animator _animator;

    private readonly string _performKey = "Perform";

    public void TryAccelerate(Player player)
    {
        if (player.Jumper.IsFalling)
        {
            player.Jumper.PlayJumpAnimation(_springProperties);
            _animator.SetTrigger(_performKey);
        }
    }
}
