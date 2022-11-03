using UnityEngine;

public class TrashCan : MonoBehaviour
{
    [SerializeField]
    private GameObject _trashInterructionView;

    private void OnEnable()
    {
        _trashInterructionView.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player) == false)
            return;

        player.Pizza.TrashCollided = true;

        if (player.Pizza.CanClean())
            _trashInterructionView.SetActive(true);

        player.Pizza.Cleared += DisableView;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player) == false)
            return;

        player.Pizza.TrashCollided = false;
        player.Pizza.Cleared -= DisableView;
        DisableView();
    }

    private void DisableView() => _trashInterructionView.SetActive(false);
}
