
namespace ViewModel
{
    internal class MainWindowController
    {
        private UserCommand commandGenerate;
        private UserCommand commandSlider;

        //logicAPI z modelu//

        public UserCommand CommandGenerate { get => commandGenerate; set => commandGenerate = value; }
        public UserCommand CommandSlider { get => commandSlider; set => commandSlider = value; }
        //property na modelapi//
        //collection of balls//

        public MainWindowController()
        {
            CommandGenerate = new UserCommand(Start, StartStatus);
            CommandSlider = new UserCommand(Create, CreateStatus);
            //modelAPi instantiate//
        }

        private bool StartStatus()
        {
            return true;
        }

        private bool CreateStatus()
        {
            return true;
        }
    }
}
