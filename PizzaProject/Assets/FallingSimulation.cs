
public class FallingSimulation : Simulation<FallingSimulationComponent>
{
    public override void StartSimulation(FallingSimulationComponent entity)
    {
        base.StartSimulation(entity);

        entity.StartFalling(() => Stop(entity));
    }
}