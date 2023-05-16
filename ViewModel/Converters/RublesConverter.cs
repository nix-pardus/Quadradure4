using Microsoft.EntityFrameworkCore;
using Quadradure4.Model;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Quadradure4.ViewModel.Converters
{
    internal class RublesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<Rate> rates = new List<Rate>();
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Rate.Load();
                rates = db.Rate.ToList();
            }
            WorkingDay? workingDay = value as WorkingDay;
            decimal rubles = 0;
            if (rates.Count > 0)
            {
                if (workingDay!.Date.DayOfWeek == DayOfWeek.Sunday || workingDay!.Date.DayOfWeek == DayOfWeek.Saturday || workingDay!.IsHolidayDay)
                    rubles +=
                        workingDay.QPyramids * rates.Where(x => x.IsWeekend && x.Сontainer == Сontainer.Pyramid).Last().Price +
                        workingDay.QPrivals * rates.Where(x => x.IsWeekend && x.Сontainer == Сontainer.Prival).Last().Price +
                        workingDay.QBoxes * rates.Where(x => x.IsWeekend && x.Сontainer == Сontainer.Box).Last().Price;
                else
                    rubles +=
                        workingDay.QPyramids * rates.Where(x => !x.IsWeekend && x.Сontainer == Сontainer.Pyramid).Last().Price +
                        workingDay.QPrivals * rates.Where(x => !x.IsWeekend && x.Сontainer == Сontainer.Prival).Last().Price +
                        workingDay.QBoxes * rates.Where(x => !x.IsWeekend && x.Сontainer == Сontainer.Box).Last().Price;
            }
            return rubles;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
