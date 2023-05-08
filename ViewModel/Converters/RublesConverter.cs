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
            //здесь надо добавить в условие праздничные дни
            if (rates.Count > 0)
            {
                if (workingDay?.Date.DayOfWeek == DayOfWeek.Sunday || workingDay?.Date.DayOfWeek == DayOfWeek.Saturday)
                    rubles += workingDay.QPyramids * rates[3].Price + workingDay.QPrivals * rates[4].Price + workingDay.QBoxes * rates[5].Price;
                else
                    rubles += workingDay!.QPyramids * rates[0].Price + workingDay.QPrivals * rates[1].Price + workingDay.QBoxes * rates[2].Price;
            }
            return rubles;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
