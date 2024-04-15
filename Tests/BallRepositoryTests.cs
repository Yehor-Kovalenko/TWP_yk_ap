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
            repo.addBall(new Ball(15, 15, 15, 15, 5));
            Assert.IsNotNull(repo.Balls);
            Assert.AreEqual(1, repo.Balls.Count);
            Assert.AreEqual(15, repo.Balls[0].PosX);
            Assert.AreEqual(15, repo.Balls[0].PosY);
            Assert.AreEqual(15, repo.Balls[0].SpeedX);
            Assert.AreEqual(15, repo.Balls[0].SpeedY);
            Assert.AreEqual(5, repo.Balls[0].Radius);
        }

        [TestMethod]
        public void TestRemoveBall()
        {
            for(int i = 0; i < 5; i++)
            {
                repo.addBall(new Ball(15 + i, 15 + i, 15 + i, 15 + i, 5 + i));
            }
            Assert.AreEqual(5, repo.Balls.Count);
            repo.removeBall(repo.Balls[repo.Balls.Count - 1]);
            Assert.AreEqual(4, repo.Balls.Count);
        }

        [TestMethod]
        public void TestRemoveAllBalls()
        {
            for (int i = 0; i < 5; i++)
            {
                repo.addBall(new Ball(15 + i, 15 + i, 15 + i, 15 + i, 5 + i));
            }
            Assert.AreEqual(5, repo.Balls.Count);
            repo.removeAllBalls();
            Assert.AreEqual(0, repo.Balls.Count);
        }
    }
}