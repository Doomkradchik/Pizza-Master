using System;
using System.Collections.Generic;
using System.Linq;

public class PhysicsRouter
{
    public PhysicsRouter(Func<IEnumerable<Record>> recordsProvider)
    {
        _recordsProvider = recordsProvider;
    }

    private Collisions _collisions = new Collisions();
    private Func<IEnumerable<Record>> _recordsProvider;

    public void TryAddCollision(object left, object right)
    {
        _collisions.TryBind(left, right);
    }

    public void Launch()
    {
        foreach (var pair in _collisions.Pairs)
            Execute(pair);

        _collisions = new Collisions();
    }

    public void Execute((object, object) pair)
    {
        IEnumerable<Record> records = _recordsProvider?.Invoke()
            .Where(record => record.IsTarget(pair));

        foreach(var record in records)
             ((dynamic)record).Do((dynamic)pair.Item1, (dynamic)pair.Item2);
    }

    public class Collisions
    {
        private List<(object, object)> _pairs
            = new List<(object, object)>();

        public IEnumerable<(object, object)> Pairs => _pairs;

        public void TryBind(object left, object right)
        {
            foreach(var (a, b) in _pairs)
            {
                if (a == left && b == right)
                    return;

                if (a == right && b == left)
                    return;
            }

            _pairs.Add((left, right));
        }
    }
}

public abstract class Record
{
    public abstract bool IsTarget((object, object) pair);
}

public sealed class Record<T1, T2> : Record
{
    public Record(Action<T1, T2> onColllided)
    {
        _onCollided = onColllided;
    }
    private Action<T1, T2> _onCollided;

    public override bool IsTarget((object, object) pair)
    {
        if (pair.Item1 is T1 && pair.Item2 is T2)
            return true;

        if (pair.Item1 is T2 && pair.Item2 is T1)
            return true;

        return false;
    }

    public void Do(T1 left, T2 right)
    {
        _onCollided?.Invoke(left, right);
    }

    public void Do(T2 left, T1 right)
    {
        _onCollided?.Invoke(right, left);
    }
}