using Laverna.Models;
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

namespace Laverna.Pages
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public List<Equipments> equipments = new List<Equipments>();
        public Main()
        {
            InitializeComponent();
            Bindings();
            if (equipments != null)
            {
                equipments = App.db.Equipments.ToList();
            }
            Filter();
        }

        private void Filter()
        {
            var TextUser = TBName.Text.ToLower();
            var SelectedType = "";
            var SelectedStatus = "";

            if (this.DGEquipment == null)
            {
                return;
            }

            if (CBType.SelectedItem is Types Types)
            {
                SelectedType = Types.Name;
            }

            if (CBStatus.SelectedItem is Status Status)
            {
                SelectedStatus = Status.Name;
            }
           
            var FilteredList = equipments.Where(p =>
            (string.IsNullOrEmpty(TextUser) || (p.Name != null && p.Name.ToLower().Contains(TextUser))) &&
            (string.IsNullOrEmpty(SelectedType) || (p.Types != null && p.Types.Name == SelectedType)) &&
            (string.IsNullOrEmpty(SelectedStatus) || (p.Status != null && p.Status.Name == SelectedStatus))).ToList();
            // Получаем отфильтрованный лист и биндим его
            DGEquipment.ItemsSource = FilteredList;
        }

        private void Bindings()
        {
            DGEquipment.ItemsSource = App.db.Equipments.ToList();
            CBType.ItemsSource = App.db.Types.ToList();
            CBStatus.ItemsSource = App.db.Status.ToList();
        }

        private void BTAdd_Click(object sender, RoutedEventArgs e)
        {
            bool flag = false;
            NavigationService.Navigate(new Pages.Add(null, flag));
        }

        private void BTDelete_Click(object sender, RoutedEventArgs e)
        {
            if(DGEquipment.SelectedItem != null)
            {
                // Здесь вытаскиваем выбранное оборудование и удаляем его
                if (DGEquipment.SelectedItem is Equipments SelectedEquipment)
                {
                    App.db.Equipments.Remove(SelectedEquipment);
                    App.db.SaveChanges();
                    equipments.Remove(SelectedEquipment);
                    Bindings();
                    Filter();
                }
            }
        }

        private void BTEdit_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            if (DGEquipment.SelectedItem != null)
            {
                if (DGEquipment.SelectedItem is Equipments SelectedEquipment)
                {
                    NavigationService.Navigate(new Pages.Add(SelectedEquipment, flag));
                }
            }
        }

        private void TBName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            Bindings();
        }

        private void CBStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void CBType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void BTClear_Click(object sender, RoutedEventArgs e)
        {
            TBName.Clear();
            CBType.SelectedItem = null;
            CBStatus.SelectedItem = null;
            DGEquipment.SelectedItem = null;
        }
    }
}