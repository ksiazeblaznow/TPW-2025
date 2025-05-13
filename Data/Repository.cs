using Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Repository : INotifyPropertyChanged
    {
        public ObservableCollection<Ball> Balls = new();

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
