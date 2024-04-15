using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;
using Data;
using System.Numerics;

namespace Tests
{
    [TestClass]
    public class BallTests
    {

        Ball testBall = new Ball(15, 15, 15, 15, 5);
        Vector2 bSize = new Vector2(140, 140);

        [TestMethod]
        public void TestMove()
        {
            testBall.move(bSize);
            Assert.AreEqual(30, testBall.PosX);
            Assert.AreEqual(30, testBall.PosY);
            testBall.PosX = 139;
            testBall.PosY = 139;
            testBall.move(bSize);
            Assert.AreEqual(124, testBall.PosX);
            Assert.AreEqual(124, testBall.PosY);
            Assert.AreEqual(-15, testBall.SpeedX);
            Assert.AreEqual(-15, testBall.SpeedY);
        }

        [TestMethod]
        public void TestStartMovement()
        {
            int pos0x = testBall.PosX;
            int pos0y = testBall.PosY;
            testBall.startMovement((int)bSize[0], (int)bSize[1]);
            Assert.IsNotNull(testBall.BallTimer);
            Assert.AreNotEqual(pos0x, testBall.PosX);
            Assert.AreNotEqual(pos0y, testBall.PosY);
        }
    }
}
