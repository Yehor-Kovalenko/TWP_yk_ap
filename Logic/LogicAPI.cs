using System;

/// <summary>
/// Summary description for Class1
/// </summary>
public abstract class LogicAPI
{
    public abstract Ball createBall();

    public abstract void removeBall();

    public abstract void removeAllBalls();

    public abstract void stopBall();

    public abstract void stopAllBalls();

    public abstract Ball createBallAtRandomPlace();

    public abstract void startAllBalls();
}
 