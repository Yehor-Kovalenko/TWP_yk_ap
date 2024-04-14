using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;
using Logic.Data;

namespace Tests
{
    public class BallRepositoryTests
    {

        //DataApi repo = DataApi.instantiate();

        public void TestAddBall()
        {
            Ball testBall = new Ball(15, 15, 15, 15, 5);
            DataApi repo = DataApi.instantiate();
            repo.addBall(testBall);
            Assert.AreEqual(repo.Balls.Count, 1);
            Assert.AreEqual(repo.Balls[0].posX, 15);
            Assert.AreEqual(repo.Balls[0].posY, 15);
            Assert.AreEqual(repo.Balls[0].speedX, 15);
            Assert.AreEqual(repo.Balls[0].speedY, 15);
            Assert.AreEqual(repo.Balls[0].radius, 5);
        }
    }
}
