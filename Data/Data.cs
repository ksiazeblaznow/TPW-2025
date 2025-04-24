using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DataAPI : IDataAPI
    {
        public Ball ConstructBall(Vector2 position, float radius)
        {
            return new Ball(position, radius);
        }
    }
}
