using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Quadradure4.Model
{
    public enum Сontainer
    {
        Pyramid,
        Box,
        Prival
    }
    public class Rate : INotifyPropertyChanged
    {
        Сontainer container;
        decimal price;
        private bool isWeekend;

        public int Id { get; set; }

        public Сontainer Сontainer
        {
            get => container;
            set
            {
                container = value;
                OnPropertyChanged();
            }
        }

        public decimal Price
        {
            get => price;
            set
            {
                price = value;
                OnPropertyChanged();
            }
        }

        public bool IsWeekend
        {
            get => isWeekend;
            set
            {
                isWeekend = value;
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
