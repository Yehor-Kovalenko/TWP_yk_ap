
using Logic;
using System.Collections.ObjectModel;

namespace Model
{
    public abstract class ModelAPI
    {
        public static ModelAPI Instantiate()
        {
            return new ModelManager(LogicAPI.Instantiate(1200, 484));
        }
        public abstract void removeAllBals();
        public abstract void startBallsMovement();
        public abstract void stopBallsMovement();
        public abstract void createBalls(int n);
        public abstract ObservableCollection<ModelBall> ReloadResub();

    }
}
