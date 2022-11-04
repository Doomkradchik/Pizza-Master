using UnityEngine;

public class HouseStateView : MonoBehaviour
{
    [SerializeField]
    private Sprite _opened;

    [SerializeField]
    private Sprite _closed;

    [SerializeField]
    private SpriteRenderer _renderer;

    public void UpdateState(bool isOpened)
    {
        if(isOpened)
            _renderer.sprite = _opened;
        else
            _renderer.sprite = _closed;
    }
}
