using Data;
using Logic;
using System.Collections.ObjectModel;

namespace Model
{
    public class ModelManager : ModelAPI
    {
        private readonly LogicAPI logicAPI;
        private ObservableCollection<ModelBall> ModelBalls = new();

        public ModelManager(LogicAPI logicAPI)
        {
            this.logicAPI = logicAPI;
        }

        public override void removeAllBals()
        {
            this.logicAPI.removeAllBalls();
        }

        public override void startBallsMovement()
        {
            this.logicAPI.startAllBalls();
        }

        public override void stopBallsMovement()
        {
            this.logicAPI.stopAllBalls();
        }

        public override void createBalls(int n)
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

        public override ObservableCollection<ModelBall> ReloadResub()
        {
            ModelBalls.Clear();
            foreach (Ball b in logicAPI.Repo.Balls)
            {
                ModelBall modelBall = new ModelBall(b.PosX, b.PosY, b.Radius);
                ModelBalls.Add(modelBall);
                b.PropertyChanged += modelBall.Update!;
            }
            return ModelBalls;
        }
    }
}
