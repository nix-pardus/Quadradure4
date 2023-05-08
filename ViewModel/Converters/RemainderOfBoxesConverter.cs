using Quadradure4.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Quadradure4.ViewModel.Converters
{
    internal class RemainderOfBoxesConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            decimal allQuadrature;
            decimal.TryParse(values[0].ToString(), out allQuadrature);
            WorkingDay workingDay = (WorkingDay)values[1];
            foreach (SingleEntry item in workingDay.SingleEntries)
            {
                allQuadrature -= item.QBoxes;
            }
            return allQuadrature.ToString("N2");
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
