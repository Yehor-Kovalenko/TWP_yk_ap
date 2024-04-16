using Logic;
using Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ViewModel
{
    class MainWindowController : INotifyPropertyChanged
    {
        private UserCommand commandGenerate;
        private ModelManager modelManager;
        private UserCommand commandStop;
        private UserCommand commandResume;
        private UserCommand commandReset;
        private bool switchStop = false;
        private double sliderValue;

        public event PropertyChangedEventHandler? PropertyChanged;

        public UserCommand CommandGenerate { get => commandGenerate; set => commandGenerate = value; }
        public UserCommand CommandStop { get => commandStop; set => commandStop = value; }
        public UserCommand CommandResume { get => commandResume; set => commandResume = value; }
        public UserCommand CommandReset { get => commandReset; set => commandReset = value; }
        public bool SwitchStop { get => switchStop; set { switchStop = value; NotifyPropertyChanged(); } }    
        public ModelManager ModelManager { get => modelManager; set => modelManager = value; }
        public double SliderValue { get => sliderValue; set { if (sliderValue != value) { sliderValue = value; NotifyPropertyChanged();  } } }

        //collection of balls//

        public MainWindowController()
        {
            CommandGenerate = new UserCommand(generate, generateStatus);
            CommandStop = new UserCommand(stop, stopStatus);
            CommandResume = new UserCommand(resume, resumeStatus);
            CommandReset = new UserCommand(reset, resetStatus);
            //modelAPi instantiate//
        }

        private bool generateStatus()
        {
            return true;
        }

        private void generate()
        {
            this.modelManager.createBalls((int)SliderValue);
            NotifyPropertyChanged(nameof(ModelManager));
            this.modelManager.startBallsMovement();
            SwitchStop = true;
        }

        private bool stopStatus()
        {
            return SwitchStop;
        }

        private void stop()
        {
            ModelManager.stopBallsMovement();
            NotifyPropertyChanged(nameof(ModelManager));
            SwitchStop = !SwitchStop;
        }
        
        private bool resumeStatus()
        {
            return !SwitchStop;
        }

        private void resume()
        {
            ModelManager.startBallsMovement();
            NotifyPropertyChanged(nameof(ModelManager));
            SwitchStop = !SwitchStop;
        }

        private bool resetStatus()
        {
            return true;
        }

        private void reset()
        {
            ModelManager.stopBallsMovement();
            SwitchStop = false;
            ModelManager.removeAllBals();
            NotifyPropertyChanged(nameof(ModelManager));


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
