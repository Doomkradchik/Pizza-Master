using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    void Update()
    {
        var right = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");

        _player.Movement.Move(right);

        if (Input.GetKeyDown(KeyCode.Space))
            if(_player.Jumper.Grounded)
                _player.Jumper.PlayJumpAnimation(_player.Jumper.BaseData);

        _player.TryClimb(vertical);

        if (Input.GetKeyDown(KeyCode.E))
            _player.Pizza.CleanUp();
    }
}
