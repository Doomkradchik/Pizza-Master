using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PhysicsCompositeRoot : MonoBehaviour
{
    private static List<PhysicsEventBroadcaster> _broadcasters
        = new List<PhysicsEventBroadcaster>();

    public static PhysicsRouter Router { get; private set; }
    public static RecordsRegister Records { get; private set; }
    public static void RegisterPhysicsModel(PhysicsEventBroadcaster model)
    {
        _broadcasters.Add(model);
    }

    public static void DisregisterPhysicsModel(PhysicsEventBroadcaster model)
    {
        _broadcasters.Remove(model);
    }

    private void Awake()
    {
        Records = new RecordsRegister();
        Router = new PhysicsRouter(Records.GetRecords);

        StartCoroutine(RecordCollisions());
    }

    private void OnDisable()
    {
        StopCoroutine(RecordCollisions());
    }

    private IEnumerator RecordCollisions()
    {
        while (true)
        {
            Router.Launch();
            yield return null;
        }
    }
}