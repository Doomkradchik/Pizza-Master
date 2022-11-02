using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PhysicsCompositeRoot : MonoBehaviour
{
    private static List<PhysicsEventBroadcaster> _broadcasters
        = new List<PhysicsEventBroadcaster>();

    private PhysicsRouter _physicsRouter;

    public static void RegisterPhysicsModel(PhysicsEventBroadcaster model)
    {
        _broadcasters.Add(model);
    }

    public static void DisregisterPhysicsModel(PhysicsEventBroadcaster model)
    {
        _broadcasters.Remove(model);
    }

    private void Start()
    {
        var register = new RecordsRegister();
        _physicsRouter = new PhysicsRouter(register.GetRecords);

        foreach (var broadcaster in _broadcasters)
            broadcaster.Init(_physicsRouter);

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
            _physicsRouter.Launch();
            yield return null;
        }
    }
}