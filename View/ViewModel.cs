using Presentation.Behaviors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using Logic;
using Data;
using System.Windows;


namespace Presentation
{
    public class ViewModel : INotifyPropertyChanged, IHaveCanvasSize
    {
        private readonly DispatcherTimer _timer;
        public ObservableCollection<Ball> Balls { get; }

        private int _liczba;
        public int Liczba
        {
            get => _liczba;
            set
            {
                _liczba = value;
                //OnPropertyChanged();
            }
        }

        public void SetCanvasSize(double width, double height)
        {
            CanvasWidth = width;
            CanvasHeight = height;
        }

        private double _canvasWidth;
        public double CanvasWidth
        {
            get => _canvasWidth;
            set
            {
                _canvasWidth = value;
                OnPropertyChanged();
            }
        }

        private double _canvasHeight;
        public double CanvasHeight
        {
            get => _canvasHeight;
            set
            {
                _canvasHeight = value;
                OnPropertyChanged();
            }
        }

        public ICommand StartCommand { get; }

        public ViewModel()
        {
            StartCommand = new RelayCommand(OnButtonClick);
            IBusinessLogic logic = new BusinessLogic();
            IPhysicsLogic physics = new PhysicsLogic();

            logic.CreateBalls(3);
            Balls = logic.GetBalls();

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(10);
            _timer.Tick += (s, e) =>
            {
                //Console.WriteLine("Ticked...");
                foreach (Ball b in Balls) {
                    logic.UpdateBall(b, CanvasWidth, CanvasHeight);
                    //Console.WriteLine(b.Position.ToString());
                }
                
            };
            _timer.Start();
        }

        private void OnButtonClick()
        {
            Console.WriteLine($"Wprowadzona liczba: {Liczba}");
            Console.WriteLine($"Canvas: {CanvasWidth} x {CanvasHeight}");
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
