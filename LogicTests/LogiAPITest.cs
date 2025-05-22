using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;

namespace LogicTests
{
    [TestClass]
    public class LogicApiTests
    {
        [TestMethod]
        public void GetBusinessLogic_ReturnsSameInstance()
        {
            LogicAPI logicAPI = new LogicAPI();

            var logic1 = logicAPI.GetBusinessLogic(500, 500);
            var logic2 = logicAPI.GetBusinessLogic(500, 500);

            Assert.AreSame(logic1, logic2);
        }

        [TestMethod]
        public void GetPhysicsLogic_ReturnsSameInstance()
        {
            LogicAPI logicAPI = new LogicAPI();

            var physics1 = logicAPI.GetPhysicsLogic(500, 500);
            var physics2 = logicAPI.GetPhysicsLogic(500, 500);

            Assert.AreSame(physics1, physics2);
        }
    }
}
