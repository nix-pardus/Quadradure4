using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Quadradure4.Model
{
    public class SingleEntry : INotifyPropertyChanged
    {
        Person _person = null!;
        WorkingDay _workingDay = null!;
        //Rate _rate = null!;
        decimal qPyramids;
        decimal qBoxes;
        decimal qPrivals;
        public int Id { get; set; }

        public Person Person
        {
            get => _person;
            set
            {
                _person = value;
                OnPropertyChanged();
            }
        }

        public WorkingDay WorkingDay
        {
            get => _workingDay;
            set
            {
                _workingDay = value;
                OnPropertyChanged();
            }
        }

        //public Rate Rate
        //{
        //    get => _rate;
        //    set
        //    {
        //        _rate = value;
        //        OnPropertyChanged("Rate");
        //    }
        //}

        public decimal QPyramids
        {
            get => qPyramids;
            set
            {
                qPyramids = value;
                OnPropertyChanged();
            }
        }

        public decimal QBoxes
        {
            get => qBoxes;
            set
            {
                qBoxes = value;
                OnPropertyChanged();
            }
        }

        public decimal QPrivals
        {
            get => qPrivals;
            set
            {
                qPrivals = value;
                OnPropertyChanged();
            }
        }
        #region prop chan
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
        #endregion
    }
}
