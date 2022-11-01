using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Jumper _jumper;

    [SerializeField]
    private Movement _movement;

    public Jumper Jumper => _jumper;
    public Movement Movement => _movement;

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
