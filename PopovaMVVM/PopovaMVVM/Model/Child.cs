using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PopovaMVVM.Model
{
    public class Child : INotifyPropertyChanged
    {
        private int id;
        [Key]
        [Column("ChildId")] // Изменено на вероятное имя столбца в БД
        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }

        private string fullName;
        public string FullName
        {
            get => fullName;
            set
            {
                fullName = value;
                OnPropertyChanged();
            }
        }

        private DateTime dateOfBirth;
        [Column("BirthDate")] // Явно указываем имя столбца
        public DateTime DateOfBirth
        {
            get => dateOfBirth;
            set
            {
                dateOfBirth = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Age));
            }
        }

        private int employeeId;
        public int EmployeeId
        {
            get => employeeId;
            set
            {
                employeeId = value;
                OnPropertyChanged();
            }
        }

        [NotMapped]
        public int Age => DateTime.Now.Year - DateOfBirth.Year;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}