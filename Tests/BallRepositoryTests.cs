using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;
using Logic.Data;

namespace Tests
{
    public class BallRepositoryTests
    {

        DataApi repo = DataApi.instantiate();

        public void TestAddBall()
        {
            Ball testBall = new(15, 15, 15, 15, 5);
            repo.AddBall(testBall);
            Assert.AreEqual(repo.balls.Count(), 1);
            Assert.AreEqual(repo.balls[0].posX, 15);
            Assert.AreEqual(repo.balls[0].posY, 15);
            Assert.AreEqual(repo.balls[0].speedX, 15);
            Assert.AreEqual(repo.balls[0].speedY, 15);
            Assert.AreEqual(repo.balls[0].radius, 5);
        }
    }
}
