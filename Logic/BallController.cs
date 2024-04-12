using System;

/// <summary>
/// Summary description for Class1
/// </summary>
public class BallController : LogicApi {

    readonly int boardWidth;
    readonly int boardHeight;

    public int BoardWidth
    {
        get => boardWidth;
    }

    public int BoardHeight
    {
        get => boardHeight;
    }

    public override Ball createBall(int posX, int posY, int speedX, int speedY, int radius)
    {
        return new Ball(posX, posY, speedX, speedY, radius);
    }



}
