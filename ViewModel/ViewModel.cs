using Model;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Input;

namespace ViewModel
{
    public class CommandSharer : ICommand
    {
        private readonly Action handler; 
        private bool isEnabled; 

        public CommandSharer(Action handler) 
        {
            this.handler = handler; 
            IsEnabled = true; 
        }

        public bool IsEnabled 
        {
            get { return isEnabled; }
            set
            {
                if (value != isEnabled) 
                {
                    isEnabled = value; 
                    if (CanExecuteChanged != null) 
                    {
                        CanExecuteChanged(this, EventArgs.Empty); 
                    }
                }
            }
        }

        public bool CanExecute(object parameter) 
        {
            return IsEnabled; 
        }

        public event EventHandler CanExecuteChanged; 

        public void Execute(object parameter)  
        {
            handler();  
        }
    }



    public class BallInstances : INotifyPropertyChanged    
    {
        private Vector2 pos;                               

        public int r { get; set; }

        public BallInstances(float x, float y)
        {
            X = x;
            Y = y;
            r = 20;

        }
        public BallInstances(Vector2 position)
        {
            X = position.X;
            Y = position.Y;
            r = 20;
        }
        public void ChangePosition(Vector2 position)
        {
            this.X = position.X;
            this.Y = position.Y;
        }

        public float Y
        {
            get { return pos.Y; }
            set { pos.Y = value; OnPropertyChanged(); }
        }

        public float X
        {
            get { return pos.X; }
            set { pos.X = value; OnPropertyChanged(); }
        }

        public BallInstances()
        {
            X = 0;
            Y = 0;
            r = 20;
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
    }

    public class ViewModel : INotifyPropertyChanged   
    {
        private ModelClass model;
        public AsyncObservableCollection<BallInstances> Circles { get; set; }

       

        public ICommand BeginSceneButton { get; }
        public ICommand StopSceneButton { get; }
        public ICommand AddBallNumberButton { get; }
        public ICommand RemoveBallNumberButton { get; }


        public double sliderValue;
        public double SliderValue { get => sliderValue; set { if (sliderValue != value) { sliderValue = value; OnPropertyChanged(); } } }



        public ViewModel()
        {
            
            Circles = new AsyncObservableCollection<BallInstances>();

            model = new ModelClass();

            

            BeginSceneButton = new CommandSharer(() =>
            {
                
                model.SetLicznikKul((int)SliderValue);

                for (int i = 0; i < (int)SliderValue; i++)
                {
                    Circles.Add(new BallInstances());
                }

                model.ZmianaKoordynatow += (z, argv) =>
                {
                    if (Circles.Count > 0)
                        Circles[argv.ID].ChangePosition(argv.Koordynaty);
                };

                model.StartProgram();
            });

            StopSceneButton = new CommandSharer(() =>
            {
                model.StopProgram();
                Circles.Clear();
                
                model.SetLicznikKul((int)SliderValue);
            });
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
    }

    public class AsyncObservableCollection<T> : ObservableCollection<T>    
    {
        private SynchronizationContext _synchronizationContext = SynchronizationContext.Current;

        public AsyncObservableCollection()
        {
        }

        public AsyncObservableCollection(IEnumerable<T> list)
            : base(list)
        {
        }


        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            
            if (SynchronizationContext.Current == _synchronizationContext)
            {
                
                RaisePropertyChanged(e);
            }
            else
            {
                
                _synchronizationContext.Send(RaisePropertyChanged, e);
            }
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            
            if (SynchronizationContext.Current == _synchronizationContext)
            {
                
                RaiseCollectionChanged(e);
            }
            else
            {
                
                _synchronizationContext.Send(RaiseCollectionChanged, e);
            }
        }

        private void RaisePropertyChanged(object param)
        {
            
            base.OnPropertyChanged((PropertyChangedEventArgs)param);
        }
        private void RaiseCollectionChanged(object param)
        {
            
            base.OnCollectionChanged((NotifyCollectionChangedEventArgs)param);
        }

    }
}
