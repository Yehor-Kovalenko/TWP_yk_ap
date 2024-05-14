using Data;
using Logic;
using System.Numerics;

namespace TestBalls
{
    [TestClass]
    public class BallMovementTests
    {
        [TestMethod]
        public void BallMovementTest()
        {
            LogicAPI test = new BallController(70, 70);
            test.addBalls(5);
            Vector2 testPos = test.getBall(0).Position;
            test.Start();
            Thread.Sleep(100);
            Vector2 testPos2 = test.getBall(0).Position;
            Assert.AreNotEqual(testPos, testPos2);
        }
    }
}