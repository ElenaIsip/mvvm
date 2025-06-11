using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PopovaMVVM.Model
{
    public class AdditionalPayment : INotifyPropertyChanged
    {
        private int _paymentId;
        private int _positionId;
        private decimal _amount;
        private string _description;

        private Position _position;
        public Position Position
        {
            get => _position;
            set { _position = value; OnPropertyChanged(); }
        }


        public int PaymentId
        {
            get => _paymentId;
            set { _paymentId = value; OnPropertyChanged(); }
        }

        public int PositionId
        {
            get => _positionId;
            set { _positionId = value; OnPropertyChanged(); }
        }

        public decimal Amount
        {
            get => _amount;
            set { _amount = value; OnPropertyChanged(); }
        }

        public string Description
        {
            get => _description;
            set { _description = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
