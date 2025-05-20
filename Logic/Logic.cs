using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class LogicAPI : ILogicAPI
    {
        //public IDataAPI _dataAPI;
        private IPhysicsLogic _physicsLogic;
        private IBusinessLogic _businessLogic;

        public LogicAPI()
        {
            //_dataAPI = new DataAPI();
        }

        public PhysicsLogic GetPhysicsLogic(float canvasWidth, float canvasHeight)
        {
            if (_physicsLogic == null)
            {
                _physicsLogic = new PhysicsLogic(canvasWidth, canvasHeight);
            }

            return (PhysicsLogic)_physicsLogic;
        }

        public BusinessLogic GetBusinessLogic(float canvasWidth, float canvasHeight)
        {
            if (_businessLogic == null)
            {
                _businessLogic = new BusinessLogic(canvasWidth, canvasHeight);
            }

            return (BusinessLogic)_businessLogic;
        }


    }
}
