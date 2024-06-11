using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp2.AppData;

namespace WpfApp2.PageAdmin
{
    /// <summary>
    /// Логика взаимодействия для PageItem.xaml
    /// </summary>
    public partial class PageItem : Page
    {
        public PageItem()
        {
            InitializeComponent();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.FrameMain.Navigate(new AddEditPage((sender as Button).DataContext as Item));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            AppConnect.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            DGridItem.ItemsSource = AppConnect.GetContext().Item.ToList();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var itemForRemoving = DGridItem.SelectedItems.Cast<Item>().ToList();
            if (MessageBox.Show($"Вы действительно хотите удалить выбранных элементов?", "Уведомление!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                AppConnect.GetContext().Item.RemoveRange(itemForRemoving);
                AppConnect.GetContext().SaveChanges();
                MessageBox.Show("Данные удалены!");
                DGridItem.ItemsSource = AppConnect.GetContext().Item.ToList();
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.FrameMain.Navigate(new AddEditPage(null));
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.FrameMain.GoBack();
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textSearch = TBoxSearch.Text;

            var filteredItem = AppConnect.GetContext().Item.Where(item => item.ItemName.ToString().Contains(textSearch)).ToList();

            DGridItem.ItemsSource = filteredItem;
        }
    }
}
