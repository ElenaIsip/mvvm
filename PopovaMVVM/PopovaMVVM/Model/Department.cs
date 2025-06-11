using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PopovaMVVM.Model
{
    public class Department : INotifyPropertyChanged
    {
        private int _departmentId;
        private string _name;
        private ObservableCollection<Position> _positions = new ObservableCollection<Position>();

        public int DepartmentId
        {
            get => _departmentId;
            set { _departmentId = value; OnPropertyChanged(); }
        }

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Position> Positions
        {
            get => _positions;
            set { _positions = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
