
public abstract class Enemy : PhysicsEventBroadcaster, IPauseHandler
{
    protected Player _player;
    protected bool _right = false;
    protected float RightDirection => _right ? 1f : -1f;

    public void Pause() => enabled = false;

    public void Unpause() => enabled = true;

    protected virtual void Awake()
    {
        _player = FindObjectOfType<Player>();
        PauseManager.Subscribe(this);
    }

    private void OnDestroy()
    => PauseManager.Unsubscribe(this);
}
