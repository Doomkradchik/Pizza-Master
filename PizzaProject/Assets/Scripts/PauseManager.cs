using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private static List<IPauseHandler> _items =
        new List<IPauseHandler>();

    public static void Subscribe(IPauseHandler obj)
    {
        _items.Add(obj);
    }

    public static void Unsubscribe(IPauseHandler obj)
    {
        _items.Remove(obj);
    }

    public static void Unpause()
    {
        foreach (var item in _items.ToArray())
        {
            if (item == null)
            {
                Unsubscribe(item);
                return;
            }
            item.Unpause();
        }
    }

    public static void Pause()
    {
        foreach (var item in _items.ToArray())
        {
            if (item == null)
            {
                Unsubscribe(item);
                return;
            }
            item.Pause();
        }
    }
}

public interface IPauseHandler
{
    void Pause();
    void Unpause();
}
