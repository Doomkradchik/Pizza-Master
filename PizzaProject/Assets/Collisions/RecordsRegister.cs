using System;
using System.Collections.Generic;

public class RecordsRegister
{
    public IEnumerable<Record> GetRecords()
    {
        yield return IfCollided((Player player, Topping topping) => {
            topping.OnPlayerCollided(player);
        });

        yield return IfCollided((Player player, Spring spring) => {
            spring.TryAccelerate(player);
        });

        yield return IfCollided((Player player, House house) => {
            house.CheckCurrentGameState();
        });
    }

    private Record<T1, T2> IfCollided<T1, T2>(Action<T1, T2> onCollided)
    {
        return new Record<T1, T2>(onCollided);
    }
}
