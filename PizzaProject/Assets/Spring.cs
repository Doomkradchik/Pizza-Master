using UnityEngine;

public class Spring : MonoBehaviour
{
    [SerializeField]
    private Jumper.Data _springProperties;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player) == false)
            return;

        if(player.Jumper.IsFalling)
            player.Jumper.PlayJumpAnimation(_springProperties);
    }


}
