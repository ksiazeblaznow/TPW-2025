using Data;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;

namespace Logic
{
    public class BusinessLogic : IBusinessLogic
    {
        private IDataAPI _dataAPI;

        public BusinessLogic(IDataAPI dataAPI)
        {
            _dataAPI = dataAPI;
        }

        public Ball CreateBall(Vector2 position, float radius)
        {
            return _dataAPI.ConstructBall(position, radius);
        }
    }
}
