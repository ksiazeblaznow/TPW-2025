using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Logic
{
    public interface IBusinessLogic
    {
        public Ball CreateBall(Vector2 position, float radius);
    }
}
