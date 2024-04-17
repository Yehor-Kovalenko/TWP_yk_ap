using Logic;
using System.Numerics;

namespace TestBalls
{
    [TestClass]
    public class UT
    {
        [TestMethod]
        public void Test1()
        {
            LogicAPI test = new BallController(50, 50);
            test.addBall(14);
            Assert.AreEqual(14, test.getBallsCount());

        }

        [TestMethod]
        public void Test2()
        {
            int iterations = 0;
            LogicAPI test2 = new BallController(70, 70);
            test2.addBall(11);

            List<Vector2> balls = new List<Vector2>();
            for (int i = 0; i < test2.getBallsCount(); i++)
            {
                balls.Add(test2.getBall(i).Position);
            }

            test2.PositionChangeEvent += (_, _) =>
            {
                iterations++;
                if (iterations >= 30)
                {
                    test2.Stop();
                }
            };
            test2.Start();
            while (iterations < 30)
            { }
            Assert.IsTrue(iterations >= 29);
            for (int i = 0; i < test2.getBallsCount(); i++)
            {
                if (balls[i] == test2.getBall(i).Position)
                {
                    Assert.Fail();
                }
            }
        }
    }
}