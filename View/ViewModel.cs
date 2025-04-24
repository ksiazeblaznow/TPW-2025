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
        public ObservableCollection<IBall> Balls { get; }

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

            Balls = new ObservableCollection<IBall>();

            var velocity = new System.Numerics.Vector2(1.0f, 1.0f);
            var velocity1 = new System.Numerics.Vector2(2.0f, 3.0f);
            var velocity2 = new System.Numerics.Vector2(-0.5f, 1.0f);

            Balls.Add((logic.CreateBall(new System.Numerics.Vector2(50.0f, 200.0f), 40.0f, velocity)));
            Balls.Add((logic.CreateBall(new System.Numerics.Vector2(100.0f, 50.0f), 20.0f, velocity1)));
            Balls.Add((logic.CreateBall(new System.Numerics.Vector2(200.0f, 300.0f), 10.0f, velocity2)));

            Balls[0].GetType();

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(5);
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
