using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Presentation
{
    public class ViewModel : INotifyPropertyChanged
    {
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

        public ICommand StartCommand { get; }

        public ViewModel()
        {
            StartCommand = new RelayCommand(OnButtonClick);
        }

        private void OnButtonClick()
        {
            Console.WriteLine($"Wprowadzona liczba: {Liczba}");
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
