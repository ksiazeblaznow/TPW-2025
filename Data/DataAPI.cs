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
        Ball CreateBall(Vector2 position, float radius);
    }
}
