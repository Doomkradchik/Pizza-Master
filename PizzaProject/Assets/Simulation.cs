using System;
using System.Collections.Generic;
using UnityEngine;

public class Simulation<T> : MonoBehaviour
{
    protected List<T> _entities = new List<T>();

    public event Action<T> Started;
    public event Action<T> Ended;

    public virtual void StartSimulation(T entity)
    {
        _entities.Add(entity);
        Started?.Invoke(entity);
    }

    public virtual void Stop(T entity)
    {
        _entities.Remove(entity);
        Ended?.Invoke(entity);
    }


}