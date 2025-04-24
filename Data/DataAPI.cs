using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IBall
    {
        
    }

    public interface IDataAPI
    {
        Ball ConstructBall(Vector2 position, float radius);
    }
}
