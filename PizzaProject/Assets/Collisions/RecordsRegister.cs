using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }

    private Record<T1, T2> IfCollided<T1, T2>(Action<T1, T2> onCollided)
    {
        return new Record<T1, T2>(onCollided);
    }
}
