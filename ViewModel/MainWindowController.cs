using Logic;
using Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ViewModel
{
    public class MainWindowController : INotifyPropertyChanged
    {
        private UserCommand commandGenerate;
        private ModelAPI modelAPI;
        private UserCommand commandStop;
        private UserCommand commandResume;
        private UserCommand commandReset;
        private bool switchStop = false;
        private double sliderValue;

        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<ModelBall> ModelBalls => modelAPI.ReloadResub();


        public UserCommand CommandGenerate { get => commandGenerate; set => commandGenerate = value; }
        public UserCommand CommandStop { get => commandStop; set => commandStop = value; }
        public UserCommand CommandResume { get => commandResume; set => commandResume = value; }
        public UserCommand CommandReset { get => commandReset; set => commandReset = value; }
        public bool SwitchStop { get => switchStop; set { switchStop = value; NotifyPropertyChanged(); } }    
        public ModelAPI ModelAPI { get => modelAPI; set => modelAPI = value; }
        public double SliderValue { get => sliderValue; set { if (sliderValue != value) { sliderValue = value; NotifyPropertyChanged();  } } }
        
     
        //collection of balls//

        public MainWindowController()
        {
            CommandGenerate = new UserCommand(generate, generateStatus);
            CommandStop = new UserCommand(stop, stopStatus);
            CommandResume = new UserCommand(resume, resumeStatus);
            CommandReset = new UserCommand(reset, resetStatus);
            modelAPI = ModelAPI.Instantiate();
            //modelAPi instantiate//
        }

        private bool generateStatus()
        {
            return true;
        }

        private void generate()
        {
            this.modelAPI.createBalls((int)sliderValue);
            NotifyPropertyChanged(nameof(ModelBalls));
            this.modelAPI.startBallsMovement();
            SwitchStop = true;
        }

        private bool stopStatus()
        {
            return SwitchStop;
        }

        private void stop()
        {
            modelAPI.stopBallsMovement();
            NotifyPropertyChanged(nameof(ModelBalls));
            SwitchStop = !SwitchStop;
        }
        
        private bool resumeStatus()
        {
            return !SwitchStop;
        }

        private void resume()
        {
            modelAPI.startBallsMovement();
            NotifyPropertyChanged(nameof(ModelBalls));
            SwitchStop = !SwitchStop;
        }

        private bool resetStatus()
        {
            return true;
        }

        private void reset()
        {
            modelAPI.stopBallsMovement();
            SwitchStop = false;
            modelAPI.removeAllBals();
            NotifyPropertyChanged(nameof(ModelBalls));


        }

        private void NotifyPropertyChanged([CallerMemberName] string? propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public static int Main()
        {
            return 0;

        }
    }
}
