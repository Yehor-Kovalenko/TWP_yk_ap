using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;
using Data;

namespace Tests
{
    [TestClass]
    public class BallRepositoryTests
    {
        DataApi repo = DataApi.instantiate();

        [TestMethod]
        public void TestAddBall()
        {
            //Ball testBall = new Ball(15, 15, 15, 15, 5);
            repo.addBall(new Ball(15, 15, 15, 15, 5));
            Assert.IsNotNull(repo.Balls);
            Assert.AreEqual(repo.Balls.Count, 1);
            Assert.AreEqual(repo.Balls[0].PosX, 15);
            Assert.AreEqual(repo.Balls[0].PosY, 15);
            Assert.AreEqual(repo.Balls[0].SpeedX, 15);
            Assert.AreEqual(repo.Balls[0].SpeedY, 15);
            Assert.AreEqual(repo.Balls[0].Radius, 5);
        }
    }
}
