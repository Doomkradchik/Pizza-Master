using UnityEngine;

public class Player : PhysicsEventBroadcaster
{
    [SerializeField]
    private Jumper _jumper;

    [SerializeField]
    private Movement _movement;

    public Jumper Jumper => _jumper;
    public Movement Movement => _movement;

    public Pizza Pizza { get; private set; }

    private void OnEnable()
    {
        Pizza = new Pizza();
        Pizza.ToppingAdded += () => Debug.Log("Added");//
        Pizza.Cleared += () => Debug.Log("Cleared");//
    }

    public void TryClimb(float vertical)
    {
        if (Movement.InterructedWithLadder == false)
        {
            Movement.ResetGravity();
            return;
        }

        Movement.AddStaticHorizontalVelocity(vertical);
    }
}
