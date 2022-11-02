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

    private void Start()
    {
        Pizza = new Pizza();
        Pizza.ToppingAdded += (topping) => Debug.Log(topping);//
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
