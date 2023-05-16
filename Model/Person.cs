using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Quadradure4.Model
{
    public enum Status
    {
        Active,
        Dismissed
    }
    public class Person : INotifyPropertyChanged
    {
        string name = null!;

        public Person()
        {
            Status = Status.Active;
        }

        public int Id { get; set; }
        public string Name 
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        public Status Status { get; set; }

        public List<SingleEntry> SingleEntries { get; set; } = null!;
        #region propertyChanged   
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
