using Quadradure4.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Quadradure4.Model
{
    public class WorkingDay : INotifyPropertyChanged
    {
        DateTime date;
        decimal qPyramids;
        decimal qBoxes;
        decimal qPrivals;
        public int Id { get; set; }
        
        public DateTime Date 
        {
            get => date;
            set
            {
                date = value;
                OnPropertyChanged();
            }
        }

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

        public List<SingleEntry> SingleEntries { get; set; } = null!;
        #region prop chan
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
        #endregion
    }
}
