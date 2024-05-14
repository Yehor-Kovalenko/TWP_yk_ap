using Logic;
using System.Numerics;

namespace TestBalls
{
    [TestClass]
    public class BallControllerTests
    {
        [TestMethod]
        public void AddBallTest()
        {
            LogicAPI test = new BallController(50, 50);
            test.addBalls(14);
            Assert.AreEqual(14, test.getBallsCount());
        }
    }
}