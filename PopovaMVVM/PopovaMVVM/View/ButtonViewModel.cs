using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace PopovaMVVM.View
{
    public class ButtonViewModel : ObservableObject
    {
        public RelayCommand AddButtonCommand { get; protected set; }
        public RelayCommand DeleteButtonCommand { get; protected set; }
        public RelayCommand EditButtonCommand { get; protected set; }
        public RelayCommand SaveButtonCommand { get; protected set; }
        public RelayCommand ExitButtonCommand { get; }

        public ButtonViewModel()
        {
            AddButtonCommand = new RelayCommand(_ => AddData());
            DeleteButtonCommand = new RelayCommand(_ => DeleteData(), _ => CanDelete());
            EditButtonCommand = new RelayCommand(_ => EditData(), _ => CanEdit());
            SaveButtonCommand = new RelayCommand(_ => SaveData(), _ => CanSave());
            ExitButtonCommand = new RelayCommand(_ =>
            {
                if (System.Windows.Application.Current != null)
                    System.Windows.Application.Current.Shutdown();
            });
        }

        protected virtual void AddData() { }
        protected virtual void DeleteData() { }
        protected virtual void EditData() { }
        protected virtual void SaveData() { }

        protected virtual bool CanDelete() => false;
        protected virtual bool CanEdit() => false;
        protected virtual bool CanSave() => false;
    }
}
