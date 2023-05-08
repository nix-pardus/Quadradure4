using Quadradure4.Model;
using Quadradure4.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Quadradure4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int x = 0;
        public MainWindow()
        {
            InitializeComponent();
            this.Language = XmlLanguage.GetLanguage("ru");
            DataContext = new ApplicationViewModel();

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            string text = ((TextBox)sender).Text;
            if (text.Contains('.') && text.Last() != '.')
            {
                text = text.Replace('.', ',');
                ((TextBox)sender).Text = text;
                ((TextBox)sender).CaretIndex = ((TextBox)sender).Text.Length;
            }
            ((ApplicationViewModel)DataContext).Save();
            var stackPanel = ((sender as TextBox)?.Parent as Grid)?.Parent as StackPanel;
            var stackPanel2 = stackPanel?.Children[2] as StackPanel;
            TextBlock? textBlock = stackPanel2?.Children[1] as TextBlock;
            BindingOperations.GetBindingExpression(textBlock, TextBlock.TextProperty).UpdateTarget();

        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            ((ApplicationViewModel)DataContext).Save();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ((ApplicationViewModel)DataContext).Save();
        }

        private void DataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            ((ApplicationViewModel)DataContext).Save();
            var p = (sender as DataGrid)?.Parent as StackPanel;
            var p1 = p.Parent as StackPanel;
            var children = p1?.Children;
            var ch1 = children?[0];
            var ch2 = (ch1 as StackPanel)?.Children[1];
            var ch3 = (ch2 as StackPanel)?.Children[1];
            var tBox1 = (ch3 as Grid)?.Children[8] as TextBox;
            var tBox2 = (ch3 as Grid)?.Children[9] as TextBox;
            var tBox3 = (ch3 as Grid)?.Children[10] as TextBox;

            BindingOperations.GetMultiBindingExpression(tBox1, TextBox.TextProperty).UpdateTarget();
            BindingOperations.GetMultiBindingExpression(tBox2, TextBox.TextProperty).UpdateTarget();
            BindingOperations.GetMultiBindingExpression(tBox3, TextBox.TextProperty).UpdateTarget();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TextBox_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ((ApplicationViewModel)DataContext).Save();
                var stackPanel = ((sender as TextBox)?.Parent as Grid)?.Parent as StackPanel;
                var stackPanel2 = stackPanel?.Children[2] as StackPanel;
                TextBlock? textBlock = stackPanel2?.Children[1] as TextBlock;
                BindingOperations.GetBindingExpression(textBlock, TextBlock.TextProperty).UpdateTarget();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var itemToCancel = UIHerlper.GetAncestorOfType<ListViewItem>(sender as Button);
            if (itemToCancel != null)
            {
                ((ApplicationViewModel)DataContext).DistributeEqually(itemToCancel.Content as WorkingDay);
            }
            TextBox tBoxPyr = UIHerlper.FindChild<TextBox>(Application.Current.MainWindow, "tBoxPyr");
            TextBox tBoxBox = UIHerlper.FindChild<TextBox>(Application.Current.MainWindow, "tBoxBox");
            TextBox tBoxPriv = UIHerlper.FindChild<TextBox>(Application.Current.MainWindow, "tBoxPriv");
            BindingOperations.GetMultiBindingExpression(tBoxPyr, TextBox.TextProperty).UpdateTarget();
            BindingOperations.GetMultiBindingExpression(tBoxBox, TextBox.TextProperty).UpdateTarget();
            BindingOperations.GetMultiBindingExpression(tBoxPriv, TextBox.TextProperty).UpdateTarget();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectionStart = 0;
            ((TextBox)sender).SelectionLength = ((TextBox)sender).Text.Length;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cbYears.SelectedIndex = cbYears.Items.Count - 1;
        }

        private void cbMonths_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int month = cbMonths.SelectedIndex + 1;
            if (cbYears.IsLoaded)
            {
                int year = ((DateTime)cbYears.SelectedItem).Year;
                ((ApplicationViewModel)DataContext).LoadSelectedMonthAndYear(month, year);
            }
            else
                ((ApplicationViewModel)DataContext).LoadSelectedMonthAndYear(month, DateTime.Now.Year);
            listView.ItemsSource = ((ApplicationViewModel)DataContext).WorkingDays;
        }

        private void tBoxNewPerson_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((TextBox)sender).Text.Length > 0)
            {
                btnAddNewPerson.IsEnabled = true;
            }
            else
                btnAddNewPerson.IsEnabled = false;
        }

        private void lBoxPersons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lBoxPersons.SelectedItems.Count > 0)
            {
                btnDeletePerson.IsEnabled = true;
            }
            else
                btnDeletePerson.IsEnabled = false;
        }

        private void btnAddNewPerson_Click(object sender, RoutedEventArgs e)
        {
            ((ApplicationViewModel)DataContext).AddNewPerson(tBoxNewPerson.Text);
            tBoxNewPerson.Text = string.Empty;
        }

        private void btnDeletePerson_Click(object sender, RoutedEventArgs e)
        {
            ((ApplicationViewModel)DataContext).DeletePerson(lBoxPersons.SelectedItem as Person);
        }






        //public T GetAncestorOfType<T>(FrameworkElement child) where T : FrameworkElement
        //{
        //    var parent = VisualTreeHelper.GetParent(child);
        //    if (parent != null && !(parent is T))
        //        return (T)GetAncestorOfType<T>((FrameworkElement)parent);
        //    return (T)parent;
        //}

    }
}
