using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Quadradure4.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Quadradure4.ViewModel
{
    internal class ApplicationViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<WorkingDay> workingDays = null!;
        private ObservableCollection<Person> persons = null!;

        public ObservableCollection<WorkingDay> WorkingDays
        {
            get => workingDays;
            set
            {
                workingDays = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Person> Persons
        {
            get => persons;
            set
            {
                persons = value;
                OnPropertyChanged();
            }
        }
        public IList<DateTime> Years { get; set; } = null!;
        public DateTime Now { get; set; } = DateTime.Now;
        public ApplicationViewModel()
        {
            Load();
        }


        public void Load()
        {
            CheckingCreationDataBeforeCurrentMonth();
            using (ApplicationContext db = new ApplicationContext())
            {
                var data = (from d in db.Workdays
                            group d by new
                            {
                                Year = d.Date.Year
                            } into g
                            select new
                            {
                                Year = g.Key.Year
                            }).AsEnumerable();
                Years = new ObservableCollection<DateTime>();
                foreach (var el in data)
                {
                    if (!Years.Any(x => x.Year == el.Year))
                        Years.Add(new DateTime(el.Year, 1, 1));
                }




                db.Persons.Load();
                Persons = db.Persons.Local.ToObservableCollection();
                db.Workdays.Include(x => x.SingleEntries).Load();
                WorkingDays = db.Workdays.Local.ToObservableCollection();
            }
        }

        private void CheckingCreationDataBeforeCurrentMonth()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                DateTime date;
                if (db.Workdays.Any())
                    date = db.Workdays.OrderBy(x => x.Date).Last().Date;
                else
                    date = new DateTime(2200, 1, 1);
                DateTime now = DateTime.Now;
                int diffYear = now.Year - date.Year;
                int currentYear = date.Year;

                if (date.Month < now.Month)
                {
                    for (int month = date.Month + 1; month <= now.Month; month++)
                    {
                        int daysInMounth = DateTime.DaysInMonth(now.Year, month);
                        for (int day = 1; day <= daysInMounth; day++)
                        {
                            db.Workdays.Add(new WorkingDay { Date = new DateTime(GetYear(month), month, day) });
                        }
                    }
                    db.SaveChanges();
                }
                else if(date.Month > now.Month)
                {
                    for (int day = 1; day <= DateTime.DaysInMonth(now.Year, now.Month); day++)
                    {
                        db.Workdays.Add(new WorkingDay { Date = new DateTime(now.Year, now.Month, day) });
                    }
                }
                int GetYear(int month)
                {
                    if (diffYear > 0)
                    {
                        if (month == 1)
                        {
                            diffYear--;
                            return ++currentYear;
                        }
                        else
                            return currentYear;
                    }
                    else
                    {
                        return now.Year;
                    }
                }
            }
        }

        public void LoadSelectedMonthAndYear(int month, int year)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Workdays.Where(x => x.Date.Month == month && x.Date.Year == year).Include(x => x.SingleEntries).Load();
                WorkingDays = db.Workdays.Local.ToObservableCollection();
            }
        }

        public void LoadPersons()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Persons.Load();
                Persons = db.Persons.Local.ToObservableCollection();
            }
        }

        public void Save()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Workdays.UpdateRange(WorkingDays);
                db.SaveChanges();
            }
        }

        public void DistributeEqually(WorkingDay item)
        {
            List<SingleEntry> entries = item.SingleEntries.ToList();
            for (int i = 0; i < entries.Count; i++)
            {
                entries[i].QPyramids = item.QPyramids / entries.Count;
                entries[i].QBoxes = item.QBoxes / entries.Count;
                entries[i].QPrivals = item.QPrivals / entries.Count;
            }
            WorkingDays.FirstOrDefault(x => x.Id == item.Id)!.SingleEntries = entries;
        }

        public void AddNewPerson(string name)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Person person = new Person { Name = name };
                db.Persons.Add(person);
                db.SaveChanges();
            }
            LoadPersons();
        }

        public void DeletePerson(Person person)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Persons.Remove(person);
                db.SaveChanges();
            }
            LoadPersons();
        }
        #region prop chan
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string prop = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        #endregion
    }
}
