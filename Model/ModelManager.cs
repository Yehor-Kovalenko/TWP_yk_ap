using Data;
using Logic;
using System.Collections.ObjectModel;

namespace Model
{
    public class ModelManager
    {
        private readonly LogicAPI logicAPI;

        public ModelManager(int boardHeight, int boardWidth)
        {
            this.logicAPI = LogicAPI.Instantiate(boardHeight, boardWidth);
        }
        public List<Ball> GetBalls { get => logicAPI.Repo.Balls; }
        public List<Ball> getAllBalls()
        {
            return logicAPI.Repo.Balls;
        }

        public void removeAllBals()
        {
            this.logicAPI.removeAllBalls();
        }

        public void startBallsMovement()
        {
            this.logicAPI.startAllBalls();
        }

        public void stopBallsMovement()
        {
            this.logicAPI.stopAllBalls();
        }

        public void createBalls(int n)
        {
            for (int i = 0; i < n; i++)
            {
                this.logicAPI.createBallAtRandomPosition();
            }
        }
        public static int Main()
        {
            return 0;

        }
    }
}
