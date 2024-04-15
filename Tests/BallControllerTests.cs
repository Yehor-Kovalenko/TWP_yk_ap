using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;
using Data;
using System.Threading;

namespace Tests
{
    [TestClass]
    public class BallControllerTests
    {
        LogicAPI bCtrlr = LogicAPI.Instantiate(140, 140);

        [TestMethod]
        public void TestCreateBall()
        {
            bCtrlr.createBall(15, 15, 15, 15, 5);
            Assert.IsNotNull(bCtrlr.Repo);
            Assert.IsNotNull(bCtrlr.Repo.Balls);
            Assert.AreEqual(15, bCtrlr.Repo.Balls[0].PosX);
        }

        [TestMethod]
        public void TestCreateBallAtRandomPosition()
        {
            bCtrlr.createBallAtRandomPosition();
            bCtrlr.createBallAtRandomPosition();
            Assert.AreNotEqual(bCtrlr.Repo.Balls[0].PosX, bCtrlr.Repo.Balls[1].PosX);
        }

        [TestMethod]
        public void TestStartAllBalls()
        {
            for (int i = 0; i < 5; i++)
            {
                bCtrlr.createBall(15 + i, 15 + i, 15 + i, 15 + i, 5 + i);
            }
            bCtrlr.startAllBalls();
            for (int i = 0; i < 5; i++)
            {
                Assert.IsNotNull(bCtrlr.Repo.Balls[i].BallTimer);
                int pos0x = bCtrlr.Repo.Balls[i].PosX;
                int pos0y = bCtrlr.Repo.Balls[i].PosY;
                Thread.Sleep(50);
                Assert.AreNotEqual(pos0x, bCtrlr.Repo.Balls[i].PosX);
                Assert.AreNotEqual(pos0y, bCtrlr.Repo.Balls[i].PosY);
            }
            for (int i = 0; i < 5; i++)
            {
                bCtrlr.Repo.Balls[i].BallTimer.Dispose();
            }
        }

        [TestMethod]
        public void TestStopAllBalls()
        {
            for (int i = 0; i < 5; i++)
            {
                bCtrlr.createBall(15 + i, 15 + i, 15 + i, 15 + i, 5 + i);
            }
            bCtrlr.startAllBalls();
            Thread.Sleep(50);
            bCtrlr.stopAllBalls();
            for (int i = 0; i < 5; i++)
            {
                int pos0x = bCtrlr.Repo.Balls[i].PosX;
                int pos0y = bCtrlr.Repo.Balls[i].PosY;
                Assert.AreEqual(pos0x, bCtrlr.Repo.Balls[i].PosX);
                Assert.AreEqual(pos0y, bCtrlr.Repo.Balls[i].PosY);
            }
        }

        [TestMethod]
        public void TestRemoveBall()
        {
            for (int i = 0; i < 5; i++)
            {
                bCtrlr.createBall(15 + i, 15 + i, 15 + i, 15 + i, 5 + i);
            }
            Assert.AreEqual(5, bCtrlr.Repo.Balls.Count);
            bCtrlr.removeBall(bCtrlr.Repo.Balls[bCtrlr.Repo.Balls.Count - 1]);
            Assert.AreEqual(4, bCtrlr.Repo.Balls.Count);
        }

        [TestMethod]
        public void TestRemoveAllBalls()
        {
            for (int i = 0; i < 5; i++)
            {
                bCtrlr.createBall(15 + i, 15 + i, 15 + i, 15 + i, 5 + i);
            }
            Assert.AreEqual(5, bCtrlr.Repo.Balls.Count);
            bCtrlr.removeAllBalls();
            Assert.AreEqual(0, bCtrlr.Repo.Balls.Count);
        }
    }
}