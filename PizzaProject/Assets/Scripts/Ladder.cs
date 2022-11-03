using UnityEngine;

public class Ladder : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.Movement.InterructedWithLadder = true;
            player.Jumper.Interrupt();
        }
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
            player.Movement.InterructedWithLadder = false;
    }
}
